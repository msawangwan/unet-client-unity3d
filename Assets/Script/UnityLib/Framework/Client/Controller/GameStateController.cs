﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityAdt;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityLib.Framework.Net;

namespace UnityLib.Framework.Client {
    public class GameStateController : ControllerBehaviour {
        public const int kMAIN_MENU = 0;
        public const int kGAME_PLAY = 1;

        private Queue<Action> commandPipelineStageOne = new Queue<Action>();
        private Queue<Action> errorPipelineStageOne = new Queue<Action>();

        private List<GameObject> worldNodes = new List<GameObject>();

        private GameState loadedGameState;

        public Profile LoadedProfile {
            get {
                return loadedGameState.currentProfile;
            }
        }

        public GameParameter LoadedGameParameters {
            get {
                return loadedGameState.currentGameParameters;
            }
        } 

        public void EnqueueStageOneCommand(Action c) {
            commandPipelineStageOne.Enqueue(c);
        }

        public void EnqueueStageOneError(Action e) {
            errorPipelineStageOne.Enqueue(e);
        }

        public IEnumerator VerifyProfileValidRoutine(string profileName) {
            string json = JsonUtility.ToJson(new ProfileSearch(profileName));

            Handler<ProfileSearch> handler = new Handler<ProfileSearch>(json);
            handler.SendJsonRequest("POST", ServiceController.Debug_Addr_Availability);

            do {
                yield return null;
                if (handler.onDone != null) {
                    ProfileSearch returnedSearchResult = handler.onDone();
                    if (returnedSearchResult.IsAvailable) {
                        Debug.LogFormat("success, name available:{0}", returnedSearchResult.Name);
                        ExecuteCommandPipeline();

                        break;
                    } else {
                        Debug.LogErrorFormat("error, name is already taken: {0}", returnedSearchResult.Name);
                        handler.Reset();
                        returnedSearchResult = null;
                        ExecuteErrorPipeline();
                    }
                }
            } while (true);
        }

        public IEnumerator CreateProfile(string profileName) {
            string json = JsonUtility.ToJson(new ProfileName(profileName));

            Handler<GameState> handler = new Handler<GameState>(json);
            handler.SendJsonRequest("POST", ServiceController.Debug_Addr_Create_Profile);

            do {
                yield return null;
                if (handler.onDone != null) {
                    loadedGameState = handler.onDone();
                    LoadedProfile.isLoaded = true;

                    Debug.LogFormat("created a profile with name {0} and uuid {1}", LoadedProfile.Name, LoadedProfile.UUID);
                    Debug.LogFormat("loaded game state with seed {0} and star count {1}", LoadedProfile.Seed, LoadedGameParameters.nodeCount);

                    ExecuteCommandPipeline();

                    break;
                }
            } while (true);
        }

        public IEnumerator LoadWorldData() {
            SceneManager.LoadSceneAsync(kGAME_PLAY, LoadSceneMode.Additive);

            List<GameObject> gos = null;
            Quadrant q = null;
            Scene s = SceneManager.GetSceneAt(kGAME_PLAY);

            do {
                yield return null;
                if (LoadedProfile != null) {
                    if (LoadedProfile.isLoaded) {
                        Debug.LogFormat("loading gameplay scene ...");
                        if (!s.isLoaded) {
                            Debug.LogFormat("gameplay scene is still loading ...");
                            yield return new WaitForEndOfFrame();
                        } else {
                            Debug.LogFormat("gameplay scene loaded");

                            q = Quadrant.InstantiateQuadrantRootGameObject(Vector3.zero, LoadedGameParameters.worldScale, LoadedGameParameters.nodeRadius);
                            gos = Quadrant.InstantiateSubQuadrantGameObjects(q, LoadedGameParameters.nodeCount); // LoadedGameState.currentStarMap.starCount

                            pRNG r = new pRNG((ulong)LoadedProfile.Seed); // LoadedGameState.currentStarMap.seed

                            Quadrant.Partition(q, gos, r, LoadedGameParameters.maximumAttemptsWhenSpawningNodes);

                            break;
                        }
                    }
                }
            } while (true);

            do {
                yield return null;
                if (q.isInitialised) {
                    Debug.LogFormat("world data loaded");
                    break;
                } else {
                    Debug.LogFormat("loading world data ...");
                }
            } while (true);

            SceneManager.MoveGameObjectToScene(q.gameObject.transform.parent.gameObject, SceneManager.GetSceneAt(kGAME_PLAY));
            SceneManager.MoveGameObjectToScene(CameraRigController.S.gameObject, SceneManager.GetSceneAt(kGAME_PLAY));

            string json = JsonUtility.ToJson(LoadedProfile);

            Handler<Confirmation> handler = new Handler<Confirmation>(json);
            handler.SendJsonRequest("POST", ServiceController.Debug_Addr_Store_World_Data);

            do {
                yield return null;
                if (handler.onDone != null) {
                    Confirmation ok = handler.onDone();
                    if (ok.OK == 1) {
                        Debug.Log("world ready"); // todo: handle errors and such
                    }
                    break;
                }
            } while (true);

            CameraRigController.S.EnableMovement();

            // enter game loop
        }

        protected override bool OnInit() {
            return true; 
        }

        private void ExecuteCommandPipeline() {
            if (commandPipelineStageOne.Count <= 0) {
                return;
            }

            while (commandPipelineStageOne.Count > 0) {
                Action current = commandPipelineStageOne.Dequeue();
                if (current != null) {
                    current();
                }
            }
        }

        private void ExecuteErrorPipeline() {
            if (errorPipelineStageOne.Count <= 0) {
                return;
            }

            while (errorPipelineStageOne.Count > 0) {
                Action current = errorPipelineStageOne.Dequeue();
                if (current != null) {
                    current();
                }
            }
        }
    }
}

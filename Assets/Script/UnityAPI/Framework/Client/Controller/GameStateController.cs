using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAPI.Game;
using UnityAPI.Framework.Game;
using UnityAPI.Framework.Net;

namespace UnityAPI.Framework.Client {
    public class GameStateController : ControllerBehaviour {
        public const int kMAIN_MENU = 0;
        public const int kGAME_PLAY = 1;

        private Queue<Action> commandPipelineStageOne = new Queue<Action>();
        private Queue<Action> errorPipelineStageOne = new Queue<Action>();

        public GameState LoadedGameState {
            get;
            private set;
        }

        public Profile LoadedProfile {
            get;
            private set;
        }

        public void EnqueueStageOneCommand(Action c) {
            commandPipelineStageOne.Enqueue(c);
        }

        public void EnqueueStageOneError(Action e) {
            errorPipelineStageOne.Enqueue(e);
        }

        public IEnumerator VerifyProfileValidRoutine(string profileName) {
            Handler<ProfileSearch> handler = new Handler<ProfileSearch>();
            string json = JsonUtility.ToJson(new ProfileSearch(profileName));

            handler.SendJsonRequest(json, "POST", ServiceController.Debug_Addr_Availability);

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
            Handler<GameState> handler = new Handler<GameState>();
            string json = JsonUtility.ToJson(new ProfileName(profileName));

            handler.SendJsonRequest(json, "POST", ServiceController.Debug_Addr_Create_Profile);

            do {
                yield return null;
                if (handler.onDone != null) {
                    LoadedGameState = handler.onDone();
                    LoadedProfile = LoadedGameState.currentProfile;
                    LoadedProfile.isLoaded = true;
                    Debug.LogFormat("created a profile with name {0} and uuid {1}", LoadedProfile.Name, LoadedProfile.UUID);
                    Debug.LogFormat("loaded game state with seed {0} and star count {1}", LoadedGameState.currentStarMap.seed, LoadedGameState.currentStarMap.starCount);
                    ExecuteCommandPipeline();
                    break;
                }
            } while (true);
        }

        public IEnumerator LoadWorldData() {
            do {
                yield return null;
                if (LoadedProfile != null) {
                    if (LoadedProfile.isLoaded) {
                        Debug.LogFormat("loading world data ...");

                        // CoreFramework.Adt.QuadTree tree = CoreFramework.Adt.QuadTree.CreateQuadTree(Vector3.zero);
                        Quadrant q = Quadrant.InstantiateQuadrantRoot(Vector3.zero);

                        SceneManager.LoadSceneAsync(kGAME_PLAY, LoadSceneMode.Additive);
                        Scene s = SceneManager.GetSceneAt(kGAME_PLAY);

                        if (!s.isLoaded) {
                            yield return new WaitForEndOfFrame();
                        }

                        SceneManager.MoveGameObjectToScene(q.gameObject.transform.parent.gameObject, SceneManager.GetSceneAt(kGAME_PLAY));
                        // q.
                        // StartCoroutine(tree.GenerateFromSeed(LoadedGameState.currentStarMap.starCount, LoadedGameState.currentStarMap.seed));
                        List<GameObject> gos = Quadrant.InstantiateSubQuadrantGameObjects(q, LoadedGameState.currentStarMap.starCount);
                        Quadrant.SortQuadrants(q, gos);
                        break;
                    }
                }
            } while (true);
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

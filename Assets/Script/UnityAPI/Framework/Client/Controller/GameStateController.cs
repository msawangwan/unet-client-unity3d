using System;
using System.Collections;
using System.Collections.Generic;
using Engine.pRNG;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAPI.Framework.Net;

namespace UnityAPI.Framework.Client {
    public class GameStateController : ControllerBehaviour {
        public const int kMAIN_MENU = 0;
        public const int kGAME_PLAY = 1;

        private Queue<Action> commandPipelineStageOne = new Queue<Action>();
        private Queue<Action> errorPipelineStageOne = new Queue<Action>();

        private List<GameObject> worldNodes = new List<GameObject>();

        public GameState LoadedGameState {
            get;
            private set;
        }

        public Profile LoadedProfile {
            get {
                return LoadedGameState.currentProfile;
            }
        }

        public StarMap LoadedStarMap {
            get {
                return LoadedGameState.currentStarMap;
            }
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
                    LoadedProfile.isLoaded = true;

                    Debug.LogFormat("created a profile with name {0} and uuid {1}", LoadedProfile.Name, LoadedProfile.UUID);
                    Debug.LogFormat("loaded game state with seed {0} and star count {1}", LoadedGameState.currentStarMap.seed, LoadedGameState.currentStarMap.starCount);

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

                            q = Quadrant.InstantiateQuadrantRootGameObject(Vector3.zero);
                            gos = Quadrant.InstantiateSubQuadrantGameObjects(q, 9); // LoadedGameState.currentStarMap.starCount

                            pRNG r = new pRNG(1482284596187742126); // LoadedGameState.currentStarMap.seed

                            Quadrant.Partition(q, gos, r);

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

            StarData data = new StarData(gos.Count);

            int i = 0; // todo: optimize this out
            foreach (GameObject go in gos) {
                data.AddPoint(go.transform.position, i);
                go.AddComponent<StarNode>();
                i++;
            }

            Handler<StarData> handler = new Handler<StarData>();
            string json = JsonUtility.ToJson(data);

            Debug.LogFormat("data to send: {0}", json);

            handler.SendJsonRequest(json, "POST", ServiceController.Debug_Addr_Store_World_Data);

            do {
                yield return null;
                if (handler.onDone != null) {
                    Debug.Log("sent world data");
                    break;
                }
            } while (true);

            worldNodes = gos;

            CameraRigController.S.EnableMovement();
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

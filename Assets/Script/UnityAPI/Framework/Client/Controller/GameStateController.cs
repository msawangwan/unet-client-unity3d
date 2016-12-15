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
        
        public static void LoadSceneAtIndex(int sceneIndex) {
            SceneManager.LoadSceneAsync(sceneIndex);
        }

        public static void LoadSceneAtIndexAdditive(int sceneIndex) {
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
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

        public IEnumerator MainMenuRoutine() {
            do {
                yield return null;
                if (LoadedProfile != null) {
                    if (LoadedProfile.isLoaded) {
                        break;
                    }
                }
            } while (true);
        }

        public IEnumerator VerifyProfileValidRoutine(string profileName) { // this shouldn't be limited to just the one screen it's started on
            ProfileNameVerificationHandler handler = new ProfileNameVerificationHandler();
            Func<ProfileSearch> searchResult = null;
            string json = JsonUtility.ToJson(new ProfileSearch(profileName));

            handler.VerifyNameIsAvailable(json, searchResult); // begin async request

            do {
                yield return null;
                if (handler.onDone != null) {
                    ProfileSearch returnedSearchResult = handler.onDone();
                    if (returnedSearchResult.IsAvailable) {
                        Debug.LogFormat("success: loaded profile: {0}", returnedSearchResult.Name);
                        // LoadedProfile = new Profile(returnedSearchResult.Name);
                        ExecuteCommandPipeline();
                        break;
                    } else {
                        Debug.LogErrorFormat("error: failed to load profile: {0}", returnedSearchResult.Name);
                        handler.Reset();
                        returnedSearchResult = null;
                        ExecuteErrorPipeline();
                    }
                }
            } while (true);

            // LoadedProfile.isLoaded = true;
        }

        public IEnumerator CreateProfile(string profileName) {
            ProfileCreateHandler handler = new ProfileCreateHandler();
            string json = JsonUtility.ToJson(new ProfileName(profileName));

            handler.CreateNewProfile(json);

            do {
                yield return null;
                if (handler.onDone != null) {
                    LoadedProfile = handler.onDone();
                    LoadedProfile.isLoaded = true;
                    ExecuteCommandPipeline();
                    Debug.Log("LOADED PROFILE " + LoadedProfile.Name);
                    break;
                }
            } while (true);
        }

        public IEnumerator LoadGame() {
            do {
                yield return null;
                if (LoadedProfile != null) {
                    if (LoadedProfile.isLoaded) {
                        Debug.LogFormat("game loaded");
                        LoadSceneAtIndexAdditive(kGAME_PLAY);
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

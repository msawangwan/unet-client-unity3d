using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityAPI.Model;
using UnityAPI.Framework.Net;

namespace UnityAPI.Global {
    public class GlobalGameController : ControllerBehaviour {
        private Queue<Action> stage1Pipeline = new Queue<Action>();
        public Profile loadedProfile;
        public Action onSuccess;

        public void QueueStage1Action(Action a) {
            stage1Pipeline.Enqueue(a);
        }

        private void ExecuteStage1Actions() {
            if (stage1Pipeline.Count <= 0) {
                return;
            }
            while (stage1Pipeline.Count > 0) {
                Action current = stage1Pipeline.Dequeue();
                if (current != null) {
                    current();
                }
            }
        }

        public IEnumerator MainMenuRoutine() {
            do {
                yield return null;
                if (loadedProfile != null) {
                    if (loadedProfile.isLoaded) {
                        break;
                    }
                }
            } while (true);
        }

        public IEnumerator VerifyProfileValidRoutine(string profileName) {
            ProfileNameVerificationHandler handler = new ProfileNameVerificationHandler();
            Func<ProfileSearch> searchResult = null;
            string json = UnityEngine.JsonUtility.ToJson(new ProfileSearch(profileName));

            handler.VerifyNameIsAvailable(json, searchResult); // begin async request

            do {
                yield return null;
                if (handler.onDone != null) {
                    ProfileSearch returnedSearchResult = handler.onDone();
                    if (returnedSearchResult.IsAvailable) {
                        Debug.LogFormat("success: loaded profile: {0}", returnedSearchResult.Name);
                        loadedProfile = new Profile(returnedSearchResult.Name);
                        // if (onSuccess != null) {
                        //     onSuccess();
                        // }
                        ExecuteStage1Actions();
                        break;
                    } else {
                        Debug.LogFormat("error: failed to load profile: {0}", returnedSearchResult.Name);
                        handler.Reset();
                        returnedSearchResult = null;
                    }
                }
            } while (true);

            loadedProfile.isLoaded = true;
        }

        protected override bool OnInit() {
            return true; 
        }
    }
}

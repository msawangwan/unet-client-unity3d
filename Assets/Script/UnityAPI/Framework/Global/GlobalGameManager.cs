using System;
using System.Collections;
using UnityEngine;
using UnityAPI.Model;
using UnityAPI.Framework.Net;

namespace UnityAPI.Global {
    public class GlobalGameManager : ControllerBehaviour {
        public static Action OnProfileLoaded;

        public Profile loadedProfile;

        // public IEnumerator GameStartRoutine() {
        //     yield return null;

        //     if (loadedProfile == null) {
        //         yield return StartCoroutine(VerifyProfileValidRoutine())
        //     }
        // }

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
            ProfileSearch newProfile = new ProfileSearch(profileName);
            string json = UnityEngine.JsonUtility.ToJson(newProfile);
            Func<ProfileSearch> searchResult = null;
            ProfileNameVerificationHandler h = new ProfileNameVerificationHandler();
            h.POSTasync(json, searchResult);

            do {
                yield return null;
                if (h.onDone != null) {
                    ProfileSearch returnedSearchResult = h.onDone();
                    Debug.Log("now " + returnedSearchResult.isAvailable + " " + returnedSearchResult.Name);
                    // yield return null;
                    // Debug.Log("later " + returnedSearchResult.isAvailable);
                    if (returnedSearchResult.isAvailable) {
                        Debug.LogFormat("loaded profile: {0}", returnedSearchResult.Name);
                        loadedProfile = new Profile(returnedSearchResult.Name);
                        break;
                    } else {
                        Debug.LogFormat("failed to load profile: {0}", returnedSearchResult.Name);
                        h.onDone = null;
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

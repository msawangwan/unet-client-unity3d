using UnityEngine;
using System.Collections.Generic;

namespace UnityLib {
    public abstract class InstanceBehaviour : MonoBehaviour {
        private static List<int> instanceIDs = new List<int>();

        private static int currentInstanceID = -1;
        private static int NextAvailableInstanceID {
            get {
                while (true) { // todo: might need a better method than this while loop bullshit
                    ++currentInstanceID;
                    if (!instanceIDs.Contains(currentInstanceID)) {
                        instanceIDs.Add(currentInstanceID);
                        return currentInstanceID;
                    }
                }
            }
        }

        public bool IsInitialised {
            get {
                return isInitialised;
            }
        }

        protected int instanceID = -1;

        private bool isInitialised = false;

        public void Init() { // called in start if not manually called on instantiation
            if (!isInitialised) {
                isInitialised = true;
                instanceID = NextAvailableInstanceID;
            }
        }

        protected virtual void Awake() {
            if (!IsInitialised) { // call this if it wasn't called when object was instantiated
                Init();
            }
        }
    }
}

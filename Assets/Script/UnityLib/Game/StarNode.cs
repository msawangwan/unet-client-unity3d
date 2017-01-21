using UnityEngine;

namespace UnityLib {
    public class Star : SelectableNode {
        [System.Serializable]
        public class AsJson : IJSONer {
            public float x;
            public float y;
            public bool isValid;

            public string Marshall() { return JsonUtility.ToJson(this); }
        }

    }
}

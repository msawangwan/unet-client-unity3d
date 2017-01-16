namespace UnityLib {
    [System.Serializable]
    public class JsonBool :IJSONer {
        public bool @value;

        public JsonBool() {}

        public JsonBool(bool @value) {
            this.@value = @value;
        }

        public string Marshall() {
            return UnityEngine.JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    public class JsonBoolWithKey :IJSONer {
        public int key;
        public bool @value;

        public JsonBoolWithKey() {}

        public JsonBoolWithKey(int key, bool @value) {
            this.key = key;
            this.@value = @value;
        }

        public string Marshall() {
            return UnityEngine.JsonUtility.ToJson(this);
        }
    }
}

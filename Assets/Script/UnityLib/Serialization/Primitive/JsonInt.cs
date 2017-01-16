// type JsonInt wraps an int for server message compatability
namespace UnityLib {
    [System.Serializable]
    public class JsonInt : IJSONer {
        public int @value;
        
        public JsonInt() {}

        public JsonInt(int @value) {
            this.@value = @value;
        }

        public string Marshall() {
            return UnityEngine.JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    public class JsonIntWithKey : IJSONer {
        public int key;
        public int @value;
        
        public JsonIntWithKey() {}

        public JsonIntWithKey(int key, int @value) {
            this.key = key;
            this.@value = @value;
        }

        public string Marshall() {
            return UnityEngine.JsonUtility.ToJson(this);
        }
    }
}

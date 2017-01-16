// type JsonLong wraps an int64 for server message compatability
namespace UnityLib {
    [System.Serializable]
    public class JsonLong : IJSONer {
        public long @value;
        
        public JsonLong() {}

        public JsonLong(long @value) {
            this.@value = @value;
        }

        public string Marshall() {
            return UnityEngine.JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    public class JsonLongWithKey : IJSONer {
        public long key;
        public long @value;
        
        public JsonLongWithKey() {}

        public JsonLongWithKey(int key, long @value) {
            this.key = key;
            this.@value = @value;
        }

        public string Marshall() {
            return UnityEngine.JsonUtility.ToJson(this);
        }
    }
}

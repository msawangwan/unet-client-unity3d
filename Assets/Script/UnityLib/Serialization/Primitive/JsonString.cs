// type JsonString wraps an string for server message compatability
namespace UnityLib {
    [System.Serializable]
    public class JsonString : IJSONer {
        public string @value;
        
        public JsonString() {}

        public JsonString(string @value) {
            this.@value = @value;
        }

        public string Marshall() {
            return UnityEngine.JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    public class JsonStringWithKey : IJSONer {
        public int key;
        public string @value;
        
        public JsonStringWithKey() {}

        public JsonStringWithKey(int key, string @value) {
            this.key = key;
            this.@value = @value;
        }

        public string Marshall() {
            return UnityEngine.JsonUtility.ToJson(this);
        }
    }

}
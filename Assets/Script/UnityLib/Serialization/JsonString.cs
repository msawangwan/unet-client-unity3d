// type JsonString wraps an string for server message compatability
namespace UnityLib {
    [System.Serializable]
    public class JsonString {
        public string @value;
        
        public JsonString() {}

        public JsonString(string @value) {
            this.@value = @value;
        }
    }

    [System.Serializable]
    public class JsonStringWithKey {
        public int key;
        public string @value;
        
        public JsonStringWithKey() {}

        public JsonStringWithKey(int key, string @value) {
            this.key = key;
            this.@value = @value;
        }
    }

}
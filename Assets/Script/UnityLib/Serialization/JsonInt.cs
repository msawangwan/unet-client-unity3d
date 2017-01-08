// type JsonInt wraps an int for server message compatability
namespace UnityLib {
    [System.Serializable]
    public class JsonInt {
        public int @value;
        
        public JsonInt() {}

        public JsonInt(int @value) {
            this.@value = @value;
        }
    }

}

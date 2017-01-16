// type JsonEmpty is a null
namespace UnityLib {
    [System.Serializable]
    public class JsonEmpty {
        public JsonEmpty() {}
    }

    // this is for future:
    // messing around with casting to anonymous types
    // uses included:
    // passing as a paramter and accessing its fields
    // thoughts about it but maybe some json uses?
    public static class JsonExtension {
        public static T  Cast<T>(this T anon) {
            return (T)anon;
        }
    }
}

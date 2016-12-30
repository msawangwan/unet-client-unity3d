using UnityEngine;

namespace UnityLib {
    public static class ExtensionGameObject {
        public static GameObject GameObjectFromInterface<T>(this T i) {
            return (i as MonoBehaviour).gameObject;
        }
    }
}

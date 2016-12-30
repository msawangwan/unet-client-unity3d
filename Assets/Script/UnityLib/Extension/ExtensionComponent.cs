using UnityEngine;

namespace UnityLib {
    public static class ExtensionComponent {
        public static T GetComponentSafe<T>(this GameObject go) where T : Component {
            T c = go.GetComponent<T>();
            if (c == null) {
                c = go.gameObject.AddComponent<T>();
            }
            return c;
        }

        public static T GetComponentInterface<T>(this GameObject go) where T : class {
            return go.GetComponent(typeof(T)) as T;
        }
    }
}

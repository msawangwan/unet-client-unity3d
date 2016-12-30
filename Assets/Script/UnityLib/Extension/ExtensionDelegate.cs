using System;

namespace UnityLib {
    public static class ExtensionDelegate {
        public static void InvokeSafe(this Action a ) {
            if (a != null) {
                a();
            }
        }

        public static void InvokeSafe<T>(this Action<T> a, T t) {
            if (a != null) {
                a(t);
            }
        }
    }
}

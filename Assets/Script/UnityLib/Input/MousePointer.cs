using UnityEngine;

namespace UnityLib.Framework.Client {
    public static class MousePointer {
        public static Vector3 ScreenCoordinates(float depth = 0f) {
            return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth));
        }
    }
}
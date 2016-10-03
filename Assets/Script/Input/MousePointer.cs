using UnityEngine;

public static class MousePointer {

    public static Vector3 ScreenCoordinates ( float depth = 10.0f ) {
        return Camera.main.ScreenToWorldPoint ( new Vector3 ( Input.mousePosition.x, Input.mousePosition.y, depth ) );
    }
}
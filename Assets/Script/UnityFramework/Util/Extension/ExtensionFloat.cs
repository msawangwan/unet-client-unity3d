using UnityEngine;

public static class ExtensionFloat {
    public static Quaternion AsXRotation ( this float x ) {
        return Quaternion.Euler (x, 0f, 0f);
    }

    public static Quaternion AsYRotation ( this float y ) {
        return Quaternion.Euler (0f, y, 0f);
    }

    public static Quaternion AsZRotation ( this float z ) {
        return Quaternion.Euler (0f, 0f, z);
    }
}

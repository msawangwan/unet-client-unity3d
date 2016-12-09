using UnityEngine;

public static class GameObjectUtil {
    public static GameObject GameObjectFromInterface<T> (T i) {
        return (i as MonoBehaviour).gameObject;
    }
}

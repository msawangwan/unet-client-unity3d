using UnityEngine;

public static class CommonUtil {
    /* wrapper for common pattern */
    public static T EnablePersistance<T>(T singleton, GameObject instance) {
        MonoBehaviour.DontDestroyOnLoad(instance);
        return singleton;
    }
}

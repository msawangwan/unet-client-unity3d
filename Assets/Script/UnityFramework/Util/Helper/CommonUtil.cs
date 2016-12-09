using UnityEngine;

public static class CommonUtil {
    /* wrapper for common pattern */
    public static T EnablePersistance<T>(T singleton, GameObject instance) {
        instance.transform.parent = null;
        MonoBehaviour.DontDestroyOnLoad(instance);
        return singleton;
    }
}

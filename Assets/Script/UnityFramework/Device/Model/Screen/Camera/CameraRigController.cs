using UnityEngine;

public class CameraRigController : MonoBehaviour {
    public static CameraRigController StaticInstance = null;

    private void Awake () {
        StaticInstance = CommonUtil.EnablePersistance (this, gameObject);
    }
}

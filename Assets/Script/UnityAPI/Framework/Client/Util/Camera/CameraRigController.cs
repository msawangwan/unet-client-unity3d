using UnityEngine;

namespace UnityAPI.Framework.Game {
    public class CameraRigController : SingletonBehaviour<CameraRigController> {
        // public static CameraRigController StaticInstance = null;

        // private void Awake () {
        //     StaticInstance = CommonUtil.EnablePersistance (this, gameObject);
        // }

        public CameraZoomController zoomController;
        public CameraPanController panController;

        private void Start() {

        }
    }
}

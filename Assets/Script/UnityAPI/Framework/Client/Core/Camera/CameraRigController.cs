using UnityEngine;

namespace UnityAPI.Framework.Client {
    public class CameraRigController : SingletonBehaviour<CameraRigController> {
        public CameraZoomController zoomController;
        public CameraPanController panController;

        public void EnableMovement() {
            if (panController == null) {
                panController = Camera.main.gameObject.AddComponent<CameraPanController>();
            }

            if (zoomController == null) {
                zoomController = Camera.main.gameObject.AddComponent<CameraZoomController>();
            }

            panController.isActive = true;
            zoomController.isActive = true;
        }

        public void DisableMovement() {
            if (panController) {
                panController.isActive = false;
            }

            if (zoomController) {
                zoomController.isActive = false;
            }
        }
    }
}

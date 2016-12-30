using UnityEngine;

namespace UnityLib.Framework.Client {
    public class CameraPanController : MonoBehaviour {
        public float SmoothTime = 0.3f;
        public float MaxSpeed = 20.0f;
        public float KillZoneThreshold = 0.1f;

        public bool isActive = false;

        private Vector3 cameraRigTargetPosition = Vector3.zero;
        private Vector3 cameraVelocity = Vector3.zero;
        private bool isNotCentered = false;

        public void CenterOnSelected(Vector3 smoothDampTarget) {
            cameraRigTargetPosition = new Vector3(smoothDampTarget.x, smoothDampTarget.y, transform.position.z);
            cameraVelocity = Vector3.zero;
            isNotCentered = true;
        }

        private void LateUpdate() {
            if (!isActive){
                return;
            }

            Vector3 cameraRigPosition = transform.position;

            if (isNotCentered) {
                if ((cameraRigPosition - cameraRigTargetPosition) == Vector3.zero) {
                    isNotCentered = false;
                    return;
                }

                transform.position = Vector3.SmoothDamp(cameraRigPosition, cameraRigTargetPosition, ref cameraVelocity, SmoothTime, MaxSpeed);
            }
        }
    }
}

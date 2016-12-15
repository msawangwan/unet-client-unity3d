using UnityEngine;

namespace UnityAPI.Framework.Game {
    public class CameraZoomController : MonoBehaviour {
        public float ZoomSpeedMultiplier = 3.0f;
        public float SpeedSmoothingMultiplier = 0.1f;
        public float MinimumOrthographic = 1.0f;
        public float MaximumOrthographic = 20.0f;

        private Camera cam = null;

        private float targetOrthographicSize = 0.0f;
        private float scrollValue = 1.0f;
        private float scrollVelocity = 0.0f;

        private float moveSpeed { get { return SpeedSmoothingMultiplier * Time.deltaTime; } }

        private void Start () {
            cam = Camera.main;
            targetOrthographicSize = cam.orthographicSize;
        }

        private void LateUpdate () {
            scrollValue = Input.GetAxis(StringConstant.Input.MouseScrollWheel);
        
            if ( scrollValue != 0.0f ) {
                targetOrthographicSize -= scrollValue * ZoomSpeedMultiplier;
                targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, MinimumOrthographic, MaximumOrthographic);
            } else {
                //scrollVelocity = 0.0f;
            }

            cam.orthographicSize = Mathf.SmoothDamp (cam.orthographicSize, targetOrthographicSize, ref scrollVelocity, SpeedSmoothingMultiplier);
        }
    }
}

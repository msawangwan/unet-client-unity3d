using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
    private static CameraController s = null;
    private static readonly object padlock = new Object();

    private CameraController() {}

    public static CameraController S {
        get {
            lock (padlock) {
                if (s == null) {
                    s = GameObject.FindObjectOfType<CameraController>();
                }
                return s;
            }
        }
    }

    public float SmoothTime = 1.0f;
    public float MaxSpeed = 5.0f;
    public float CameraDefaultZ = -10.0f;

    Vector3 startPosition = Vector3.zero;
    Vector3 endPosition = Vector3.zero;
    Vector3 cameraVelocity = Vector3.zero;

    bool lerpEnabled = false;

    Vector3 cameraCurrentPosition { get { return new Vector3 ( transform.position.x, transform.position.y, CameraDefaultZ ); } }

    public void CenterOnSelected (Vector3 targetPosition) {
        cameraVelocity = Vector3.zero;
        startPosition = cameraCurrentPosition;
        endPosition = targetPosition;
        lerpEnabled = true;
    }

    void LateUpdate () {
        if (lerpEnabled == false) {
            return;
        }

        transform.position = Vector3.SmoothDamp(startPosition, endPosition, ref cameraVelocity, SmoothTime, MaxSpeed);

        if (cameraCurrentPosition - endPosition == Vector3.zero) {
            lerpEnabled = false;
        }
    }
}

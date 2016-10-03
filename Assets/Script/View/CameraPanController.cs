using UnityEngine;

public class CameraPanController : MonoBehaviour {
    private static CameraPanController s = null;
    private static readonly object padlock = new Object();

    private CameraPanController() {}

    public static CameraPanController S {
        get {
            lock (padlock) {
                if (s == null) {
                    s = GameObject.FindObjectOfType<CameraPanController>();
                }
                return s;
            }
        }
    }

    public float SmoothTime = 0.3f;
    public float MaxSpeed = 20.0f;
    public float KillZoneThreshold = 0.5f;
    public float CameraDefaultZ = -10.0f;

    Vector3 endPosition = Vector3.zero;
    Vector3 cameraVelocity = Vector3.zero;

    bool lerpEnabled = false;

    public void CenterOnSelected (Vector3 targetPosition) {
        cameraVelocity = Vector3.zero;
        endPosition = targetPosition + new Vector3 ( 0f, 0f, CameraDefaultZ );
        lerpEnabled = true;
    }

    private void Start () {
        s = this;
        DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate () {
        if (lerpEnabled == true) {
            if ( ( cameraCurrentPosition - endPosition ) == Vector3.zero ) {
                lerpEnabled = false;
            }
            transform.position = Vector3.SmoothDamp ( cameraCurrentPosition, endPosition, ref cameraVelocity, SmoothTime, MaxSpeed );
        }
    }

    private Vector3 cameraCurrentPosition { get { return new Vector3 ( transform.position.x, transform.position.y, CameraDefaultZ ); } }
}

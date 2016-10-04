using UnityEngine;

public class CameraPanController : MonoBehaviour {
    private static CameraPanController s = null;
    public static CameraPanController S {
        get {
            if (s == null) {
                s = GameObject.FindObjectOfType<CameraPanController> ();
                DontDestroyOnLoad ( s.gameObject );
                MapStarController.RaiseNodeSelected += OnNodeSelect;
            }
            return s;
        }
    }

    public float SmoothTime = 0.3f;
    public float MaxSpeed = 20.0f;
    public float KillZoneThreshold = 0.5f;

    private static Vector3 endPosition = Vector3.zero;
    private static Vector3 cameraVelocity = Vector3.zero;
    private static bool lerpEnabled = false;

    private static void CenterOnSelected ( Vector3 targetPosition, float z = -10.0f ) {
        cameraVelocity = Vector3.zero;
        endPosition = targetPosition + new Vector3 ( 0f, 0f, z );
        lerpEnabled = true;
    }

    private static void OnNodeSelect (MapStarNode starNode) {
        CenterOnSelected(starNode.gameObject.transform.position);
    }

    private void LoadSingletonInstance() { // make sure the property is called at least once
        Debug.LogFormat ( gameObject, "loaded {0}", gameObject.name ); 
    }

    private void Start () {
        CameraPanController.S.LoadSingletonInstance ();
    }

    private void LateUpdate () {
        Vector3 cameraCurrentPosition = transform.position;
        if (lerpEnabled == true) {
            if ( ( cameraCurrentPosition - endPosition ) == Vector3.zero ) {
                lerpEnabled = false;
            }
            transform.position = Vector3.SmoothDamp ( cameraCurrentPosition, endPosition, ref cameraVelocity, SmoothTime, MaxSpeed );
        }
    }
}

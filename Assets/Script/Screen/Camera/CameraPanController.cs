using UnityEngine;

public class CameraPanController : MonoBehaviour {
    public static CameraPanController StaticInstance = null;

    public float SmoothTime = 0.3f;
    public float MaxSpeed = 20.0f;
    public float KillZoneThreshold = 0.1f;

    private static Vector3 cameraRigTargetPosition = Vector3.zero;
    private static Vector3 cameraVelocity = Vector3.zero;
    private static bool isNotCentered = false;

    private static void CenterOnSelected ( Vector3 smoothDampTarget ) {
        cameraRigTargetPosition = smoothDampTarget;
        cameraVelocity = Vector3.zero;
        isNotCentered = true;
    }

    private static void OnNodeSelect ( StarNode starNode ) {
        CenterOnSelected(starNode.transform.position);
    }

    private void Start () {
        StaticInstance = CommonUtil.EnablePersistance(this, gameObject);
        StarMapController.RaiseNodeSelected += OnNodeSelect;
    }

    private void LateUpdate () {
        Vector3 cameraRigPosition = transform.position;

        if (isNotCentered == true) {
            if ( ( cameraRigPosition - cameraRigTargetPosition ) == Vector3.zero ) {
                isNotCentered = false;
                return;
            }

            transform.position = Vector3.SmoothDamp ( cameraRigPosition, cameraRigTargetPosition, ref cameraVelocity, SmoothTime, MaxSpeed );
        }
    }
}

using UnityEngine;

public class PlayerStatus : MonoBehaviour {
	public static Vector3 CurrentPosition = Vector3.zero;
    public static Vector3 PotentialPosition = Vector3.zero;

    void OnTargetNodeSelected (StarNode a) {
        PotentialPosition = a.gameObject.transform.position;
        StarMapRoute.Instance.DrawRoute(CurrentPosition, PotentialPosition);
    }

	void OnNullSelection() {
        PotentialPosition = CurrentPosition;
        StarMapRoute.Instance.ClearRoute();
    }

	void Start () {
		StarMapController.RaiseNodeSelected += OnTargetNodeSelected;
        StarMapController.RaiseNodeDeselected += OnNullSelection;
    }
}

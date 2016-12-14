using UnityEngine;

public class Player : MonoBehaviour {
    // [System.Serializable]
    // public class PlayerProfile {
    //     public string Name;
    // }
    
    // [SerializeField] private PlayerProfile profile;
    // public PlayerState State = null;

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

        // Debug.LogFormat("{0}", profile.Name);
    }
}

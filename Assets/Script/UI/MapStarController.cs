using UnityEngine;
using System;

public class MapStarController : MonoBehaviour {
    private static MapStarController s = null;
    public static MapStarController S { 
        get {
            if ( s == null ) {
                s = GameObject.FindObjectOfType<MapStarController> ();
                DontDestroyOnLoad ( s.gameObject );
                SelectionArea.S.RaiseSelectionAreaDownEvent += NotifyNodeDeselect;
            }
            return s;
        }
    }

	public static Action<MapStarNode> RaiseNodeSelected { get; set; }
	public static Action RaiseNodeDeselected { get; set; }

	public static void NotifyNodeSelected (MapStarNode starNode) {
        EventController.SafeInvoke(RaiseNodeSelected, starNode);
    }

	private static void NotifyNodeDeselect () {
        EventController.SafeInvoke(RaiseNodeDeselected);
    }

    private void LoadSingletonInstance() {
        Debug.LogFormat ( gameObject, "loaded {0}", gameObject.name );
    }

    private void Start () {
        MapStarController.S.LoadSingletonInstance ();
    }
}

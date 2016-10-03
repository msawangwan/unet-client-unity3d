using UnityEngine;
using System;

public class MapStarController : MonoBehaviour {
    public static MapStarController S = null;

	public Action<MapStarNode> RaiseNodeSelected { get; set; }
	public Action RaiseNodeDeselected { get; set; }

    void Start () {
        S = this;
        DontDestroyOnLoad(gameObject);

        SelectionArea.S.RaiseSelectionAreaDownEvent += NotifyNodeDeselect;
    }

	public void NotifyNodeSelected(MapStarNode starNode) {
        EventController.SafeInvoke(RaiseNodeSelected, starNode);
    }

	public void NotifyNodeDeselect() {
        EventController.SafeInvoke(RaiseNodeDeselected);
    }
}

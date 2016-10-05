using UnityEngine;
using System;

public class StarMapController : MonoBehaviour {
    public static StarMapController Instance = null;

	public static Action<StarMapNode> RaiseNodeSelected { get; set; }
	public static Action RaiseNodeDeselected { get; set; }

    public static void NotifyNodeSelected (StarMapNode starNode) {
        EventController.SafeInvoke(RaiseNodeSelected, starNode);
    }

	private static void NotifyNodeDeselect () {
        EventController.SafeInvoke(RaiseNodeDeselected);
    }

    void Start () {
        Instance = CommonUtil.EnablePersistance(this, gameObject);
        SelectionArea.RaiseSelectionAreaDownEvent += NotifyNodeDeselect;
    }
}

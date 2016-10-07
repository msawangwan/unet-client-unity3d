using UnityEngine;
using System;

public class StarMapController : MonoBehaviour {
    public static StarMapController Instance = null;

	public static Action<StarNode> RaiseNodeSelected { get; set; }
	public static Action RaiseNodeDeselected { get; set; }

    public static void NotifyNodeSelected (StarNode starNode) {
        EventController.InvokeSafe(RaiseNodeSelected, starNode);
    }

	private static void NotifyNodeDeselect () {
        EventController.InvokeSafe(RaiseNodeDeselected);
    }

    void Start () {
        Instance = CommonUtil.EnablePersistance(this, gameObject);
        SelectionArea.RaiseSelectionAreaDownEvent += NotifyNodeDeselect;
    }
}

using UnityEngine;

public class StarNodeManager : MonoBehaviour {
    public static StarNodeManager Instance = null;
    //public static StarNode CurrentNode = null;
    //public static StarNode TargetNode = null;

    private void OnTargetNodeSelected (StarNode starNode) {

    }

    private void Awake () {
        Instance = CommonUtil.EnablePersistance(this, gameObject);
    }

    private void Start () {
        StarMapController.RaiseNodeSelected += OnTargetNodeSelected;
    }
}

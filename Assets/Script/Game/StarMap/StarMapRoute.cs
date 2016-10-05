using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class StarMapRoute : MonoBehaviour {
    public static StarMapRoute Instance = null;

    private LineRenderer lr = null;

    void Start () {
        Instance = CommonUtil.EnablePersistance(this, gameObject);
        lr = gameObject.GameObjectComponent<LineRenderer>();
    }

    public void DrawRoute (Vector3 s, Vector3 e) {
        Vector3[] nodes = new Vector3[] { s, e };
        lr.SetPositions(nodes);
    }
}

using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class StarMapRoute : MonoBehaviour {
    private LineRenderer lr = null;

    void Start () {
        lr = gameObject.GameObjectComponent<LineRenderer>();
    }
}

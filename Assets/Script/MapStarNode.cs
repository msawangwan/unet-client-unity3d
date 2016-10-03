using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class MapStarNode : MonoBehaviour {
    class StarNode {
        public string Name = "star node";
    }

    public static List<GameObject> MapStarNodes = new List<GameObject>();
    public static bool HasSelected = false;

    public string Name { get { return starNode.Name; } set { starNode.Name = value; } }

    private StarNode starNode = null;

    void Awake () {
        starNode = new StarNode ();
        MapStarNodes.Add (gameObject);
    }

    void OnMouseDown () {
        MapStarSelector.S.SetNodeAsCurrentSelection (this);
        CameraPanController.S.CenterOnSelected (transform.position);
        HasSelected = true;
    }
}

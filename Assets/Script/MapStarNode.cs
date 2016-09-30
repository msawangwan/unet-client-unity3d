using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class MapStarNode : MonoBehaviour {
    class StarNode {
        public string Name = "star node";

        public StarNode(string name) {
            Name = name;
        }
    }

    static public List<GameObject> MapStarNodes = new List<GameObject>();
    
    System.Action Selected;

    public string Name { get { return starNode.Name; } }

    private StarNode starNode = null;

    void Start () {
        string debugName = string.Format ("star {0}", Random.Range(0, 10000));
        starNode = new StarNode (debugName);
        MapStarNodes.Add (gameObject);
    }

    void OnMouseDown () {
        MapStarSelector.s.OnStarNodeSelect (this);
    }
}

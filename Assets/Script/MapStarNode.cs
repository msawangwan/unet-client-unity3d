using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class MapStarNode : MonoBehaviour {
    static public List<GameObject> MapStarNodes = new List<GameObject>();

    void Start () {
        MapStarNodes.Add(gameObject);
    }

    void OnMouseEnter () {
        Debug.LogFormat(gameObject, "mouse entered {0}", gameObject.name);
    }
}

using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class MapStarNode : MonoBehaviour {

    public static List<GameObject> MapStarNodes = new List<GameObject>();

    public Star StarNode = null;
    public string Name { get; set; }

    void Awake () {
        MapStarNodes.Add (gameObject);
    }

    void Start () {
        StarNode = ScriptableObject.CreateInstance<Star>();
        StarNode.Name = Name;
        StarNode.Fuel = Random.Range(0, 100);
        StarNode.Oxygen = Random.Range(0, 100);
    }

    void OnMouseDown () {
        MapStarController.S.NotifyNodeSelected(this);
    }
}

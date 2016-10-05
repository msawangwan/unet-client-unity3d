using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class StarMapNode : MonoBehaviour {

    public static List<GameObject> StarMapNodes = new List<GameObject>();

    public Star StarNode = null;
    public string Name { get; set; }

    void Awake () {
        StarMapNodes.Add (gameObject);
    }

    void Start () {
        StarNode = ScriptableObject.CreateInstance<Star>();
        StarNode.Name = Name;
        StarNode.FuelSupply = Random.Range(0, 100);
        StarNode.OxygenSupply = Random.Range(0, 100);
    }

    void OnMouseDown () {
        MapStarController.NotifyNodeSelected(this);
    }
}

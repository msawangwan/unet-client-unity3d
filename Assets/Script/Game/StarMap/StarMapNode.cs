using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class StarMapNode : MonoBehaviour, Descriptor {

    public static List<GameObject> StarMapNodes = new List<GameObject>();

    public Star_deprecated_so StarNode = null;
    public string Name { get; set; }

    public string NameTextField { get; set; }
    public string DescriptionTextField { get; set; }

    void Awake () {
        StarMapNodes.Add (gameObject);
    }

    void Start () {
        StarNode = ScriptableObject.CreateInstance<Star_deprecated_so>();
        StarNode.Name = Name;
        StarNode.FuelSupply = Random.Range (0, 100);
        StarNode.OxygenSupply = Random.Range (0, 100);

        NameTextField = StarNode.Name;
        DescriptionTextField = string.Format("fuel {0} \nand air {1}", StarNode.FuelSupply, StarNode.OxygenSupply);
    }

    void OnMouseDown () {
        StarMapController.NotifyNodeSelected (this);
    }
}

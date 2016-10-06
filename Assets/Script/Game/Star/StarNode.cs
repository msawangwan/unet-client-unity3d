using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class StarNode : MonoBehaviour, Descriptor {
    public static LinkedList<StarNode> StarNodes = new LinkedList<StarNode>();
    public LinkedList<StarNode>.Node NodeLink = null;

    public string NameField { get; set; }
    public string DescriptionField { get; set; }

    void Awake () {
        NodeLink = StarNodes.Add (this);
    }

    void Start () {
        NameField = "star-node";
        DescriptionField = string.Format("description");
    }

    void OnMouseDown () {
        StarMapController.NotifyNodeSelected (this);
    }
}

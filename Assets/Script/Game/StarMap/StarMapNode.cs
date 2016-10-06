using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class StarMapNode : MonoBehaviour, Descriptor {
    public static LinkedList<StarMapNode> StarMapNodes = new LinkedList<StarMapNode>();
    public LinkedList<StarMapNode>.Node NodeLink = null;

    public string NameField { get; set; }
    public string DescriptionField { get; set; }

    void Awake () {
        NodeLink = StarMapNodes.Add (this);
    }

    void Start () {
        NameField = "star-node";
        DescriptionField = string.Format("description");
    }

    void OnMouseDown () {
        StarMapController.NotifyNodeSelected (this);
    }
}

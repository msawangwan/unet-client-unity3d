using UnityEngine;

public class DebugCollider : MonoBehaviour {
    public bool IsColliderDrawEnabled = false;
    public bool IsColliderContactDrawEnabled = false;
    public Color ContactColor = Color.red;

    void Start () {
        Physics2D.alwaysShowColliders = IsColliderDrawEnabled;
        Physics2D.showColliderContacts = IsColliderContactDrawEnabled;
        Physics2D.colliderContactColor = ContactColor;
    }
    void OnDrawGizmos () {

    }
}

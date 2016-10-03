using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SelectionArea : MonoBehaviour {
    public static SelectionArea S = null; // todo: lock pattern

    public float SafeBufferAmount = 20.0f;

    public System.Action RaiseSelectionAreaDownEvent { get; set; } // todo: send position
    public System.Action RaiseSelectionAreaUpEvent { get; set; }

    private BoxCollider2D deselectionTriggerArea = null;
    private bool isHolding = false;

    private void CalculateSelectionArea () {
        float camOrthoSize = Camera.main.orthographicSize;
        float verticalSize = camOrthoSize * Screen.width / Screen.height * 2f;
        float horizontalSize = camOrthoSize * 2f;

        deselectionTriggerArea = gameObject.GameObjectComponent<BoxCollider2D> ();
        deselectionTriggerArea.size = new Vector2 ( verticalSize + SafeBufferAmount, horizontalSize + SafeBufferAmount);

        transform.position = new Vector3 ( transform.position.x, transform.position.y, 100f );
    }

    void Awake () {
        S = this;
        DontDestroyOnLoad(gameObject);

        CalculateSelectionArea ();
    }

	void OnMouseDown () {
        EventController.SafeInvoke(RaiseSelectionAreaDownEvent);
        isHolding = true;
    }

    void OnMouseUp () {
        EventController.SafeInvoke(RaiseSelectionAreaUpEvent);
        isHolding = false;
    }
}
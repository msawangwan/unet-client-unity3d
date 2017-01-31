using UnityEngine;

namespace UnityLib {
    [RequireComponent(typeof(BoxCollider2D))]
    public class SelectionArea : MonoBehaviour {
        public static SelectionArea Instance = null;

        public float SafeBufferAmount = 20.0f;

        public static System.Action RaiseSelectionAreaDownEvent { get; set; } // todo: send position
        public static System.Action RaiseSelectionAreaUpEvent { get; set; }

        private BoxCollider2D deselectionTriggerArea = null;

        private void CalculateSelectionArea () {
            float camOrthoSize = Camera.main.orthographicSize;
            float verticalSize = camOrthoSize * Screen.width / Screen.height * 2f;
            float horizontalSize = camOrthoSize * 2f;

            deselectionTriggerArea = gameObject.GetComponentSafe<BoxCollider2D> ();
            deselectionTriggerArea.size = new Vector2 (verticalSize + SafeBufferAmount, horizontalSize + SafeBufferAmount);
        }

        void Awake () {
            CalculateSelectionArea ();
        }

        void OnMouseDown () {
            RaiseSelectionAreaDownEvent.InvokeSafe();
        }

        void OnMouseUp () {
            RaiseSelectionAreaUpEvent.InvokeSafe();
        }
    }
}
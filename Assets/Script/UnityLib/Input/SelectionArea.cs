using UnityEngine;

namespace UnityLib.Framework.Client {
    [RequireComponent(typeof(BoxCollider2D))]
    public class SelectionArea : MonoBehaviour {
        public static SelectionArea Instance = null; // todo: lock pattern

        public float SafeBufferAmount = 20.0f;

        public static System.Action RaiseSelectionAreaDownEvent { get; set; } // todo: send position
        public static System.Action RaiseSelectionAreaUpEvent { get; set; }

        private BoxCollider2D deselectionTriggerArea = null;
        // private bool isHolding = false;

        private void CalculateSelectionArea () {
            float camOrthoSize = Camera.main.orthographicSize;
            float verticalSize = camOrthoSize * Screen.width / Screen.height * 2f;
            float horizontalSize = camOrthoSize * 2f;

            // deselectionTriggerArea = gameObject.GetComponentSafe<BoxCollider2D> ();
            deselectionTriggerArea.size = new Vector2 ( verticalSize + SafeBufferAmount, horizontalSize + SafeBufferAmount);

            transform.position = new Vector3 ( transform.position.x, transform.position.y, 100f );
        }

        void Awake () {
            // Instance = CommonUtil.EnablePersistance(this, gameObject);
            CalculateSelectionArea ();
        }

        void OnMouseDown () {
            RaiseSelectionAreaDownEvent.InvokeSafe();
            // isHolding = true;
        }

        void OnMouseUp () {
            RaiseSelectionAreaUpEvent.InvokeSafe();
            // isHolding = false;
        }
    }
}
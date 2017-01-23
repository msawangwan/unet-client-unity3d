using UnityEngine;

namespace UnityLib {
    public abstract class SelectableNode : MonoBehaviour {
        private PopupController popupController;

        public System.Action Pressed;

        protected abstract void Notify();

        private void OnMouseDown() {
            CameraRigController.S.panController.CenterOnSelected(gameObject.transform.position);
            popupController.Activate(gameObject.transform.position);
            if (Pressed != null) {
                Pressed();
            }
            Notify();
        }

        private void OnEnable() {
            if (!popupController) {
                popupController = Globals.S.popupMenuController as PopupController;
            }
        }
    }
}

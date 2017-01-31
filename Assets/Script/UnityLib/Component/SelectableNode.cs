using UnityEngine;

namespace UnityLib {
    public abstract class SelectableNode : MonoBehaviour {
        private GameHUDDetailsPanelController gamehudDetailsPanel;
        // private PopupController popupController;

        public System.Action Pressed;

        public void Construct(GameHUDDetailsPanelController gamehudDetailsPanel) {
            if (this.gamehudDetailsPanel == null) {
                this.gamehudDetailsPanel = gamehudDetailsPanel;
            }
        }

        private void OnMouseDown() {
            CameraRigController.S.panController.CenterOnSelected(gameObject.transform.position);
            // popupController.Activate(gameObject.transform.position);
            if (Pressed != null) {
                Pressed();
            }
        }

        private void OnEnable() {
            // if (!popupController) {
                // popupController = Globals.S.popupMenuController as PopupController;
            // }
        }
    }
}

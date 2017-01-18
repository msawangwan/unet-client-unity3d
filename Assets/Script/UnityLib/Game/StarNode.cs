using UnityEngine;
using UnityLib.UI;

namespace UnityLib {
    public class StarNode : MonoBehaviour {
        private PopupController popupController;

        private void OnMouseDown() {
            CameraRigController.S.panController.CenterOnSelected(gameObject.transform.position);
            popupController.Activate(gameObject.transform.position);
        }

        private void OnEnable() {
            if (!popupController) {
                popupController = Globals.S.popupMenuController as PopupController;
            }
        }
    }
}

using UnityEngine;
using UnityLib.Framework.Client.Core;

namespace UnityLib.Framework.Client {
    public class StarNode : MonoBehaviour {
        private void OnMouseDown() {
            CameraRigController.S.panController.CenterOnSelected(gameObject.transform.position);
        }
    }
}

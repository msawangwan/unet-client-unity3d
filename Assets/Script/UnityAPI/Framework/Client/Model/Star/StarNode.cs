using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAPI.Framework.Client {
    public class StarNode : MonoBehaviour {
        private void OnMouseDown() {
            CameraRigController.S.panController.CenterOnSelected(gameObject.transform.position);
        }
    }
}

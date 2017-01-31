using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class GameHUDPanel : MonoBehaviour {
        public void ToggleActive(bool toOn) {
            if (toOn) {
                gameObject.SetActive(true);
            } else {
                gameObject.SetActive(false);
            }
        }

        public void ShowDetails(GameHandle.OnStarNodeSelectedCallback sel) {
        
        }
    }
}

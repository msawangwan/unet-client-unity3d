using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib.UI {
    public class PopupPanel : MonoBehaviour {
        [SerializeField] private bool rootPanel;

        public bool isRootPanel { get { return rootPanel; } }
        public bool isInitialised { get; private set; }

        public void Init() {
            isInitialised = true;
        }
    }
}

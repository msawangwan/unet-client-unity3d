using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class PopupPanel : MonoBehaviour {
        [SerializeField] private PopupInstance instance;
        [SerializeField] private bool rootPanel;

        public bool isRootPanel { get { return rootPanel; } }
        public bool isInitialised { get; private set; }

        public int subpanelIndex { get { return instance.panelID; } }

        public void Init() {
            isInitialised = true;
        }
    }
}

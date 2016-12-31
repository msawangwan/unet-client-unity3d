using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib.UI {
    public class PopupController : ControllerBehaviour {
        public PopupView PopupView;

        private Stack<PopupPanel> traversalPath = new Stack<PopupPanel>();

        private PopupPanel rootPanel;
        private PopupPanel activePanel;

        public void Activate(Vector3 center) {
            Debug.LogFormat("called activate");
            PopupView.transform.position = new Vector3(center.x + 3f, center.y, center.z);
            PopupView.gameObject.SetActive(true);
            SetActiveState(rootPanel, true);
        }

        public void Deactivate() {
            PopupView.gameObject.SetActive(false);
            while (traversalPath.Count > 0) {
                PopupPanel p = traversalPath.Pop();
                SetActiveState(p, false);
            }
        }

        protected override bool OnInit() {
            for (int i = 0; i < PopupView.transform.childCount; i++) {
                PopupPanel p = PopupView.transform.GetChild(i).GetComponent<PopupPanel>();
                if (!p.isInitialised) {
                    p.Init();
                    if (p.isRootPanel && rootPanel == null) {
                        rootPanel = p;
                    }
                }
            }
            return true;
        }

        private void SetActiveState(PopupPanel p, bool toggleOn) {
            p.gameObject.SetActive(toggleOn);
            if (toggleOn) {
                this.activePanel = p;
                traversalPath.Push(p);
            }
        }
    }
}

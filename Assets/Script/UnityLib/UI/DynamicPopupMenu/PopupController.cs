using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib.UI {
    public class PopupController : ControllerBehaviour {
        public PopupView PopupView;

        private Stack<PopupPanel> traversalPath = new Stack<PopupPanel>();
        private PopupPanel[] subpanels;

        private PopupPanel rootPanel;
        private PopupPanel activePanel;

        [SerializeField] private GameObject topLevel;
        [SerializeField] private GameObject secondLevel;
        [SerializeField] private GameObject subMenus;

        private float offset = 4.0f;

        public void Activate(Vector3 center) {
            PopupView.transform.position = new Vector3(center.x + offset, center.y, center.z);
            PopupView.gameObject.SetActive(true);
            topLevel.SetActive(true);
            secondLevel.SetActive(false);
            SetActiveState(rootPanel, true);
        }

        public void Deactivate() {
            PopupView.gameObject.SetActive(false);
            while (traversalPath.Count > 0) {
                PopupPanel p = traversalPath.Pop();
                SetActiveState(p, false);
            }
        }

        public void DownOneLevel(int id) {
            PopupPanel p = null;
            if (id == -1) {
                p = rootPanel;
            } else {
                p = subpanels[id];
                topLevel.SetActive(false);
                secondLevel.SetActive(true);
            }
            ChangeViewTo(p);
        }

        public void UpOneLevel() {
            if (traversalPath.Count > 0) {
                PopupPanel p = traversalPath.Pop();

                if (p == activePanel) {
                    p = traversalPath.Pop();
                }

                if (p.isRootPanel) {
                    secondLevel.SetActive(false);
                    topLevel.SetActive(true);
                }
                
                ChangeViewTo(p);
            }
        }

        protected override bool OnInit() {
            if (rootPanel == null) {
                PopupPanel p = topLevel.GetComponent<PopupPanel>();
                if (p) {
                    if (!p.isInitialised) {
                        p.Init();
                        if (p.isRootPanel) {
                            rootPanel = p;
                        }
                    }
                }
            }

            int nSubmenus = subMenus.transform.childCount;
            subpanels = new PopupPanel[nSubmenus];

            for (int i = 0; i < nSubmenus; i++) {
                PopupPanel p = subMenus.transform.GetChild(i).GetComponent<PopupPanel>();
                if (p) {
                    if (!p.isInitialised) {
                        p.Init();
                        subpanels[p.subpanelIndex] = p;
                    }
                }
            }

            PopupView.Init();

            return true;
        }

        private void ChangeViewTo(PopupPanel p) {
            SetActiveState(activePanel, false);
            SetActiveState(p, true);
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

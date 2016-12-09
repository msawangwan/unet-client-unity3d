using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.UI.Manager {
    public abstract class MenuManager : ManagerBehaviour {
        protected Dictionary<int, GameObject> panels = new Dictionary<int, GameObject>();
        protected System.Action<int, GameObject> onPanelRegistered = null;

        public void RegisterWithManager(GameObject panel, int panelIID) {
            if (!panel) {
                return;
            } else {
                if (!panels.ContainsKey(panelIID)) {
                    panels.Add(panelIID, panel);
                    Debug.Log("added to panels: " + panelIID);

                    if (onPanelRegistered != null) {
                        Debug.Log("raised listener: " + panelIID);
                        onPanelRegistered(panelIID, panel);
                    }
                }
            }
        }

        protected override bool HandleInitialisation() {
            return true;
        }
    }
}

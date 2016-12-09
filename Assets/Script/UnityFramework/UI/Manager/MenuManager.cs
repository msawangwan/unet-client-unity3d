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

                    if (onPanelRegistered != null) {
                        onPanelRegistered(panelIID, panel);
                    }
                }
            }

            Debug.LogFormat("[{0}] MenuManager registered: {1} {2}", Time.time, panel.name, panelIID);
        }

        protected override bool HandleInitialisation() {
            return true;
        }
    }
}

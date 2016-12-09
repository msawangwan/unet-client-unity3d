using UnityEngine;
using UnityFramework.UI.Manager;
using System.Collections;
using System.Collections.Generic;

namespace UnityFramework.UI.Model {
    public class HomeMenuManager : MenuManager {
        private MenuPanel currentActivePanel = null;
        
        private void PanelHandler(int panelIID, GameObject panel) {
            MenuPanel p = panel.GetComponent<MenuPanel>();
            if (p) {
                if (p.isDefaultView) {
                    panel.gameObject.SetActive(true);
                }
            }
        }

        private void OnEnable() {
            base.onPanelRegistered += PanelHandler;
        }

        // private void Update() {

        // }

        private void OnTransformChildrenChanged() {
            Debug.Log("transform children changed");
        }

        private void OnDisable() {
            base.onPanelRegistered -= PanelHandler;
        }
    }
}

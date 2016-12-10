﻿using UnityEngine;
using UnityFramework.UI.Model;
using System.Collections.Generic;

namespace UnityFramework.UI.Manager {
    public class HomeMenuManager : MenuManager {
        private Stack<MenuPanel> menuPath = new Stack<MenuPanel>();
        private MenuPanel currentActivePanel = null;

        public void DownOneLevel(MenuPanel mp, int id) {
            menuPath.Push(mp);
        }

        public void UpOneLevel(MenuPanel mp, int id) {

        }
        
        private void HandleOnMenuLoaded(int panelIID, GameObject panel) {
            MenuPanel p = panel.GetComponent<MenuPanel>();
            if (p) {
                if (currentActivePanel == null) {
                    currentActivePanel = p;
                    menuPath.Push(currentActivePanel);
                } else if (p.isRootMenu) { // technically, this could be more than 1 panel if we forget to set it
                    currentActivePanel.gameObject.SetActive(false);
                    currentActivePanel = p;

                    panel.gameObject.SetActive(true);

                    menuPath.Pop();
                    menuPath.Push(currentActivePanel); // should always be the 'default' panel of a menu set
                } else {
                    panel.gameObject.SetActive(false);
                }

                p.MapUIDependencies();
            }
        }

        private void OnEnable() {
            base.onMenuRegistered += HandleOnMenuLoaded;

            for (int i = 0; i < transform.childCount; i++) { // register each panel in the set
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        private void OnTransformChildrenChanged() {
            Debug.Log("transform children changed");
        }

        private void OnDisable() {
            base.onMenuRegistered -= HandleOnMenuLoaded;
        }
    }
}

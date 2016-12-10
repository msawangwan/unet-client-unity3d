using UnityEngine;
using UnityFramework.UI.Model;
using System.Collections.Generic;

namespace UnityFramework.UI.Manager {
    public class HomeMenuManager : MenuManager<HomeMenuManager> {
        // private Stack<MenuPanel<HomeMenuManager>> menuPath = new Stack<MenuPanel<HomeMenuManager>>();
        private MenuPanel<HomeMenuManager> currentActivePanel = null;

        public void DownOneLevel(MenuPanel<HomeMenuManager> mp, int id) {

        }

        public void UpOneLevel(MenuPanel<HomeMenuManager> mp, int id) {

        }

        protected override int menuCount { 
            get { 
                return 3;
            }
        }

        private void HandleOnMenuLoaded(int panelIID, GameObject menuGameObject) {
            var menu = menuGameObject.GetComponent<MenuPanel<HomeMenuManager>>();

            if (menu) {
                if (menu.isRootMenu) {
                    Debug.LogFormat("set active: {0}", true);
                    menuGameObject.SetActive(true);
                } else {
                    Debug.LogFormat("set active: {0}", false);
                    menuGameObject.SetActive(false);
                }
                menu.MapUIDependencies();
            }
        }

        private void OnEnable() {
            base.onMenuCached += HandleOnMenuLoaded;

            for (int i = 0; i < transform.childCount; i++) { // register each panel in the set
                Debug.LogFormat("enable menu #{0}", i);
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        private void OnDisable() {
            base.onMenuCached -= HandleOnMenuLoaded;
        }
    }
}

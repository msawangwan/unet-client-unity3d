using UnityEngine;
using UnityFramework.UI.Model;
using System.Collections.Generic;

namespace UnityFramework.UI.Manager {
    public class HomeMenuManager : MenuManager<HomeMenuManager> {
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

        protected override bool isRootSet { get; set; }

        private void HandleOnMenuLoaded(int panelIID, GameObject menuGameObject) {
            var menu = menuGameObject.GetComponent<MenuPanel<HomeMenuManager>>();

            if (menu) {
                if (menu.isRootMenu && !isRootSet) {
                    menuGameObject.SetActive(true);
                    isRootSet = true;
                } else {
                    menuGameObject.SetActive(false);
                }
                menu.MapUIDependencies();
            }
        }

        private void OnEnable() {
            base.onMenuCached += HandleOnMenuLoaded;

            for (int i = 0; i < transform.childCount; i++) { // register each panel in the set
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        private void OnDisable() {
            isRootSet = false;
            base.onMenuCached -= HandleOnMenuLoaded;
        }
    }
}

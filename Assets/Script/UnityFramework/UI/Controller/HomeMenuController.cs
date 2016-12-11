using UnityEngine;
using UnityAPI.UI.Model;
using System.Collections.Generic;

namespace UnityAPI.UI.Manager {
    public class HomeMenuController : MenuController<HomeMenuController> {
        private MenuPanel<HomeMenuController> currentActivePanel = null;

        public void DownOneLevel(MenuPanel<HomeMenuController> mp, int id) {

        }

        public void UpOneLevel(MenuPanel<HomeMenuController> mp, int id) {

        }

        protected override int menuCount { 
            get {
                return 3;
            }
        }

        protected override bool isRootSet { get; set; }

        private void HandleOnMenuLoaded(int panelIID, GameObject menuGameObject) {
            var menu = menuGameObject.GetComponent<MenuPanel<HomeMenuController>>();

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
                // register

            }
        }

        private void OnDisable() {
            isRootSet = false;
            base.onMenuCached -= HandleOnMenuLoaded;
        }
    }
}

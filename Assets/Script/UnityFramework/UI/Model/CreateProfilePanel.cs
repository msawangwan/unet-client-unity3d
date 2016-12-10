using UnityEngine;
using UnityEngine.UI;
using UnityFramework.UI.Manager;
using System.Collections;

namespace UnityFramework.UI.Model {
    public class CreateProfilePanel : MenuPanel<HomeMenuManager> {
        private HomeMenuManager homeMenu = null;

        public override bool isRootMenu { get { return true; } }
        protected override int submenuCount { get { return 1; } }

        public override void MapUIDependencies() {
            Debug.Log("create profile runned settup");
        }

        protected override void MapParentMenu(MenuManager<HomeMenuManager> parentMenu) {
            homeMenu = parentMenu as HomeMenuManager;
        }

        protected override IEnumerator onLoadMapSubMenus() {
            while (true) {
                yield return null;
            }
        }
    }
}
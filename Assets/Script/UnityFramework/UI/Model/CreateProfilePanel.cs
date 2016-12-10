using UnityEngine;
using UnityEngine.UI;
using UnityFramework.UI.Manager;
using System.Collections;

namespace UnityFramework.UI.Model {
    public class CreateProfilePanel : MenuPanel<HomeMenuController> {
        private HomeMenuController homeMenu = null;

        public override bool isRootMenu { get { return false; } }
        protected override int submenuCount { get { return 1; } }

        public override void MapUIDependencies() {
            Debug.LogFormat("map dependencies: {0}", gameObject.name);
        }

        protected override void MapParentMenu(MenuController<HomeMenuController> parentMenu) {
            homeMenu = parentMenu as HomeMenuController;
        }

        protected override IEnumerator onLoadMapSubMenus() {
            while (true) {
                yield return null;
            }
        }
    }
}
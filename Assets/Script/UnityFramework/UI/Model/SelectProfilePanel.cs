using UnityEngine;
using UnityEngine.UI;
using UnityFramework.UI.Manager;
using System.Collections;

namespace UnityFramework.UI.Model {
    public class SelectProfilePanel : MenuPanel<HomeMenuManager> {
        private HomeMenuManager homeMenu = null;

        public override bool isRootMenu { get { return false; } }
        protected override int submenuCount { get { return 1; } }

        public override void MapUIDependencies() {
            Debug.LogFormat("map dependencies: {0}", gameObject.name);
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

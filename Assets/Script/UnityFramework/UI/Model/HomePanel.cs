using UnityEngine;
using UnityEngine.UI;
using UnityAPI.UI.Manager;
using System.Collections;

namespace UnityAPI.UI.Model {
    public class HomePanel : MenuPanel<HomeMenuController> {
        [SerializeField] private Button NewGameButton = null;
        [SerializeField] private Button LoadGameButton = null;

        private HomeMenuController homeMenu = null;

        public override bool isRootMenu { get { return true; } }
        protected override int submenuCount { get { return 2; } }

        public override void MapUIDependencies() {
            NewGameButton.onClick.RemoveAllListeners();
            NewGameButton.onClick.AddListener(
                () => {
                    homeMenu.DownOneLevel(this, base.instanceID);
                }
            );

            LoadGameButton.onClick.RemoveAllListeners();
            LoadGameButton.onClick.AddListener(
                () => {
                    homeMenu.DownOneLevel(this, base.instanceID);
                }
            );

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

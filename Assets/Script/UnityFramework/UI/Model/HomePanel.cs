using UnityEngine;
using UnityEngine.UI;
using UnityFramework.UI.Manager;
using System.Collections;

namespace UnityFramework.UI.Model {
    public class HomePanel : MenuPanel<HomeMenuManager> {
        [SerializeField] private Button NewGameButton = null;
        [SerializeField] private Button LoadGameButton = null;

        private HomeMenuManager homeMenu = null;

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

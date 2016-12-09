using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityFramework.UI.Manager;

namespace UnityFramework.UI.Model {
    public class HomePanel : MenuPanel {
        [SerializeField] private Button NewGameButton = null;
        [SerializeField] private Button LoadGameButton = null;

        private HomeMenuManager homeMenu = null;

        public override bool isDefaultView { get { return true; } }

        public override void MapDependencies() {
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

        protected override void Start() {
            base.Start();

            if (!homeMenu) {
                homeMenu = base.menu.gameObject.GetComponent<HomeMenuManager>();
            }
        }
    }
}

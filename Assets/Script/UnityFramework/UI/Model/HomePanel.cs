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

        public override void RunSetup() {
            Debug.LogFormat("mapped buttons {0}", gameObject.name);
            MapButtons();
        }

        protected override void Start() {
            base.Start();

            if (!homeMenu) {
                homeMenu = base.menu.gameObject.GetComponent<HomeMenuManager>();
            }
        }

        private void MapButtons() {
            NewGameButton.onClick.RemoveAllListeners();
            NewGameButton.onClick.AddListener(
                () => {
                    Debug.LogFormat("new game btn pressed");
                    homeMenu.DownOneLevel(this, base.instanceID);
                }
            );

            LoadGameButton.onClick.RemoveAllListeners();
            LoadGameButton.onClick.AddListener(
                () => {
                    Debug.LogFormat("load game btn pessed");
                    homeMenu.DownOneLevel(this, base.instanceID);
                }
            );
        }
    }
}

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

        protected override void Start() {
            base.Start();

            if (!homeMenu) {
                Debug.Log("get home menu");
                homeMenu = base.menu.gameObject.GetComponent<HomeMenuManager>();
                if (homeMenu) {
                    Debug.Log("got home menu");
                }
            }
        }

        private void OnEnable() {
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

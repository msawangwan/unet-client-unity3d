using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityAPI.Framework.UI {
    public class MenuView : MonoBehaviour {
        public MenuController controller;

        public Button newProfile;
        public Button confirmProfile;

        public Button selectProfile;
        public Button displayProfile;
        public Button loadProfile;

        public Button upOneLevel;

        public void Init() {
            Button[] allButtons = new Button[] {
                newProfile,
                confirmProfile,
                selectProfile,
                displayProfile,
                loadProfile,
                upOneLevel,
            };
            
            foreach (var item in allButtons) {
                if (item != null) {
                    item.onClick.RemoveAllListeners();
                }
            }

            newProfile.onClick.AddListener(
                () => {
                    controller.DownOneLevel(0, 0); // get the menu with key 0 and the link at points index 0
                }
            );

            confirmProfile.onClick.AddListener(
                () => {
                    Debug.Log("starting new game");
                }
            );

            selectProfile.onClick.AddListener(
                () => {
                    controller.DownOneLevel(1, 0);
                }
            );
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class UIManager : MonoBehaviour {
        [SerializeField] MainMenuController mainMenuController;
        [SerializeField] GameHUDController gameHUDController;
        [SerializeField] GameHUDDetailsPanelController gameHUDDetailsController;

        public MainMenuController MainMenu {
            get {
                return mainMenuController;
            }
        }

        public GameHUDController GameHUD {
            get {
                return gameHUDController;
            }
        }

        public GameHUDDetailsPanelController GameDetailPanel {
            get {
                return gameHUDDetailsController;
            }
        }
    }
}

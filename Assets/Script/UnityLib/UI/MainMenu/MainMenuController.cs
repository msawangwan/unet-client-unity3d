using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityLib {
    public class MainMenuController : MonoBehaviour {
        [SerializeField] GameHUDController gameHUDController;

        [SerializeField] MainMenuView view;
        [SerializeField] MainMenuLevel[] levels;
        [SerializeField] MainMenuPanel newSession;
        [SerializeField] MainMenuPanel findSession;

        [SerializeField] GameObject lobbyList;
        [SerializeField] GameObject lobbyListingPrefab;

        private MainMenuLevel currentLevel;

        private string currentPlayerName;
        private string currentSessionName;

        public GameHUDController GameHUDCtrl {
            get {
                return gameHUDController;
            }
        }

        public void ShowConfirmation(GameObject panel, Button button, Action action) {
            if (action != null) {
                if (button != null) {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(
                        () => { 
                            action();
                            panel.SetActive(false);
                        }
                    );
                    panel.SetActive(true);
                } else {
                    action();
                }
            }
        }

        public MainMenuLevel SwitchLevel(int next) {
            currentLevel.gameObject.SetActive(false);

            if (next == -1) {
                ViewOff();
                return currentLevel;
            }

            currentLevel = levels[next];
            currentLevel.gameObject.SetActive(true);

            return currentLevel;
        }

        public void ReturnToMainMenu() {
            SwitchLevel(1);
        }

        public void Cancel() {
            levels[3].gameObject.SetActive(false);
        }

        private void ViewOff() {
            view.gameObject.SetActive(false);
        }
        
        private IEnumerator Start() {
            do {
                yield return null;
            } while (Globals.S.AppState != Globals.ApplicationState.Menu); {
                view.gameObject.SetActive(true);
            }

            foreach (var l in levels) {
                if (l.levelIndex == 0) {
                    l.gameObject.SetActive(true);
                } else {
                    l.gameObject.SetActive(false);
                }
            }

            currentLevel = levels[0];

            view.Init();
        }
    }
}
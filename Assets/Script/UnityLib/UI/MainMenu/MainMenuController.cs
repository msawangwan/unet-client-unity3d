using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityLib.UI {
    public class MainMenuController : MonoBehaviour {
        [SerializeField] MainMenuView view;
        [SerializeField] MainMenuLevel[] levels;
        [SerializeField] MainMenuPanel newSession;
        [SerializeField] MainMenuPanel findSession;
        [SerializeField] GameObject lobbyList;

        [SerializeField] GameObject lobbyListingPrefab;

        private MainMenuLevel currentLevel;

        private string currentPlayerName;
        private string currentSessionName;

        public SessionHandle session { get; set; }
        public string SessionName { get; set; }

        public string NameChoice { get; set; }

        // public IEnumerator Host() {
        //     bool isHostNameAvailable = false;

        //     Action<bool> onCheck = (result) => { isHostNameAvailable = result; };

        //     do {
        //         yield return session.CheckSessionAvailability(SessionName, onCheck);
        //     } while (!isHostNameAvailable);

        //     float start = Time.time;
        //     bool wg = false;

        //     Action onSuccess = () => { wg = true; };

        //     do {
        //         yield return session.HostGame(onSuccess);

        //         Debug.LogFormat("-- -- [*] hosting game [{0}] ...", Time.time);

        //         if (wg) {
        //             Debug.LogFormat("-- -- -- [*] success [{0}] ...", Time.time);
        //             break;
        //         }
        //     } while (true);
            
        //     Debug.LogFormat("-- -- [*] done (took {1} seconds) [{0}] ...", Time.time, (Time.time - start));
        // }


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
        
        private void Start() {
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
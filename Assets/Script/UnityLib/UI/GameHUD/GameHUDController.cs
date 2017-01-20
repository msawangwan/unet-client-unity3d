using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class GameHUDController : MonoBehaviour {
        [SerializeField] MainMenuController mainMenuController;
    
        [SerializeField] GameHUDView view;

        public MainMenuController MainMenuCtrl {
            get {
                return mainMenuController;
            }
        }

        public int GameKey { get; set; }
        public string OpponentName { get; set; }

        private IEnumerator Start() {
            view.gameObject.SetActive(false);

            yield return new WaitUntil(
                ()=> {
                    if (Globals.S.AppState == Globals.ApplicationState.Game) {
                        return true;
                    }
                    return false;
                }
            );

            Debug.LogWarningFormat("[+] entered application state [game], activating gamehudcontroller ... [{0}]", Time.time);

            view.gameObject.SetActive(true);
        }
    }
}

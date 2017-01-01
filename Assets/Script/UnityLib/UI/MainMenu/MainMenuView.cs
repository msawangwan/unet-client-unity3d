using UnityEngine;
using UnityEngine.UI;

namespace UnityLib.UI {
    public class MainMenuView : MonoBehaviour {
        [SerializeField] private MainMenuController mainMenuController;

        [SerializeField] private InputField playerNameInputField;
        [SerializeField] private InputField sessionNameInputField;

        [SerializeField] private Button confirmPlayerName;
        [SerializeField] private Button newSession;
        [SerializeField] private Button findSession;
        [SerializeField] private Button toMainMenu;
        [SerializeField] private Button createSession;
        [SerializeField] private Button cancel;
        [SerializeField] private Button confirm;

        [SerializeField] private Text headerConfirm;
        [SerializeField] private Text inputToConfirm;

        [SerializeField] private GameObject lobbyList;

        private string playername;
        private string sessionname;

        public void Init() {
            Button[] buttons = new Button[] {
                confirmPlayerName,
                newSession,
                findSession,
                toMainMenu,
                createSession,
                cancel,
                confirm,
            };

            foreach (var b in buttons) {
                b.onClick.RemoveAllListeners();
            }

            confirmPlayerName.onClick.AddListener(
                () => {
                    playername = playerNameInputField.text;
                    headerConfirm.text = "are you sure you want the player name:";
                    inputToConfirm.text = playername;
                    mainMenuController.ChooseName(playername);
                }
            );

            newSession.onClick.AddListener(
                () => {
                    createSession.gameObject.SetActive(true);
                    mainMenuController.NewSession();
                }
            );

            findSession.onClick.AddListener(
                () => {
                    createSession.gameObject.SetActive(false);
                    mainMenuController.FindSession();
                }
            );

            cancel.onClick.AddListener(
                () => {
                    mainMenuController.Cancel();
                }
            );

            confirm.onClick.AddListener(
                () => {
                    mainMenuController.Confirm();
                }
            );

            createSession.onClick.AddListener(
                () => {
                    sessionname = sessionNameInputField.text;
                    headerConfirm.text = "create session with name:";
                    inputToConfirm.text = sessionname;
                    mainMenuController.CreateGame(sessionname);
                }
            );

            toMainMenu.onClick.AddListener(
                () => {
                    mainMenuController.ReturnToMainMenu();
                }
            );
        }
    }
}

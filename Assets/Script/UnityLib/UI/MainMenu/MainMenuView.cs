using System;
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

        [SerializeField] private GameObject lobbyListingPrefab;

        [SerializeField] private GameObject newSessionPanel;
        [SerializeField] private GameObject findSessionPanel;
        [SerializeField] private GameObject confirmationPanel;
        [SerializeField] private GameObject lobbyList;

        private MainMenuLevel currentLevel;

        private string playername;
        private string sessionname;

        private SessionHandle sessionHandle;

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

            newSession.onClick.AddListener(
                () => {
                    createSession.gameObject.SetActive(true);

                    findSessionPanel.SetActive(false);
                    newSessionPanel.SetActive(true);

                    currentLevel = mainMenuController.SwitchLevel(2);
                }
            );

            findSession.onClick.AddListener(
                () => {
                    createSession.gameObject.SetActive(false);

                    newSessionPanel.SetActive(false);
                    findSessionPanel.SetActive(true);

                    currentLevel = mainMenuController.SwitchLevel(2);

                    Action<string[]> onFetch = (listing) => {
                        foreach (string item in listing) {
                            GameObject go = Instantiate(lobbyListingPrefab, Vector3.zero, Quaternion.identity, lobbyList.transform);
                            go.GetComponentInChildren<Text>().text = item;
                            go.GetComponent<Button>().onClick.RemoveAllListeners();
                            go.GetComponent<Button>().onClick.AddListener(
                                () => {
                                    currentLevel.gameObject.SetActive(false);
                                    StartCoroutine(sessionHandle.Join(item));
                                }
                            );
                        }
                    };

                    StartCoroutine(sessionHandle.FetchLobbyList(onFetch));
                }
            );

            confirmPlayerName.onClick.AddListener(
                () => {
                    playername = playerNameInputField.text;

                    headerConfirm.text = "are you sure you want the player name:";
                    inputToConfirm.text = playername;

                    sessionHandle = SessionHandle.New(playername);
                    mainMenuController.session = sessionHandle;

                    mainMenuController.ShowConfirmation(
                        confirmationPanel,
                        confirm,
                        () => {
                            Debug.LogFormat("[+] starting a new session ... [{0}]", Time.time);
                            currentLevel = mainMenuController.SwitchLevel(1);
                            sessionHandle.StartCoroutine(sessionHandle.Register(playername));
                        }
                    );
                }
            );

            createSession.onClick.AddListener(
                () => {
                    sessionname = sessionNameInputField.text;

                    headerConfirm.text = "create session with name:";
                    inputToConfirm.text = sessionname;

                    mainMenuController.ShowConfirmation(
                        confirmationPanel,
                        confirm,
                        () => {
                            Debug.LogFormat("[+] creating a new game as host ... [{0}]", Time.time);
                            currentLevel = mainMenuController.SwitchLevel(-1);
                            sessionHandle.StartCoroutine(sessionHandle.CreateHostSession(sessionname));
                        }
                    );
                }
            );

            cancel.onClick.AddListener(
                () => {
                    mainMenuController.Cancel();
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

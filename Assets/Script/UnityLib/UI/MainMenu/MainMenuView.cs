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
        [SerializeField] private Button confirmButton;

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

        private ClientHandle clientHandle;
        private SessionHandle sessionHandle;
        private GameHandle gameHandle;

        public void Init() {
            Button[] buttons = new Button[] {
                confirmPlayerName,
                newSession,
                findSession,
                toMainMenu,
                createSession,
                cancel,
                confirmButton,
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

                    StartCoroutine(clientHandle.RequestHostKey(
                        () => {
                            if (sessionHandle == null) { // TODO: do better when we come around a second time rather than just null check
                                Debug.LogFormat("-- [+] registered a host session handle ... [{0}]", Time.time);
                                sessionHandle = SessionHandle.New(clientHandle.SessionKey);
                            } else {
                                Debug.LogFormat("-- [+] already registered a host session handle (must be coming around again) ... [{0}]", Time.time);
                            }
                        }
                    ));
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
                                    // StartCoroutine(sessionHandle.Join(item));
                                }
                            );
                        }
                    };

                    // StartCoroutine(sessionHandle.FetchLobbyList(onFetch));

                    StartCoroutine(clientHandle.RequestJoinKey(onFetch));
                }
            );

            confirmPlayerName.onClick.AddListener(
                () => {
                    playername = playerNameInputField.text;

                    headerConfirm.text = "confirm player name:";
                    inputToConfirm.text = playername;

                    mainMenuController.ShowConfirmation(
                        confirmationPanel,
                        confirmButton,
                        () => {
                            Debug.LogFormat("[+] registering client handler with server ... [{0}]", Time.time);

                            currentLevel = mainMenuController.SwitchLevel(1);
                            clientHandle = ClientHandle.New(playername);

                            StartCoroutine(clientHandle.Register(null));
                        }
                    );
                }
            );

            createSession.onClick.AddListener(
                () => {
                    bool hostNameIsValid = false;
                    sessionname = sessionNameInputField.text;

                    headerConfirm.text = "create session with name:";
                    inputToConfirm.text = sessionname;

                    Action<bool> onAvailability = (isAvailable) => {
                        hostNameIsValid = isAvailable;
                    };

                    StartCoroutine(sessionHandle.VerifyName(sessionname, onAvailability));

                    mainMenuController.ShowConfirmation(
                        confirmationPanel,
                        confirmButton,
                        () => {
                            Debug.LogFormat("[+] request sent to server, host game ... [{0}]", Time.time);

                            if (hostNameIsValid) {
                                Debug.LogWarningFormat("-- [+] {0} unique name: {1} ... [{2}]", sessionname, hostNameIsValid, Time.time);
                                currentLevel = mainMenuController.SwitchLevel(-1);

                                Debug.LogWarning("-- -- -- [+] spwned game seession");

                                gameHandle = GameHandle.New(sessionname, true);
                                StartCoroutine(sessionHandle.LoadGameHandler(gameHandle, null));
                            } else {
                                Debug.LogErrorFormat("-- [--] {0} unique name: {1} ... [{2}]", sessionname, hostNameIsValid, Time.time);
                                currentLevel = mainMenuController.SwitchLevel(1);
                            }
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

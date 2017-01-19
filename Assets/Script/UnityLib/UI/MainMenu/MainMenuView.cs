using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityLib {
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
        private string gamename;

        private ClientHandle clientHandle;
        private SessionHandle sessionHandle;
        private LobbyHandle lobbyHandle;
        private GameHandle gameHandle;
        private PollHandle pollHandle;

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

            Action getSessionkey = () => {
                if (sessionHandle == null) { // WARNING: is this robust enough?
                    Debug.LogFormat("-- [+] registered a host session handle ... [{0}]", Time.time);
                    sessionHandle = SessionHandle.New(clientHandle.SessionKey);
                } else {
                    Debug.LogFormat("-- [+] already registered a host session handle (must be coming around again) ... [{0}]", Time.time);
                }
            };

            Action onContextChanged = () => { // WARNING: potential bugs from this, will kill all coroutines running on the mainmenuview
                Debug.LogFormat("[+] cleanup all menu routines [{0}]", Time.time);
                currentLevel = mainMenuController.SwitchLevel(-1); 
            };

            Action onGameLoadCompleted = () => {
                Debug.LogFormat("[+] started polling for game start ... [{0}]", Time.time);
                currentLevel.gameObject.SetActive(false);
                StartCoroutine(mainMenuController.GameHUDCtrl.BeginWaitAndPollGameStart(
                    () => {
                        StartCoroutine(pollHandle.WaitForGameStart(gameHandle.GameKey, onContextChanged)); // maybe this should start in the gamehudcontroller
                    }
                ));

                // StartCoroutine(pollHandle.WaitForGameStart(gameHandle.GameKey, onContextChanged)); // maybe this should start in the gamehudcontroller
            };
    
            newSession.onClick.AddListener(
                () => {
                    createSession.gameObject.SetActive(true);

                    findSessionPanel.SetActive(false);
                    newSessionPanel.SetActive(true);

                    currentLevel = mainMenuController.SwitchLevel(2);

                    getSessionkey();

                    StartCoroutine(clientHandle.RequestSessionKey(null));
                }
            );

            findSession.onClick.AddListener(
                () => {
                    createSession.gameObject.SetActive(false);

                    newSessionPanel.SetActive(false);
                    findSessionPanel.SetActive(true);

                    currentLevel = mainMenuController.SwitchLevel(2);

                    getSessionkey();

                    if (lobbyHandle == null) {
                        lobbyHandle = new LobbyHandle();
                    }

                    Action<string[]> onFetch = (listing) => {
                        foreach (string gamenamestr in listing) {
                            GameObject go = Instantiate(lobbyListingPrefab, Vector3.zero, Quaternion.identity, lobbyList.transform);
                            go.GetComponentInChildren<Text>().text = gamenamestr;
                            go.GetComponent<Button>().onClick.RemoveAllListeners();
                            go.GetComponent<Button>().onClick.AddListener(
                                () => {
                                    Debug.LogFormat("-- -- -- [+] extracted button text [gamename: {0}] ... [{1}]", gamenamestr, Time.time);

                                    gamename = gamenamestr;
                                    
                                    gameHandle = GameHandle.New(gamename, false);
                                    pollHandle = PollHandle.New();

                                    Action loadAsClientThenJoin = () => {
                                        StartCoroutine(gameHandle.SendClientGameParameters(
                                            () => {
                                                StartCoroutine(gameHandle.Join(clientHandle.ClientName, onGameLoadCompleted));
                                            }
                                        ));
                                    };

                                    StartCoroutine(sessionHandle.StartClientSession(gameHandle, loadAsClientThenJoin));
                                }
                            );
                        }
                    };

                    StartCoroutine(clientHandle.RequestSessionKey(
                        () => {
                            StartCoroutine(lobbyHandle.FetchGameList(onFetch));
                        }
                    ));
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
                    gamename = sessionNameInputField.text;

                    headerConfirm.text = "create session with name:";
                    inputToConfirm.text = gamename;

                    Action<bool> onAvailability = (isAvailable) => {
                        hostNameIsValid = isAvailable;
                    };

                    StartCoroutine(sessionHandle.VerifyName(gamename, onAvailability));

                    mainMenuController.ShowConfirmation(
                        confirmationPanel,
                        confirmButton,
                        () => {
                            Debug.LogFormat("[+] request sent to server, host game ... [{0}]", Time.time);

                            if (hostNameIsValid) {
                                Debug.LogWarningFormat("-- [+] {0} unique name: {1} ... [{2}]", gamename, hostNameIsValid, Time.time);

                                Debug.LogWarning("-- -- -- [+] spwned game session");

                                gameHandle = GameHandle.New(gamename, true);
                                pollHandle = PollHandle.New();

                                Action loadAsHostThenJoin = () => {
                                    StartCoroutine(gameHandle.SendHostGameParameters(
                                        () => {
                                            StartCoroutine(gameHandle.Join(clientHandle.ClientName, onGameLoadCompleted));
                                        }
                                    ));
                                };

                                StartCoroutine(sessionHandle.StartHostSession(gameHandle, loadAsHostThenJoin));
                            } else {
                                Debug.LogErrorFormat("-- [--] {0} unique name: {1} ... [{2}]", gamename, hostNameIsValid, Time.time);
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

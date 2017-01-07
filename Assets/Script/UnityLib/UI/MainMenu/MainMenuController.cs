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

        private SessionHandle session;

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

        public void ChooseName(string playerName) {
            currentPlayerName = playerName; 
            levels[3].gameObject.SetActive(true);
        }

        public void CreateGame(string sessionName) {
            currentSessionName = sessionName;
            session.sessionNameAvailabilityCheck.Enqueue(
                (isAvailable) => {
                    if (isAvailable) {
                        levels[3].gameObject.SetActive(true);
                    } else {
                        Debug.LogError("choose another name");
                    }
                }
            );

            StartCoroutine(session.CheckSessionAvailability(sessionName));
        }

        public void NewSession() {
            SwitchLevel(2);
            findSession.gameObject.SetActive(false);
            newSession.gameObject.SetActive(true);
        }

        public void FindSession() {
            SwitchLevel(2);
            newSession.gameObject.SetActive(false);
            findSession.gameObject.SetActive(true);
        
            session.executeOnListFetched.Enqueue(
                (listing) => {
                    foreach (var item in listing) {
                        GameObject go = Instantiate(lobbyListingPrefab, Vector3.zero, Quaternion.identity, lobbyList.transform);
                        go.GetComponentInChildren<Text>().text = item;
                        go.GetComponent<Button>().onClick.RemoveAllListeners();
                        go.GetComponent<Button>().onClick.AddListener(
                            () => {
                                currentLevel.gameObject.SetActive(false);
                                StartCoroutine(session.Join(item));
                            }
                        );
                    }
                }
            );

            StartCoroutine(session.FetchSessionList());
        }

        public void ReturnToMainMenu() {
            SwitchLevel(1);
        }

        // this is here for future refactoring, deprecate the zero paramter version
        public void Confirm(System.Action onConfirm) {
            if (onConfirm != null) {
                onConfirm();
            }

            if (currentLevel.levelIndex == 0) {
                SwitchLevel(1);
            } else if (currentLevel.levelIndex == 1) {
                SwitchLevel(2);
            } else if (currentLevel.levelIndex == 2) {
                SwitchLevel(-1);
            }

            levels[3].gameObject.SetActive(false);
        }

        public void Confirm() {
            if (currentLevel.levelIndex == 0) {
                SwitchLevel(1);
                session = SessionHandle.New(currentPlayerName);
                session.StartCoroutine(session.Register());
            } else if (currentLevel.levelIndex == 1) {
                SwitchLevel(2);
            } else if (currentLevel.levelIndex == 2) {
                SwitchLevel(-1);
                StartCoroutine(session.Create(currentSessionName));
            }

            levels[3].gameObject.SetActive(false);
        }

        public void Cancel() {
            levels[3].gameObject.SetActive(false);
        }

        private void SwitchLevel(int next) {
            currentLevel.gameObject.SetActive(false);
            if (next == -1) {
                return;
            }
            currentLevel = levels[next];
            currentLevel.gameObject.SetActive(true);
        }
    }
}
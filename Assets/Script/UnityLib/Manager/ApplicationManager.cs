using System.Collections;
using UnityEngine;

namespace UnityLib {
    public class ApplicationManager : MonoBehaviour {
        public enum GlobalState : byte {
            None = 0,
            Menu = 1,
            Game = 2,
        }

        private const ApplicationManager.GlobalState nullState = ApplicationManager.GlobalState.None;
        private const float pollIntervalInSeconds = 2.5f;

        [SerializeField] private UnityEngine.UI.Text debug_label_applicationState;

        private ApplicationManager.GlobalState applicationState = nullState;

        public delegate void OnMenuEntered();
        public delegate void OnGameEntered();

        public OnMenuEntered RaiseMenuEnteredCallback { get; set; }
        public OnGameEntered RaiseGameEnteredCallback { get; set; }

        public ApplicationManager.GlobalState ApplicationState {
            get {
                return applicationState;
            }
            set {
                applicationState = value;
                switch (applicationState) {
                    case ApplicationManager.GlobalState.Menu:
                        if (RaiseMenuEnteredCallback != null) {
                            RaiseMenuEnteredCallback();
                        }
                        break;
                    case ApplicationManager.GlobalState.Game:
                        if (RaiseGameEnteredCallback != null) {
                            RaiseGameEnteredCallback();
                        }
                        break;
                    default:
                        break;
                }
                Log("set application state");
            }
        }

        private void Log(string s, int severity = 0) {
            string put = string.Format("[+] {0} ... [{1}]", s, Time.time);
            switch(severity) {
                case 1:
                    Debug.LogWarning(put);
                    break;
                case 2:
                    Debug.LogError(put);
                    break;
                default:
                    Debug.Log(put);
                    break;
            }
        }

        private IEnumerator Start() {
            Log("starting application");

            var last = ApplicationManager.GlobalState.None;

            do {
                if (last != applicationState) {
                    debug_label_applicationState.text = string.Format("app state: {0}", applicationState);
                    last = applicationState;
                }
                yield return Wait.ForSeconds(pollIntervalInSeconds);
            } while (true);
        }
    }
}

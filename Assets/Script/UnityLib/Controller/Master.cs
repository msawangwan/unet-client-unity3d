using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class Master : MonoBehaviour {
        private static Master instance = null;

        [SerializeField] private ApplicationManager.GlobalState initialState; // set in the inspector

        [SerializeField] private ApplicationManager applicationManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UIManager uiManager;

        private Dictionary<string, IManager> managers = new Dictionary<string, IManager>();

        public static Master Instance {
            get {
                return instance;
            }
            private set {
                instance = value;
            }
        }

        public IManager this[string label] {
            get {
                if (!managers.ContainsKey(label)) {
                    return null;
                }
                return managers[label];
            }
            set {
                if (!managers.ContainsKey(label)) {
                    managers[label] = value;
                    Debug.LogFormat("registered [{0}] with label [{1}]", value.ToString(), label);
                }
            }
        }

        private void LoadManagers() {
            applicationManager.ApplicationState = initialState;
        }

        private void Awake() {
            LoadManagers();
        }

        private void OnEnable() {
            if (Master.Instance == null) {
                Master.Instance = this;
                DontDestroyOnLoad(this);
            } else {
                Debug.LogWarningFormat("[+] already a master controller so destroying duplicate instance ... [{0}]", Time.time);
                Destroy(gameObject);
            }
        }
    }
}

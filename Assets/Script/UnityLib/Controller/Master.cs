using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class Master : MonoBehaviour {
        private static Master instance = null;

        [SerializeField] public ApplicationManager.GlobalState initState; // set in the inspector

        [SerializeField] private ApplicationManager applicationManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UIManager uiManager;


        public static Master Instance {
            get {
                return instance;
            }
            private set {
                instance = value;
            }
        }

        private void Init() {
            applicationManager.ApplicationState = initState;
        }

        private void Awake() {
            Init();
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

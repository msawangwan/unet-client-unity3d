using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class Master : MonoBehaviour {
        private static Master instance = null;

        [SerializeField] private ApplicationManager applicationManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UIManager uiManager;
    
        public static Master Instance {
            get {
                return instance;
            }
        }
    }
}

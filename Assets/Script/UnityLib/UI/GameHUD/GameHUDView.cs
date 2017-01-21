using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityLib {
    public class GameHUDView : MonoBehaviour {
        [SerializeField] private GameHUDController gameHUDController;

        [SerializeField] private Text playerNameFieldText;
        [SerializeField] private Text opponentNameFieldText;
        [SerializeField] private Text currentTurnNumberText;

        // text color changes based on who's turn it is (a highlight/bold/italized effect)

        public void SetNameTextField(string name, bool isOpponentName) {
            Debug.LogFormat("set name [name: {0}][opponent: {1}]", name, isOpponentName);

            if (isOpponentName) {
                opponentNameFieldText.text = name;
                return;
            }

            playerNameFieldText.text = name;
        }

        public void Init() {
            Debug.LogWarningFormat("init gamehudview");
        }
    }
}

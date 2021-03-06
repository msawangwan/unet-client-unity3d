﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityLib {
    public class GameHUDView : MonoBehaviour {
        [SerializeField] private GameHUDController gameHUDController;

        [SerializeField] private Text playerNameFieldText;
        [SerializeField] private Text opponentNameFieldText;
        [SerializeField] private Text currentTurnNumberText;
        [SerializeField] private Text actionText;
        [SerializeField] private Text actionToConfirmBtnText;
        [SerializeField] private GameObject actionOverlayPanelContainer;
        [SerializeField] private GameObject executeActionPanelContainer;
        [SerializeField] private GameObject loadingBlockContainer;
        [SerializeField] private Button executeActionBtn;
        [SerializeField] private Button endTurnButton;

        private IEnumerator executingFadeRoutine = null;
        private IEnumerator executingWaitRoutine = null;

        // TODO: text color changes based on who's turn it is (a highlight/bold/italized effect)

        public void OnTurnEndAndButtonPress(Action onPress) {
            endTurnButton.onClick.RemoveAllListeners();
            endTurnButton.onClick.AddListener(
                () => {
                    if (onPress != null) {
                        onPress();
                    }
                }
            );
        }

        public void DisplayLoadingBlockPanel(string s="loading...") {
            loadingBlockContainer.SetActive(true);
        }

        public void DisableLoadingBlockPanel(string s="loading...") {
            loadingBlockContainer.SetActive(false);
        }

        public void DisplayActionButtonAndOnPressExecute(string text, Action onPress) {
            actionToConfirmBtnText.text = text;
            executeActionPanelContainer.SetActive(true);
            executeActionBtn.onClick.RemoveAllListeners();
            executeActionBtn.onClick.AddListener(
                () => {
                    if (onPress != null) {
                        onPress();
                    }
                    if (executeActionPanelContainer.activeInHierarchy) {
                        executeActionPanelContainer.SetActive(false);
                    }
                }
            );
        }

        public void SetTextHUDMessageOverlayThenFade(string alertmsg) {
            actionText.text = alertmsg;
            actionOverlayPanelContainer.SetActive(true);
            if (executingFadeRoutine == null) {
                executingFadeRoutine = FadeOut(actionOverlayPanelContainer);
                StartCoroutine(executingFadeRoutine);
            } else {
                // TODO: add TO A QUEUE then drain the queue
                Debug.LogErrorFormat(gameObject, "fade routine is already running cant load another");
            }
        }

        public void SetTextHUDMessageOverlay(string alertmsg) {
            actionText.text = alertmsg;
            actionOverlayPanelContainer.SetActive(true);
        }
        
        public void ClearTextHUDMessageOverlay() {
            actionOverlayPanelContainer.SetActive(false);
        }

        public void SetTextHUDNameField(string name, bool isOpponentName) {
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

        private static IEnumerator FadeOut (GameObject go, float delay = 5.0f, float rate = 0.05f) { /* clean up and put this else where */
            CanvasGroup cgroup = go.GetComponent<CanvasGroup>();

            if (cgroup != null) {
                cgroup.alpha = 1.0f;

                yield return new WaitForSeconds(delay);


                while (cgroup.alpha > 0) {
                    cgroup.alpha -= rate;
                    yield return Wait.ForEndOfFrame;
                }
            }

            yield return Wait.ForEndOfFrame;

            yield return new WaitUntil(
                ()=>{
                    if(go.activeInHierarchy) {
                        go.SetActive(false);
                        return false;
                    }
                    return true;
                }
            );
        }

        // TODO: temp solution
        private void Update() {
            if (executingFadeRoutine != null && !actionOverlayPanelContainer.activeInHierarchy) {
                executingFadeRoutine = null;
            } else {
                return;
            }
        }

        private void OnEnable() {
            actionOverlayPanelContainer.SetActive(false);
            executeActionPanelContainer.SetActive(false);
            loadingBlockContainer.SetActive(false);
        }
    }
}

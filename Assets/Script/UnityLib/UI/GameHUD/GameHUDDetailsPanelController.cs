using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityLib {
    public class GameHUDDetailsPanelController : MonoBehaviour {
        [SerializeField] private GameObject detailsContainerPanel;
        [SerializeField] private GameObject nodeDetailsControllerPanel;
        [SerializeField] private GameObject unitDetailsControllerPanel;

        [SerializeField] private Text nodeTitleText;
        [SerializeField] private Text unitTitleText;
        [SerializeField] private Text defText;
        [SerializeField] private Text capText;
        [SerializeField] private Text hpText;
        [SerializeField] private Text atkText;
        [SerializeField] private Text mvText;

        private Star current;
        private Star last;

        private bool shouldDisable = true;

        public void ToggleActive(bool toOn) {
            if (toOn) {
                detailsContainerPanel.SetActive(true);
            } else {
                detailsContainerPanel.SetActive(false);
            }
            nodeDetailsControllerPanel.SetActive(false);
            unitDetailsControllerPanel.SetActive(false);
        }

        public void ShowDetails(GameHandle.OnStarNodeSelectedCallback sel) {
            StartCoroutine(ShowWhenDataLoaded(sel())); // TODO: add to the queue instead of runnning here
        }

        private void DisableWhen() {
            if (shouldDisable) {
                nodeDetailsControllerPanel.SetActive(false);
                unitDetailsControllerPanel.SetActive(false);
            }
        }

        private IEnumerator ShowWhenDataLoaded(Star star) {
            yield return new WaitUntil(() => { return star.CachedProperties != null && star.CachedState != null; });
            // if (current == null) {
            //     current = star;
            // } else {
            //     if (current.CachedProperties.name == star.CachedProperties.name) {
                    
            //     }
            // }
            nodeTitleText.text = star.CachedProperties.name;
            defText.text = star.CachedProperties.defenseBonus.ToString();
            capText.text = star.CachedProperties.capacity.ToString();
            nodeDetailsControllerPanel.SetActive(true);
        }

        private void OnEnable() {
            SelectionArea.RaiseSelectionAreaUpEvent += DisableWhen;
        }

        private void OnDisable() {
            SelectionArea.RaiseSelectionAreaUpEvent -= DisableWhen;
        }
    }
}

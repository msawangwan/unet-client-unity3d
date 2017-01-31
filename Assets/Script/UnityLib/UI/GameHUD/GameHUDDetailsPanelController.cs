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

        public void Activate() {
            nodeDetailsControllerPanel.SetActive(false);
            unitDetailsControllerPanel.SetActive(false);
            detailsContainerPanel.SetActive(true);
        }

        public void ShowNodeDetails(GameHandle.OnStarNodeSelectedCallback sel) {
            StartCoroutine(ShowWhenDataLoaded(sel())); // TODO: add to the queue instead of runnning here
        }

        private IEnumerator ShowWhenDataLoaded(Star star) {
            yield return new WaitUntil(() => { return star.CachedProperties != null && star.CachedState != null; });
            nodeTitleText.text = star.CachedProperties.name;
            nodeDetailsControllerPanel.SetActive(true);
        }
    }
}

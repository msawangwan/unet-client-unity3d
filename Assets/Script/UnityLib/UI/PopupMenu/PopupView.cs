using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityLib {
    public class PopupView : MonoBehaviour {
        public PopupController controller;

        [SerializeField] private Button viewDeployMenuButton;
        [SerializeField] private Button viewMoveToMenuButton;
        [SerializeField] private Button viewAttackMenuButton;
        [SerializeField] private Button viewInfoMenuButton;

        [SerializeField] private Button persistantBackButton;

        // TODO: CLEAR THESE WHEN FOCUS IS LOST
        [SerializeField] private Text headerNameText;
        [SerializeField] private Text infoText;
        [SerializeField] private Text moveToCostText;
        [SerializeField] private Text movetoNodeText;

        [SerializeField] private GameObject unitlistUnitPrefab;

        [SerializeField] private GameObject unitDeployListScrollViewContentPanel;

        private string currentKey;
        private string currentSelectedName;
        private string currentSelectedInfo;
        private int currentSelectedCapacity;
        private int currentSelectedDeployCost;
        private int currentSelectedMoveCost;
        private int currentSelectedAttackPenalty;

        public void Load(GameHandle.OnStarNodeSelectedCallback sel) {
            StartCoroutine(Cache(sel()));
        }

        private IEnumerator Cache(Star star) {
            yield return new WaitUntil(() => { return star.CachedProperties != null && star.CachedState != null; });
            currentSelectedName = star.CachedProperties.name;
            // currentSelectedInfo = star.CachedProperties.info;
            // currentSelectedCapacity = star.CachedProperties.capactiy;
            // currentSelectedDeployCost = star.CachedProperties.deployCost;
            // currentSelectedMoveCost = star.CachedProperties.moveCost;
            // currentSelectedAttackPenalty = star.CachedProperties.attackPenalty;
            yield return Wait.ForEndOfFrame;
            headerNameText.text = currentSelectedName;
        }

        public void Init() {
            Button[] allButtons = new Button[] {
                viewDeployMenuButton,
                viewMoveToMenuButton,
                viewAttackMenuButton,
                viewInfoMenuButton,
                persistantBackButton
            };

            foreach (var item in allButtons) {
                if (item != null) {
                    item.onClick.RemoveAllListeners();
                }
            }

            viewDeployMenuButton.onClick.AddListener(
                () => {
                    controller.DownOneLevel(0);
                }
            );

            viewMoveToMenuButton.onClick.AddListener(
                () => {
                    controller.DownOneLevel(1);
                }
            );

            viewAttackMenuButton.onClick.AddListener(
                () => {
                    controller.DownOneLevel(2);
                }
            );

            viewInfoMenuButton.onClick.AddListener(
                () => {
                    controller.DownOneLevel(3);
                    infoText.text = currentSelectedInfo;
                }
            );

            persistantBackButton.onClick.AddListener(
                () => {
                    controller.UpOneLevel();
                }
            );
        }
    }
}

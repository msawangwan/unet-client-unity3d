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

        // TODO: GET THIS WORKING!!
        public IEnumerator SetCurrent(string key, Star.State state, Star.Properties prop) {
            if (currentKey != key) {

            }
            yield return Wait.ForEndOfFrame;
            yield return new WaitUntil(() => { return state != null && prop != null; });
            currentSelectedName = prop.name;
            currentSelectedInfo = prop.info;
            currentSelectedCapacity = prop.capactiy;
            currentSelectedDeployCost = prop.deployCost;
            currentSelectedMoveCost = prop.moveCost;
            currentSelectedAttackPenalty = prop.attackPenalty;
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

        private void OnEnable() {
            Debug.LogFormat("MY NAME::::: {0}", currentSelectedName);
            headerNameText.text = currentSelectedName;
        }
    }
}

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

        [SerializeField] private Text moveToCostText;
        [SerializeField] private Text movetoNodeText;

        [SerializeField] private GameObject unitlistUnitPrefab;

        [SerializeField] private GameObject unitDeployListScrollViewContentPanel;


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

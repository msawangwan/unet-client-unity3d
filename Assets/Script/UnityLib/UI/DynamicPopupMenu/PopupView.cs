using UnityEngine;
using UnityEngine.UI;

namespace UnityLib.UI {
    public class PopupView : MonoBehaviour {
        public PopupController controller;

        [SerializeField] private Button openResourceSubmenu;
        [SerializeField] private Button openEnergySubmenu;
        [SerializeField] private Button openPopulationSubmenu;
        [SerializeField] private Button openFactionSubmenu;

        [SerializeField] private Button persistantBackButton;



        public void Init() {
            Button[] allButtons = new Button[] {
                openResourceSubmenu,
                openEnergySubmenu,
                openPopulationSubmenu,
                openFactionSubmenu,
                persistantBackButton
            };

            foreach (var item in allButtons) {
                if (item != null) {
                    item.onClick.RemoveAllListeners();
                }
            }

            openResourceSubmenu.onClick.AddListener(
                () => {
                    controller.DownOneLevel(0);
                }
            );

            openEnergySubmenu.onClick.AddListener(
                () => {
                    controller.DownOneLevel(1);
                }
            );

            openPopulationSubmenu.onClick.AddListener(
                () => {
                    controller.DownOneLevel(2);
                }
            );

            openFactionSubmenu.onClick.AddListener(
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

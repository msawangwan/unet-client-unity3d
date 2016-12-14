using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAPI.Model;

namespace UnityAPI.Framework.UI {
    public class MenuView : MonoBehaviour {
        public MenuController controller;

        [SerializeField] private Button createProfile;
        [SerializeField] private Button selectProfile;

        [SerializeField] private Button confirmCreate;
        [SerializeField] private Button toTitleFromCreate;

        [SerializeField] private Button loadFromSlot1;
        [SerializeField] private Button loadFromSlot2;
        [SerializeField] private Button loadFromSlot3;
        [SerializeField] private Button toTitleFromSelect;

        [SerializeField] private Button confirmConfirm;
        [SerializeField] private Button cancelConfirm;

        [SerializeField] private InputField profileNameTextField;

        [SerializeField] private Text confirmNameText;

        // private Action onConfirm;
        // private Action onCancel;

        // private bool fromCreate;
        private string profileName;

        public void Init() {
            Button[] allButtons = new Button[] {
                createProfile,
                selectProfile,

                confirmCreate,
                toTitleFromCreate,

                loadFromSlot1,
                loadFromSlot2,
                loadFromSlot3,
                toTitleFromSelect,

                confirmCreate,
                cancelConfirm
            };
            
            foreach (var item in allButtons) {
                if (item != null) {
                    item.onClick.RemoveAllListeners();
                }
            }

            createProfile.onClick.AddListener(
                () => {
                    controller.Traverse(0, 0, 0);
                }
            );

            selectProfile.onClick.AddListener(
                () => {
                    controller.Traverse(0, 0, 1);
                }
            );

            confirmCreate.onClick.AddListener(
                () => {
                    Global.GlobalGameController gameController = Global.Globals.S.globalGameController as Global.GlobalGameController;

                    profileName = profileNameTextField.text;

                    if (string.IsNullOrEmpty(profileName)){
                        /* todo: needs menu prompt */
                        Debug.LogErrorFormat("err: empty name not allowed!");
                        /* todo: needs menu prompt */
                    } else {
                        confirmNameText.text = profileName + " >>";

                        gameController.QueueStage1Action(() => { controller.Traverse(2, 0, -1, true); });
                        StartCoroutine(gameController.VerifyProfileValidRoutine(profileName));
                    }
                }
            );

            toTitleFromCreate.onClick.AddListener(
                () => {
                    controller.Traverse(1, 0, 0);
                }
            );

            loadFromSlot1.onClick.AddListener(
                () => {
                    Debug.Log("loading profile slot 1");
                }
            );

            loadFromSlot2.onClick.AddListener(
                () => {
                    Debug.Log("loading profile slot 2");
                }
            );

            loadFromSlot3.onClick.AddListener(
                () => {
                    Debug.Log("loading profile slot 3");
                }
            );

            toTitleFromSelect.onClick.AddListener(
                () => {
                    controller.Traverse(1, 1, 0);
                }
            );

            confirmConfirm.onClick.AddListener(
                () => {
                    controller.ExitMenu(true, null);
                }
            );

            cancelConfirm.onClick.AddListener(
                () => {
                    controller.UpOneLevel();
                }
            );
        }
    }
}

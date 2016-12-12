using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField] private InputField profileNameTextField;

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

            toTitleFromCreate.onClick.AddListener(
                () => {
                    controller.Traverse(1, 0, 0);
                }
            );

            toTitleFromSelect.onClick.AddListener(
                () => {
                    controller.Traverse(1, 1, 0);
                }
            );

            confirmCreate.onClick.AddListener(
                () => {
                    string profileName = profileNameTextField.text;
                    if (controller.VerifyProfileIsValid(profileName)) {
                        Debug.Log("new profile created: " + profileName);
                    } else {
                        Debug.Log("invalid profile name: " + profileName);
                    }
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
        }
    }
}

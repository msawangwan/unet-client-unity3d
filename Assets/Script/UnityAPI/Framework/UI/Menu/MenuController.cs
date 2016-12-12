using System.Collections.Generic;
using System;
using UnityEngine;

namespace UnityAPI.Framework.UI {
    public class MenuController : ControllerBehaviour {
        public MenuView MenuView;

        private MenuModel[,] menuGraph = new MenuModel[5, 5];
        private List<int> menus = new List<int>();
        private Stack<MenuModel> traversalPath = new Stack<MenuModel>();

        private MenuModel rootMenu;
        private MenuModel activeMenu;
        
        public void Init() {
            for (int i = 0; i < MenuView.transform.childCount; i++) {
                MenuModel m = MenuView.transform.GetChild(i).GetComponent<MenuModel>();
                if (!m.isInitialised) {
                    m.Init(this);
                    if (!this.menus.Contains(m.MenuUUID)) {
                        this.menuGraph[m.MenuLevel, m.MenuID] = m;

                        if (m.isRoot) {
                            this.SetActiveState(m, true);
                            this.traversalPath.Push(m);
                        }

                        this.menus.Add(m.MenuUUID);
                    }
                }
            }
        }

        public void Traverse(int level, int id, int submenuKey) {
            MenuModel m = this.menuGraph[level, id][submenuKey];
            SetActiveState(activeMenu, false);
            SetActiveState(m, true);
        }

        public bool VerifyProfileIsValid(string profileName) {
            if (profileName != string.Empty) { // query db
                UnityAPI.Model.ProfileSearch newProfile = new UnityAPI.Model.ProfileSearch(profileName);
                string json = JsonUtility.ToJson(newProfile);
                UnityAPI.Framework.Net.ServiceController service = Global.Globals.S.serviceController as UnityAPI.Framework.Net.ServiceController;
                return service.ValidateNewProfile(json);
            }
            return false;
        }

        public void ExitMenu(bool disableAll, System.Action onExit) {
            if (disableAll) {
                foreach (var item in menuGraph) {
                    if (item != null) {
                        this.SetActiveState(item, false);
                    }
                }
            }
            if (onExit != null) {
                onExit();
            }
        }

        protected override bool OnInit() {
            this.Init();
            MenuView.Init();
            return true;
        }

        private void SetActiveState(MenuModel m, bool toggleActive) {
            m.gameObject.SetActive(toggleActive);
            if (toggleActive) {
                this.activeMenu = m;
                traversalPath.Push(activeMenu);
            }
        }
    }
}

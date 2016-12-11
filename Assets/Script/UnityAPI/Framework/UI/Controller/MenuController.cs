using System.Collections.Generic;
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
                        Debug.LogFormat("{0} added to menu list [level {1}][id {2}]", m.name, m.MenuLevel, m.MenuID);
                    }
                }
            }
        }

        protected override bool OnInit() {
            this.Init();
            MenuView.Init();
            return true;
        }

        private void SetActiveState(MenuModel m, bool toggleActive = true) {
            m.gameObject.SetActive(toggleActive);
            if (toggleActive) {
                this.activeMenu = m;
            }
        }

        public void DownOneLevel(int level, int id) {
            MenuModel m = this.menuGraph[level, id];
            if (traversalPath.Peek().MenuLevel != m.MenuLevel) {
                if (activeMenu.MenuID != m.MenuID) {
                    MenuModel p = traversalPath.Pop();

                    traversalPath.Push(m);

                    p.MakeInactive();
                    m.MakeActive();
                }
            }
        }
    }
}

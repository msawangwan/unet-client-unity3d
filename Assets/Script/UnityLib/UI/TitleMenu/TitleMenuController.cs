using System.Collections.Generic;

namespace UnityLib.UI {
    public class TitleMenuController : ControllerBehaviour {
        public TitleMenuView TitleMenuView;

        private TitleMenuPanel[,] menuGraph = new TitleMenuPanel[5, 5];
        private List<int> menus = new List<int>();
        private Stack<TitleMenuPanel> traversalPath = new Stack<TitleMenuPanel>();

        private TitleMenuPanel rootMenu;
        private TitleMenuPanel activeMenu;

        public void Traverse(int level, int id, int submenuKey, bool keepPreviousActive = false) {
            TitleMenuPanel m = null;

            if (submenuKey == -1) {
                m = this.menuGraph[level, id];
            } else {
                m = this.menuGraph[level, id][submenuKey];
            }

            SetActiveState(activeMenu, keepPreviousActive);
            SetActiveState(m, true);
        }

        public void UpOneLevel() {
            TitleMenuPanel m = traversalPath.Pop();

            if (m == activeMenu) { // works for now but, find a cleaner solution than this me thinks...
                m = traversalPath.Pop();
            }

            SetActiveState(activeMenu, false);
            SetActiveState(m, true);
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
            for (int i = 0; i < TitleMenuView.transform.childCount; i++) {
                TitleMenuPanel m = TitleMenuView.transform.GetChild(i).GetComponent<TitleMenuPanel>();
                if (!m.isInitialised) {
                    m.Init();
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

            TitleMenuView.Init();

            return true;
        }

        private void SetActiveState(TitleMenuPanel m, bool toggleActive) {
            m.gameObject.SetActive(toggleActive);
            if (toggleActive) {
                this.activeMenu = m;
                traversalPath.Push(activeMenu);
            }
        }
    }
}

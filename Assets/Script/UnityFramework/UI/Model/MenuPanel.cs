using UnityFramework.UI.Manager;
using System.Collections;

namespace UnityFramework.UI.Model {
    public abstract class MenuPanel<T> : InstanceBehaviour where T : class {
        protected MenuManager<T> menu = null;
        protected MenuPanel<T>[] submenus = null;

        private bool executeSetupComplete = false;

        public bool isCurrentView { get; private set; }

        public abstract bool isRootMenu { get; }
        protected abstract int submenuCount { get; }

        public abstract void MapUIDependencies();
        protected abstract void MapParentMenu(MenuManager<T> parentMenu);
        protected abstract IEnumerator onLoadMapSubMenus();

        protected virtual void Start() {
            if (!executeSetupComplete) {
                menu = Global.Globals.S.homeMenuManager as MenuManager<T>;
                MapParentMenu(menu);
                
                menu.CacheMenuWithManager(gameObject, base.instanceID, submenuCount);
                submenus = new MenuPanel<T>[submenuCount];

                StartCoroutine(onLoadMapSubMenus());

                executeSetupComplete = true;
            }
        }
    }
}
using UnityAPI.UI.Manager;
using System.Collections;
using System.Collections.Generic;

namespace UnityAPI.UI.Model {
    public abstract class MenuPanel<T> : InstanceBehaviour where T : class {
        protected MenuController<T> menu = null;
        protected MenuPanel<T>[] submenus = null;

        // public int MenuKey {get { return 0; } }

        private bool executeSetupComplete = false;
        private IEnumerator onLoadRoutine = null;

        public bool isCurrentView { get; private set; }

        public abstract bool isRootMenu { get; }
        protected abstract int submenuCount { get; }

        public abstract void MapUIDependencies();

        protected abstract void MapParentMenu(MenuController<T> parentMenu);
        protected abstract IEnumerator onLoadMapSubMenus();

        public IEnumerator MapDependenciesAsync(IEnumerable<MenuPanel<T>> a, System.Predicate<MenuPanel<T>> pred) {
            foreach (MenuPanel<T> item in a) {
                if (pred(item)) {
                    yield break;
                }
                yield return item;
            }
        }

        protected virtual void OnEnable() {
            if (onLoadRoutine == null) { // the first time this thing comes to life we do this
                UnityEngine.Debug.LogFormat("loaded map routine: {0}", gameObject.name);
                onLoadRoutine = onLoadMapSubMenus();
            } else {
                UnityEngine.Debug.LogFormat("executing map routine: {0}", gameObject.name);
                StartCoroutine(onLoadRoutine);
            }
        }

        protected virtual void Start() {
            if (!executeSetupComplete) {
                menu = Global.Globals.S.homeMenuManager as MenuController<T>;

                MapParentMenu(menu);
                
                executeSetupComplete = menu.Cache(gameObject, base.instanceID, submenuCount);
                submenus = new MenuPanel<T>[submenuCount];
            }
        }

        protected virtual void OnDisable() {
            if (onLoadRoutine != null) {
                UnityEngine.Debug.LogFormat("terminating map routine: {0}", gameObject.name);
                StopCoroutine(onLoadRoutine);
            }
        }
    }
}
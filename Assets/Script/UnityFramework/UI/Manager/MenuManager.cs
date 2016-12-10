using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.UI.Model;

namespace UnityFramework.UI.Manager {
    public abstract class MenuManager : ManagerBehaviour {
        protected List<int[]> menuGraph = new List<int[]>();
        protected Stack<int> menuHistory = new Stack<int>();
        protected Dictionary<int, MenuPanel> menuCache = new Dictionary<int, MenuPanel>();
        protected System.Action<int, GameObject> onMenuRegistered = null;

        protected abstract int subMenuCount { get; }

        public MenuPanel this[int key] {
            get {
                return menuCache[key];
            }
        }

        public bool isCacheEmpty {
            get {
                return menuCache.Count <= 0;
            }
        }

        public bool allMenusCached { 
            get;
            protected set;
        }

        // IEnumerator Start()

        public bool isCached(int menuID) {
            return menuCache.ContainsKey(menuID);
        }

        public bool CacheMenuWithManager(GameObject menuGameObject, int menuInstanceID, int submenuCount) {
            if (menuGameObject) {
                if (!menuCache.ContainsKey(menuInstanceID)) {
                    MenuPanel m = menuGameObject.GetComponent<MenuPanel>();
                    menuCache.Add(menuInstanceID, m);
                    menuGraph.Add(new int[submenuCount]);
                    if (onMenuRegistered != null) {
                        onMenuRegistered(menuInstanceID, menuGameObject);
                    }
                    return true;
                }
            }
            return false;
        }

        protected override bool HandleInitialisation() {
            return true;
        }
    }
}

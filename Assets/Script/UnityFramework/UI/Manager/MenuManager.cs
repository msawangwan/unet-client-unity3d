using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.UI.Manager {
    public abstract class MenuManager<T> : ManagerBehaviour where T : class {
        protected List<int[]> menuGraph = new List<int[]>();
        protected Stack<int> menuHistory = new Stack<int>();
        protected Dictionary<int, T> menuCache = new Dictionary<int, T>();
        protected System.Action<int, GameObject> onMenuRegistered = null;

        protected abstract int menuCount { get; }

        public T this[int key] {
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
                    T m = menuGameObject.GetComponent<T>();
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

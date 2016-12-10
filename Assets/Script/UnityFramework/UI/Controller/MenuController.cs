using System.Collections.Generic;
using UnityEngine;
// using UnityFramework.UI.Model;

namespace UnityFramework.UI.Manager {
    public abstract class MenuController<T> : ControllerBehaviour where T : class {
        protected List<int[]> menuGraph = new List<int[]>();
        protected Stack<int> menuHistory = new Stack<int>();
        protected Dictionary<int, T> menuCache = new Dictionary<int, T>();
        protected System.Action<int, GameObject> onMenuCached = null;

        protected abstract int menuCount { get; }
        protected abstract bool isRootSet { get; set; }

        public T this[int key] {
            get {
                return menuCache[key];
            }
        }

        protected int[] menuRoot {
            get {
                return menuGraph[0];
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

        public bool Cache(GameObject menuGameObject, int menuInstanceID, int submenuCount) {
            bool cachedSuccessfully = false;
            if (menuGameObject) {
                if (!menuCache.ContainsKey(menuInstanceID)) {
                    T m = menuGameObject.GetComponent<T>();

                    menuCache.Add(menuInstanceID, m);
                    menuGraph.Add(new int[submenuCount]);

                    if (onMenuCached != null) {
                        onMenuCached(menuInstanceID, menuGameObject);
                    }

                    cachedSuccessfully = true;
                }
            }
            return cachedSuccessfully;
        }

        protected override bool HandleInitialisation() {
            return true;
        }
    }
}

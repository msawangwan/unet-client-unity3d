using System.Collections.Generic;
using UnityEngine;

namespace UnityLib.UI {
    public class TitleMenuPanel : MonoBehaviour {
        public static List<int> menusByUIID = new List<int>();

        public static int nextMenuUIID {
            get {
                while (true) {
                    ++currentMenuUUID;
                    if (!menusByUIID.Contains(currentMenuUUID)) {
                        return currentMenuUUID;
                    }
                }
            }
        }

        private static int currentMenuUUID = -1;

        private bool isInit = false;

        [SerializeField] private TitleMenuInstance menu;
        [SerializeField] private TitleMenuPanel parent;
        [SerializeField] private TitleMenuPanel[] links;

        public TitleMenuPanel this[int lookupID] {
            get {
                if (!hasLinks || lookupID > NumberOfLinks) {
                    return null;
                }
                return links[lookupID];
            }
        }

        public IEnumerable<TitleMenuPanel> Links {
            get {
                return links;
            }
        }

        public int NumberOfLinks {
            get {
                return links.Length;
            }
        }

        public int MenuUUID { 
            get; 
            set; 
        }

        public int MenuLevel { 
            get { 
                return menu.menuLevel; 
            } 
        }

        public int MenuID { 
            get { 
                return menu.menuInstanceID; 
            } 
        }

        public bool isRoot { 
            get { 
                return menu.menuType == TitleMenuInstance.MenuType.Root; 
            } 
        }

        public bool hasLinks { 
            get { 
                return links.Length > 0; 
            } 
        }

        public bool isInitialised {
            get { 
                return isInit; 
            } 
        }


        public void Init()  {
            if (!isInit) {
                this.MenuUUID = nextMenuUIID;

                gameObject.SetActive(true);
                if (links.Length <= 0) {
                    if (parent != null) {
                        links = new TitleMenuPanel[1] { parent };
                    }
                }
                gameObject.SetActive(false);

                isInit = true;
            }
        }

        public void MakeActive() {
            gameObject.SetActive(true);
        }

        public void MakeInactive() {
            gameObject.SetActive(false);
        }
    }
}

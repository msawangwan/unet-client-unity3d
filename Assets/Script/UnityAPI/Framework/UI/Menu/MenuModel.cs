using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAPI.Framework.UI {
    public class MenuModel : MonoBehaviour {
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

        [SerializeField] private MenuData menu;
        [SerializeField] private MenuModel parent;
        [SerializeField] private MenuModel[] links;

        public MenuModel this[int lookupID] {
            get {
                if (!hasLinks || lookupID > NumberOfLinks) {
                    return null;
                }
                return links[lookupID];
            }
        }

        public IEnumerable<MenuModel> Links {
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
                return menu.menuType == MenuData.MenuType.Root; 
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
                        links = new MenuModel[1] { parent };
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

﻿using System.Collections;
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

        private MenuController owner;

        [SerializeField] private MenuData menu;

        public MenuModel this[int lookupID] {
            get {
                if (hasLinks || lookupID <= NumberOfLinks) {
                    return null;
                }
                return menu.links[lookupID];
            }
        }

        public IEnumerable<MenuModel> Links {
            get {
                return menu.links;
            }
        }

        public int NumberOfLinks {
            get {
                return menu.links.Length;
            }
        }

        public int MenuUUID { get; set; }
        public int MenuLevel { get { return menu.menuLevel; } }
        public int MenuID { get { return menu.menuInstanceID; } }
        public bool isRoot { get { return menu.menuType == MenuData.MenuType.Root; } }
        public bool hasLinks { get { return menu.links.Length > 0; } }


        public void Init(MenuController owner)  {
            this.MenuUUID = nextMenuUIID;
            this.owner = owner;

            gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void MakeActive() {
            gameObject.SetActive(true);
        }

        public void MakeInactive() {
            gameObject.SetActive(false);
        }
    }
}
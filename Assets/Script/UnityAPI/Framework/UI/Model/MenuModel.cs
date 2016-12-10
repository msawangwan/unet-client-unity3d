using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAPI.Framework.UI {
    public class MenuModel : MonoBehaviour {
        [SerializeField] private int menuLevel;
        [SerializeField] private int menuID;
        [SerializeField] private bool isRootMenu;

        public MenuModel[] links;
        public MenuController owner;

        public int MenuLevel {
            get {
                return menuLevel;
            }
        }

        public int MenuID { 
            get { 
                return menuID; 
            }
        }

        public bool isRoot {
            get {
                return isRootMenu;
            }
        }

        public void OnInit(MenuController owner)  {
            this.owner = owner;
            gameObject.SetActive(true);
            if (isRoot) {
                gameObject.SetActive(false);
            }
        }
    }
}

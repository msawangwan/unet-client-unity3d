using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAPI.Framework.UI {
    public class MenuController : MonoBehaviour {
        public Dictionary<int, MenuModel[]> menuGraph = new Dictionary<int, MenuModel[]>();

        public void OnInit() {
            for (int i = 0; i < transform.childCount; i++) {
                MenuModel m = transform.GetChild(i).GetComponent<MenuModel>();
                m.OnInit(this);
                menuGraph.Add(m.MenuLevel, m.links);
            }
        }
    }
}

using UnityEngine;
using UnityFramework.UI.Manager;
using System.Collections;
using System.Collections.Generic;

namespace UnityFramework.UI.Model {
    public abstract class MenuPanel : InstanceBehaviour {
        public class Node {
            public readonly Node Next;

            public Node(Node next) {
                Next = next;
            }
        }

        protected MenuManager menu = null;
        protected MenuPanel.Node link = null;

        private bool hasRegisteredWithManager = false;

        public abstract bool isDefaultView { get; }

        public void LinkMenuNode(MenuPanel.Node next) {
            if (link == null) {
                link = new MenuPanel.Node(next);
            }
        }

        protected virtual void Start() {
            if (!menu) {
                menu = Global.Globals.S.homeMenuManager as MenuManager;
            }

            if (!hasRegisteredWithManager) { // id not assigned yet..
                menu.RegisterWithManager(gameObject, base.instanceID);
            }
        }
    }
}

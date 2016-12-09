using UnityEngine;
using UnityFramework.UI.Manager;
using System.Collections;
using System.Collections.Generic;

namespace UnityFramework.UI.Model {
    public abstract class MenuPanel : InstanceBehaviour {
        private MenuManager menu = null;
        private bool hasRegisteredWithManager = false;

        public abstract bool isDefaultView { get; }

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

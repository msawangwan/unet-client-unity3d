using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.UI.Model {
    public class SelectProfilePanel : MenuPanel {
        public override bool isDefaultView { get { return false; } }

        public override void RunSetup() {
            Debug.Log("select profile runned settup");
        }
    }
}

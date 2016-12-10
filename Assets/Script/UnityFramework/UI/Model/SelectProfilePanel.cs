using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.UI.Model {
    public class SelectProfilePanel : MenuPanel {
        public override bool isRootMenu { get { return false; } }

        public override void MapUIDependencies() {
            Debug.Log("select profile runned settup");
        }
    }
}

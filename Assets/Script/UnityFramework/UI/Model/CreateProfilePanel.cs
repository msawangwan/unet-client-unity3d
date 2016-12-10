using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.UI.Model {
    public class CreateProfilePanel : MenuPanel {
        public override bool isRootMenu { get { return false; } }

        public override void MapUIDependencies() {
            Debug.Log("create profile runned settup");
        }
    }
}
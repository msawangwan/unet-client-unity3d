﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.UI.Model {
    public class CreateProfilePanel : MenuPanel {
        public override bool isDefaultView { get { return false; } }

        public override void RunSetup() {
            Debug.Log("create profile runned settup");
        }
    }
}
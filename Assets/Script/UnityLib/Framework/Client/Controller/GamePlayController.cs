using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib.Framework.Client {
    public class GamePlayController : ControllerBehaviour {
        private Queue<Action> stageOne = new Queue<Action>();

        public void EnqueueStageOneCommand(Action c) {
            stageOne.Enqueue(c);
        }

        protected override bool OnInit() {
            return true;
        }
    }
}

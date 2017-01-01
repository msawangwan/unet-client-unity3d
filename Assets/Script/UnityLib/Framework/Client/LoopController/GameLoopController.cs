using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib.Framework.Client {
    public class GameLoopController : ControllerBehaviour {
        private Queue<Action> stageOne = new Queue<Action>();
        private List<GameObject> worldNodes = new List<GameObject>();

        private Player currentPlayer;

        public void EnqueueStageOneCommand(Action c) {
            stageOne.Enqueue(c);
        }

        public void LoadGameplayWorldNodes(List<GameObject> worldNodes) {
            this.worldNodes = worldNodes;
        }

        public void EnterGamePlay() {
            currentPlayer = Player.New();
            foreach (GameObject go in worldNodes) {
                go.AddComponent<StarNode>();
            }
            CameraRigController.S.EnableMovement();
        }

        protected override bool OnInit() {
            return true;
        }
    }
}

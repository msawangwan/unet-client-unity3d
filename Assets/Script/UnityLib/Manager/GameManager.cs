using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class GameManager : MonoBehaviour, IManager {
        public string Label {get { return gameObject.name; } }
        public UpdateHandler update { get; private set; }

        public void Init() {
        }

        private void OnEnable() {
            Master.Instance[Label] = this;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class GameManager : MonoBehaviour {
        public UpdateHandler update { get; private set; }

        public void Init(UpdateHandler update) {
            this.update = update;
        }
    }
}

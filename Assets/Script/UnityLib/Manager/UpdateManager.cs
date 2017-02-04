using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class UpdateManager : MonoBehaviour {
        public UpdateHandler UpdateHandle { get; private set; }

        public void Load(UpdateHandler updateHandle) {
            this.UpdateHandle = updateHandle;
        }
    }
}

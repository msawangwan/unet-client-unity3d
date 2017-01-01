using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib.UI {
    public class MainMenuLevel : MonoBehaviour {
        public int levelIndex;
        public Dictionary<int, MainMenuPanel> SubLevels = new Dictionary<int, MainMenuPanel>();

        public void Start() {
            int numchild = transform.childCount;
            for (int i = 0; i < numchild; i++) {
                MainMenuPanel mmp = transform.GetChild(i).GetComponent<MainMenuPanel>();
                if (mmp) {
                    if (mmp.isSubPanel) {
                        SubLevels.Add(mmp.sublevelIndex, mmp);
                    }
                }
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class World {
        public Quadrant RootNode;
        public List<GameObject> ChildNodes;

        public World() {}

        public World(Quadrant rootNode, List<GameObject> childNodes) {
            this.RootNode = rootNode;
            this.ChildNodes = childNodes;
        }
    }
}

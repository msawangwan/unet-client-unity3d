using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class World {
        [System.Serializable]
        public class Parameters : IJSONer {
            public int nodeMaxSpawnAttempts;
            public int nodeCount;
            public float nodeRadius;
            public float worldScale;
            public long worldSeed;

            public Parameters() {}

            public string Marshall() { return JsonUtility.ToJson(this); }
            public override string ToString() { return string.Format("[node count: {0}][node radius: {1}][world scale: {2}][max spawn attempts: {3}][world seed: {4}]", nodeCount, nodeRadius, worldScale, nodeMaxSpawnAttempts, worldSeed); }
        }

        public Quadrant RootNode;
        public List<GameObject> ChildNodes;
        public Dictionary<string, Star> Stars = new Dictionary<string, Star>();

        public World() {}

        public World(Quadrant rootNode, List<GameObject> childNodes) {
            this.RootNode = rootNode;
            this.ChildNodes = childNodes;
        }
    }
}

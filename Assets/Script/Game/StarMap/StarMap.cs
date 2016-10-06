using UnityEngine;

public class StarMap : MonoBehaviour {
    [System.Serializable]
    public class GeneratorParameters {
        [System.Serializable]
        public class PrefabLinks {
            public Transform StarMapTransform = null;
            public Transform StarNodeContainerTransform = null;
            public GameObject StarPrefab = null;
        }

        public GeneratorParameters.PrefabLinks Prefabs = null;
        public bool UseRandomSeed = true;
        public int UniqueSeed = 0;
        public int MaxRetryAttempts = 20;
        public float GalaxyScale = 20.0f; // ie, map radius
        public float StarDensity = 1.5f; // ie, node spacing
    }

    [System.Serializable]
    public class MapParameters {
        public int MaxStarCount = 30;
        public Random.State SeedState;
        private const int nodeLayer = 1 << 20;
    }

    public static LinkedList<Star> Stars = new LinkedList<Star>();

    public StarMap.GeneratorParameters GeneratorSettings = null;
    public StarMap.MapParameters MapProperties = null;
}

using UnityEngine;

public class StarMap : MonoBehaviour {
    [System.Serializable]
    public class GeneratorParameters {
        public bool UseRandomSeed = true;
        public int Seed = 0;
        public int MaxRetryAttempts = 20;
        public float MapRadius = 20.0f;
        public float DensityFactor = 1.5f; // ie, node spacing
    }
    
    public static LinkedList<Star> Stars = new LinkedList<Star>();
    
    public int UniqueSeed { get; private set; }
    public int GalaxyStarCount { get; private set; }
    public float MapRadius { get; private set; }
    public Random.State SeedState { get; private set; }

    private const int nodeLayer = 1 << 20;

}

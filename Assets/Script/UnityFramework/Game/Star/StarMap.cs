using UnityEngine;

public class StarMap : MonoBehaviour {
    [System.Serializable]
    public class State {
        public Random.State SeedState;
        public int SeedValue;
        public int InstanceID;
        public int NodeCount;
        public float Scale;
        public float Density;
    }

    [System.Serializable]
    public class GeneratorParameters {
        [System.Serializable]
        public class PrefabLinks {
            public Transform StarMapTransform = null;
            public Transform StarNodeContainerTransform = null;
            public GameObject StarPrefab = null;
        }

        public GeneratorParameters.PrefabLinks Prefabs = null;
        public bool UseCustomSeedValue = false;
        public int CustomSeedValue = 0;
        public int NumberOfStars = 0;
        public int MaxRetryAttemptsAllowed = 20;
        public float GalaxyScale = 20.0f; // ie, map radius
        public float StarDensity = 1.5f; // ie, node spacing

        public int UniqueSeedValue { get { return System.Environment.TickCount; } }
    }

    public static StarMap StaticInstance = null;

    public StarMap.GeneratorParameters StarMapGeneratorParameters = null;
    private StarGenerator.Parameters GeneratorParameterData = null;

    public StarMap.State StarMapState { get; private set; }
    public int StarMapInstanceID { get; set;}

    public StarMap.State InitialiseNewMapWithRandomParameters () {
        GeneratorParameterData = new StarGenerator.Parameters(
            StarMapGeneratorParameters.Prefabs.StarPrefab,
            StarMapGeneratorParameters.Prefabs.StarNodeContainerTransform,
            StarMapGeneratorParameters.UseCustomSeedValue,
            StarMapGeneratorParameters.CustomSeedValue,
            StarMapGeneratorParameters.NumberOfStars,
            StarMapGeneratorParameters.GalaxyScale,
            StarMapGeneratorParameters.StarDensity
        );

        StarMapState = StarGenerator.GenerateStarMapState(GeneratorParameterData);
        return StarMapState;
    }

    public StarMap.State InitialiseSavedMapWithLoadedParameters (StarMap.State state) {
        GeneratorParameterData = new StarGenerator.Parameters(
            StarMapGeneratorParameters.Prefabs.StarPrefab,
            StarMapGeneratorParameters.Prefabs.StarNodeContainerTransform,
            true,
            state.SeedValue,
            state.NodeCount,
            state.Scale,
            state.Density
        );

        StarMapState = StarGenerator.GenerateStarMapState(GeneratorParameterData);
        return StarMapState;
    }

    private void Awake () {
        StaticInstance = CommonUtil.EnablePersistance (this, gameObject);
    }
}

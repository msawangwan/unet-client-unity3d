using UnityEngine;

public class StarMap : MonoBehaviour {
    [System.Serializable]
    public class SaveData {
        public bool IsNew = false;
        public int SeedValue = 0;
    }

    [System.Serializable]
    public class State {
        
    }

    [System.Serializable]
    public class GeneratorParameters {
        [System.Serializable]
        public class PrefabLinks {
            public Transform StarMapTransform = null;
            public Transform StarNodeContainerTransform = null;
            public GameObject StarPrefab = null;
        }

        public const int NodeLayer = 1 << 20;

        public GeneratorParameters.PrefabLinks Prefabs = null;
        public bool UseCustomSeedValue = false;
        public int CustomSeedValue = 0;
        public int NumberOfStars = 0;
        public int MaxRetryAttemptsAllowed = 20;
        public float GalaxyScale = 20.0f; // ie, map radius
        public float StarDensity = 1.5f; // ie, node spacing

        public int UniqueSeedValue { get { return System.Environment.TickCount; } }
    }

    [System.Serializable]
    public class MapStateParameters {
        public Random.State SeedState;
        public int SeedValue = 0;
        public int StarCount = 0;
        public Star CurrentLocation = null;
    }

    public static StarMap StaticInstance = null;

    public StarMap.GeneratorParameters GeneratorOptions = null;
    public StarMap.MapStateParameters MapState = null;

    public int StarMapInstanceID { get; set;}

    public void InitialiseNewMapWithRandomParameters () {

    }

    public void InitialiseSavedMapWithLoadedParameters () {

    }

    private void Awake () {
        StaticInstance = CommonUtil.EnablePersistance (this, gameObject);
    }
}

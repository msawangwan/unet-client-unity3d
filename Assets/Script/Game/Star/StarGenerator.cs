using UnityEngine;

public static class StarGenerator {
    public class Parameters {
        public class SceneObjects {
            public GameObject StarPrefab { get; private set; }
            public Transform StarContainer { get; private set; }
        }

        public SceneObjects Prefabs { get; private set; }
        public bool IsLoadedSeed { get; private set; }
        public int LoadedSeedValue { get; private set; }
        public int UniqueSeedValue { get { return System.Environment.TickCount; } }
        public int NumStarsToGenerate { get; private set; }
        public int MaxNumSpawnRetriesAllowed { get; private set; }
        public float MapScale { get; private set; } // i.e, map radius
        public float StarNodeProximityFactor { get; private set; } // i.e, node spacing
    }

    public static GameObject SpawnStartNode (GameObject nodePrefab) {
        return SpawnNewNode (null, null, new Star());
    }

    public static GameObject SpawnNewNode (GameObject starPrefab, Transform starParent, Star star) {
        if (starPrefab != null) {
            Quaternion nodeRotation = star.StarProperties.RotationOffset.AsZRotation();
            GameObject nodeObject = MonoBehaviour.Instantiate ( starPrefab, star.StarProperties.GalaxyCoordinate, nodeRotation, starParent ) as GameObject;
            star.NodeObject = nodeObject.GetComponentSafe<StarNode> ();
            nodeObject.name = star.NodeObject.NameField = star.StarProperties.Designation;
            return nodeObject;
        }
        return null;
    }

    public static Star SpawnNewStar (Vector3 galaxyCoordinate) {
        Star star = new Star();
        star.StarProperties = new Star.Properties();
        star.StarProperties.GalaxyCoordinate = galaxyCoordinate;
        star.StarProperties.ID = Random.Range (0, 10000);
        star.StarProperties.Designation = string.Format ("star {0}", star.StarProperties.ID);
        star.StarProperties.RotationOffset = Random.Range (35.0f, 55.0f);
        return star;
    }

    public static StarMap.State SpawnStarMap (StarGenerator.Parameters parameters) {
        StarMap.State mapState = new StarMap.State();
        StarMap.MapStateParameters mapStateParameters = new StarMap.MapStateParameters();

        int seed = parameters.LoadedSeedValue;
        if ( parameters.IsLoadedSeed == false ) {
            seed = parameters.UniqueSeedValue;
        }

        mapStateParameters.SeedState = RandomUtil.SetSeedState ( seed );
        mapStateParameters.SeedValue = seed;

        float proximityThreshold = parameters.Prefabs.StarPrefab.GetComponentSafe<CircleCollider2D> ().radius * parameters.StarNodeProximityFactor;
        int positionCalculationRetryCount = 0;
        int i = 0;
        while ( i < parameters.NumStarsToGenerate ) { // spawn non-overlapping positions within a defined radius
            while ( positionCalculationRetryCount <= parameters.MaxNumSpawnRetriesAllowed ) {
                Vector3 spawnPoint = RandomUtil.RandomVectorWithinRadius(parameters.MapScale);
                RaycastHit2D contact = Physics2D.CircleCast ( spawnPoint, proximityThreshold, Vector3.zero, Mathf.Infinity, StarMap.GeneratorParameters.NodeLayer );
                if ( contact.collider == null ) {
                    Star star = SpawnNewStar (spawnPoint);
                    SpawnNewNode (parameters.Prefabs.StarPrefab, parameters.Prefabs.StarContainer, star);
                    positionCalculationRetryCount = 0;
                    ++mapStateParameters.StarCount;
                    break;
                }
                ++positionCalculationRetryCount;
            }
            ++i;
        }

        RandomUtil.SetSeedStateDefault ();
        return mapState;
    }
}
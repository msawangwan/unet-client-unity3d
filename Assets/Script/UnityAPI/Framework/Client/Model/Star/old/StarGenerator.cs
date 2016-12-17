using UnityEngine;

namespace UnityAPI.Game {
    public static class StarGenerator {
        public class Parameters {
            public class SceneObjects {
                public GameObject StarPrefab { get; private set; }
                public Transform StarContainer { get; private set; }

                public SceneObjects (GameObject starPrefab, Transform starContainer) {
                    StarPrefab = starPrefab;
                    StarContainer = starContainer;
                }
            }

            public SceneObjects Prefabs { get; private set; }
            public bool IsLoadedSeed { get; private set; }
            public int LoadedSeedValue { get; private set; }
            public int UniqueSeedValue { get { return System.Environment.TickCount; } }
            public int NumStarsToGenerate { get; private set; }
            public int PositionCalculationMaxRetryAttemptsAllowed { get { return 20; } }
            public float MapScale { get; private set; } // i.e, map radius
            public float StarNodeProximityFactor { get; private set; } // i.e, node spacing

            public Parameters (GameObject starPrefab, Transform starContainer, bool isLoadedSeed, int loadedSeedValue, int numStarsToGenerate, float mapScale, float proximityFactor) {
                Prefabs = new SceneObjects (starPrefab, starContainer);
                IsLoadedSeed = isLoadedSeed;
                LoadedSeedValue = loadedSeedValue;
                NumStarsToGenerate = numStarsToGenerate;
                MapScale = mapScale;
                StarNodeProximityFactor = proximityFactor;
            }
        }

        public static StarMap_old.State GenerateStarMapState (StarGenerator.Parameters parameters) {
            StarMap_old.State mapState = new StarMap_old.State();

            int seed = parameters.LoadedSeedValue;
            if ( parameters.IsLoadedSeed == false ) {
                seed = parameters.UniqueSeedValue;
            }

            mapState.SeedState = RandomUtil.SetSeedStateWithValue ( seed );
            mapState.SeedValue = seed;
            mapState.InstanceID = 0; // todo: hash
            mapState.NodeCount = parameters.NumStarsToGenerate;
            mapState.Scale = parameters.MapScale;
            mapState.Density = parameters.StarNodeProximityFactor;

            GameObject starGameObject = parameters.Prefabs.StarPrefab;
            Transform starTransform = parameters.Prefabs.StarContainer;

            float proximityThreshold = starGameObject.GetComponentSafe<CircleCollider2D> ().radius * parameters.StarNodeProximityFactor;
            int positionCalculationRetryCount = 0;
            int positionCalculationMaxAttemptsAllowed = parameters.PositionCalculationMaxRetryAttemptsAllowed;

            if ( positionCalculationMaxAttemptsAllowed <= 0 ) {
                positionCalculationMaxAttemptsAllowed = 20; // default value, magic number
            }

            int i = 0;
            while ( i < parameters.NumStarsToGenerate ) { // spawn non-overlapping positions within a defined radius
                while ( positionCalculationRetryCount <= positionCalculationMaxAttemptsAllowed ) {
                    Vector3 spawnPoint = RandomUtil.RandomVectorWithinRadius(parameters.MapScale);
                    RaycastHit2D contact = Physics2D.CircleCast ( spawnPoint, proximityThreshold, Vector3.zero, Mathf.Infinity, GameConstant.Layer.Star );
                    if ( contact.collider == null ) {
                        InstantiateStarPrefab (starGameObject, starTransform, SpawnStar (spawnPoint) );
                        positionCalculationRetryCount = 0;
                        break;
                    }
                    ++positionCalculationRetryCount;
                }
                ++i;
            }

            RandomUtil.SetSeedStateDefault ();
            return mapState;
        }

        private static Star SpawnStar (Vector3 coordinates) {
            Star star = new Star();
            star.StarProperties = new Star.Properties();
            star.StarProperties.GalaxyCoordinate = coordinates;
            star.StarProperties.ID = Random.Range (0, 10000);
            star.StarProperties.Designation = string.Format ("star {0}", star.StarProperties.ID);
            star.StarProperties.RotationOffset = Random.Range (35.0f, 55.0f);
            return star;
        }

        private static GameObject InstantiatePlayerStartingStarPrefab (GameObject nodePrefab) {
            return InstantiateStarPrefab (null, null, new Star());
        }

        private static GameObject InstantiateStarPrefab (GameObject starPrefab, Transform starParent, Star star) {
            if (starPrefab != null) {
                Quaternion nodeRotation = star.StarProperties.RotationOffset.AsZRotation();
                GameObject nodeObject = MonoBehaviour.Instantiate ( starPrefab, star.StarProperties.GalaxyCoordinate, nodeRotation, starParent ) as GameObject;
                star.NodeObject = nodeObject.GetComponentSafe<StarNode> ();
                nodeObject.name = star.NodeObject.NameField = star.StarProperties.Designation;
                return nodeObject;
            }
            return null;
        }
    }
}
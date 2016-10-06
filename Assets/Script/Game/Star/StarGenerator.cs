using UnityEngine;

public static class StarGenerator {
    public static Transform NodeContainerTransform = null;

    public static GameObject SpawnStartNode (GameObject nodePrefab) {
        return SpawnNewNode (null, new Star());
    }

    public static GameObject SpawnNewNode (GameObject nodePrefab, Star star) {
        if (nodePrefab != null) {
            Quaternion nodeRotation = star.StarProperties.RotationOffset.AsZRotation();
            GameObject nodeObject = MonoBehaviour.Instantiate ( nodePrefab, star.StarProperties.GalaxyCoordinate, nodeRotation, StarGenerator.NodeContainerTransform ) as GameObject;
            star.NodeObject = nodeObject.GameObjectComponent<StarNode> ();
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

    public static StarMap.MapStateParameters SpawnStarField (StarMap.GeneratorParameters generatorProperties) {
        StarMap.MapStateParameters mapStateParameters = new StarMap.MapStateParameters();

        int seed = generatorProperties.CustomSeedValue;
        if ( generatorProperties.UseCustomSeedValue == false ) {
            seed = generatorProperties.UniqueSeedValue;
        }

        mapStateParameters.SeedState = RandomUtil.SetSeedState ( seed );
        mapStateParameters.SeedValue = seed;

        if ( StarGenerator.NodeContainerTransform == null ) {
            StarGenerator.NodeContainerTransform = generatorProperties.Prefabs.StarNodeContainerTransform;
        }

        float proximityThreshold = generatorProperties.Prefabs.StarPrefab.GetComponent<CircleCollider2D> ().radius * generatorProperties.StarDensity;
        int positionCalculationRetryCount = 0;
        int i = 0;
        while ( i < generatorProperties.NumberOfStars ) { // spawn non-overlapping positions within a defined radius
            while ( positionCalculationRetryCount <= generatorProperties.MaxRetryAttemptsAllowed ) {
                Vector3 spawnPoint = Random.insideUnitSphere * generatorProperties.GalaxyScale;
                spawnPoint.z = 0.0f; // todo: set sort order?
                RaycastHit2D contact = Physics2D.CircleCast ( spawnPoint, proximityThreshold, Vector3.zero, Mathf.Infinity, StarMap.GeneratorParameters.NodeLayer );
                if ( contact.collider == null ) {
                    Star star = SpawnNewStar (spawnPoint);
                    SpawnNewNode (generatorProperties.Prefabs.StarPrefab, star); // todo: return value to static list?
                    positionCalculationRetryCount = 0;
                    ++mapStateParameters.StarCount;
                    break;
                }
                ++positionCalculationRetryCount;
            }
            ++i;
        }

        RandomUtil.SetSeedStateDefault ();
        return mapStateParameters;
    }
}
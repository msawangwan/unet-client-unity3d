using UnityEngine;

public class GenerateStarMap : MonoBehaviour {
    public GameObject StarSymbolPrefab = null;

    public bool UseUniqueSeed = true;

    public int StarMapSeed = 3;
    public int NumberOfStars = 15;
    public int MaxSpawnAttempts = 20;

    public float StarMapRadius = 20.0f;
    public float NodeSpacing = 1.5f; // todo: come up with a formula for ensuring spacing doesn't interfere with spawn count

    private static Random.State starMapSeedState;
    private const int nodeLayer = 1 << 20;

    private static GameObject CreateStarNode (GameObject nodePrefab, Transform nodeParent, Vector3 nodePosition, string nodeName, float nodeRotationOffset = 0.0f) {
        Quaternion nodeRotation = Quaternion.Euler ( 0f, 0f, nodeRotationOffset );
        GameObject nodeObject = Instantiate ( nodePrefab, nodePosition, nodeRotation, nodeParent ) as GameObject;
        StarMapNode starNode = nodeObject.GameObjectComponent<StarMapNode> ();
        nodeObject.name = starNode.Name = nodeName;
        return nodeObject;
    }

    void SpawnHomeStar () {
        CreateStarNode(StarSymbolPrefab, null, Vector3.zero, "home-star");
    }
    // seed is being manipulated in this method
    void SpawnStarGalaxy () {
        if ( UseUniqueSeed == true ) {
            StarMapSeed = System.Environment.TickCount;
        }

        starMapSeedState = RandomUtil.SetSeedState ( StarMapSeed ); // see line 47

        Transform container = new GameObject("map_star-symbols").transform;
        float surroundingArea = StarSymbolPrefab.GetComponent<CircleCollider2D> ().radius * NodeSpacing; // todo: this getcomponent call ain't robust
        int attempts = 0;
        int i = 0;
        while ( i < NumberOfStars ) { // spawn non-overlapping positions within a defined radius
            while ( attempts <= MaxSpawnAttempts ) {
                Vector3 spawnPoint = Random.insideUnitSphere * StarMapRadius;
                spawnPoint.z = 0.0f; // todo: set sort order
                RaycastHit2D contact = Physics2D.CircleCast( spawnPoint, surroundingArea, Vector3.zero, Mathf.Infinity, nodeLayer );
                if ( contact.collider == null ) {
                    float rotationVariance = Random.Range(35.0f, 55.0f);
                    float starID = Random.Range(0, 10000);
                    string starName = string.Format ("star {0}", starID);
                    CreateStarNode(StarSymbolPrefab, container, spawnPoint, starName, rotationVariance);
                    attempts = 0;
                    break;
                }
                attempts++;
            }
            i++;
        }

        RandomUtil.SetSeedStateDefault ();
    }

    void Start () {
        if ( StarSymbolPrefab == null ) {
            Debug.LogAssertionFormat ( gameObject, "map star symbol prefab is null: {0}", StarSymbolPrefab == null );
        } else {
            SpawnHomeStar ();
            SpawnStarGalaxy ();
        }
    }
}

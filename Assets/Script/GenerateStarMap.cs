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

    // seed is being manipulated in this method
    void SpawnStarMapSymbols () {
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
                    Quaternion rotationVariance = Quaternion.Euler ( 0f, 0f, Random.Range ( 35.0f, 55.0f ) );
                    GameObject curr = Instantiate ( StarSymbolPrefab, spawnPoint, rotationVariance, container ) as GameObject;
                    MapStarNode node = curr.GameObjectComponent<MapStarNode>();
                    node.Name = string.Format ("star {0}", Random.Range(0, 10000));
                    curr.name = node.Name;
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
            SpawnStarMapSymbols ();
        }
    }
}

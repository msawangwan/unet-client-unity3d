using UnityEngine;

public class GenerateStarMap : MonoBehaviour {
    public GameObject StarSymbolPrefab = null;
    public int NumberOfStars = 15;
    public int MaxSpawnAttempts = 20;
    public float StarMapRadius = 20.0f;
    public float NodeSpacing = 1.5f; // todo: come up with a formula for ensuring spacing doesn't interfere with spawn count

    Quaternion rotationVariance { get { return Quaternion.Euler(0, 0, Random.Range ( 35.0f, 55.0f ) ); } }

    void SpawnStarMapSymbols () {
        Transform container = new GameObject("map_star-symbols").transform;
        float surroundingArea = StarSymbolPrefab.GetComponent<CircleCollider2D> ().radius * NodeSpacing; // todo: this getcomponent call ain't robust
        int attempts = 0;
        int i = 0;
        while ( i < NumberOfStars ) { // spawn non-overlapping positions within a defined radius
            while ( attempts <= MaxSpawnAttempts ) {
                Vector3 spawnPoint = Random.insideUnitSphere * StarMapRadius;
                spawnPoint.z = 0.0f;
                RaycastHit2D contact = Physics2D.CircleCast( spawnPoint, surroundingArea, Vector3.zero );
                if ( contact.collider == null ) {
                    GameObject curr = Instantiate ( StarSymbolPrefab, spawnPoint, rotationVariance, container ) as GameObject;
                    curr.name = string.Format ( "star symbol #{0}", i );
                    attempts = 0;
                    break;
                }
                attempts++;
            }
            i++;
        }
    }

    void Start () {
        if ( StarSymbolPrefab == null ) {
            Debug.LogAssertionFormat ( gameObject, "map star symbol prefab is null: {0}", StarSymbolPrefab == null );
        } else {
            SpawnStarMapSymbols ();
        }
    }
}

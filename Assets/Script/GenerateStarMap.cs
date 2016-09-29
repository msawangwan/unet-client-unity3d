using UnityEngine;

public class GenerateStarMap : MonoBehaviour {
    public GameObject StarSymbolPrefab = null;
    public int NumberOfStars = 10;
    public int MaxSpawnAttempts = 20;
    public float SpawnRadius = 5;

    Quaternion rotationVariance { get { return Quaternion.Euler(0, 0, Random.Range ( 35.0f, 45.0f ) ); } }

    void SpawnStarMapSymbols () {
        if (StarSymbolPrefab == null) {
            Debug.LogAssertionFormat(gameObject, "missing map star symbol prefab!");
        }
        Transform container = new GameObject("map_star-symbols").transform;
        float r = StarSymbolPrefab.GetComponent<CircleCollider2D>().radius; // todo: this ain't robust
        int attempts = 0;
        int i = 0;
        while (i < NumberOfStars) { // spawn non-overlapping positions within a defined radius
            while (attempts <= MaxSpawnAttempts) {
                Vector3 spawnPoint = Random.insideUnitSphere * SpawnRadius;
                spawnPoint.z = 0.0f;
                RaycastHit2D contact = Physics2D.CircleCast(spawnPoint, r, Vector3.zero);
                if ( contact.collider == null ) {
                    GameObject curr = Instantiate(StarSymbolPrefab, spawnPoint, rotationVariance, container) as GameObject;
                    curr.name = string.Format("star symbol #{0}", i);
                    attempts = 0;
                    i++;
                    break;
                }
                attempts++;
            }
        }
    }

    void Start () {
        SpawnStarMapSymbols ();
    }
}

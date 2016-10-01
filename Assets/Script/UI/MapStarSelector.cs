using UnityEngine;
using UnityEngine.UI;

public class MapStarSelector : MonoBehaviour {
    private static MapStarSelector s = null;
    private static bool isApplicationRunning = true;
    private static readonly object padlock = new Object();

    private MapStarSelector() {}

    public static MapStarSelector S {
        get {
            if (isApplicationRunning) {
                lock (padlock) {
                    if (s == null) {
                        s = GameObject.FindObjectOfType<MapStarSelector>();
                        if (s == null) {
                            GameObject instance = new GameObject("map_star-selector");
                            instance.GameObjectComponent<MapStarSelector>();
                        }
                        DontDestroyOnLoad(s);
                    }
                    return s;
                }   
            } else {
                Debug.LogWarningFormat(s, "[singleton err] attempted to call a singleton while application wasn't running")
                return null;
            }
        }
    }

    public GameObject SelectedTitle = null;
    //public event Action OnStarNodeSelected = null;

    private Text TitleText = null;

    void Start () {
        if (gameObject.activeInHierarchy == true) {
            gameObject.SetActive(false);
        }

        DontDestroyOnLoad(gameObject);

        TitleText = SelectedTitle.GetComponent<Text> ();
        Debug.AssertFormat(gameObject, "text component reference {0}", TitleText == null);
    }

    public void OnStarNodeSelect (MapStarNode starNode) {
        TitleText.text = starNode.Name;
        gameObject.transform.parent = starNode.transform;
        gameObject.transform.position = starNode.transform.position;
        gameObject.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class MapStarSelector : MonoBehaviour {
    private static MapStarSelector s = null;
    private static readonly object padlock = new Object();

    private MapStarSelector() {}

    public static MapStarSelector S {
        get {
            lock (padlock) {
                if (s == null) {
                    s = GameObject.FindObjectOfType<MapStarSelector>();
                }
                return s;
            }
        }
    }

    public GameObject SelectedTitle = null;
    //public event Action OnStarNodeSelected = null;

    private Text TitleText = null;

    void Start () {
        s = this;
        DontDestroyOnLoad(gameObject);

        if (gameObject.activeInHierarchy == true) {
            gameObject.SetActive(false);
        }

        TitleText = SelectedTitle.GetComponent<Text> ();
        Debug.AssertFormat(gameObject, "text component reference {0}", TitleText == null);
    }

    void OnDisable () {
        //gameObject.transform.parent = null;
    }

    public void OnStarNodeSelect (MapStarNode starNode) {
        TitleText.text = starNode.Name;
        gameObject.transform.parent = starNode.transform;
        gameObject.transform.position = starNode.transform.position;
        gameObject.SetActive(true);
    }
}

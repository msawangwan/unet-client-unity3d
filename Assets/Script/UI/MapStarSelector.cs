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

    public System.Action RaiseSelectorEnabled { get; set; }
    public System.Action RaiseSelectorDisabled { get; set; }

    private Text TitleText = null;

    void OnNullSelection () {
        gameObject.SetActive(false);
    }

    void OnNodeSelection (MapStarNode starNode) {
        if (starNode == null) {
            return;
        } else {
            if ( TitleText != null ) {
                TitleText.text = starNode.Name;
                gameObject.transform.position = starNode.transform.position;
                gameObject.SetActive(true);
                EventController.SafeInvoke(RaiseSelectorEnabled);
            }
        }
    }

    void Start () {
        s = this;
        DontDestroyOnLoad(gameObject);

        MapStarController.S.RaiseNodeSelected += OnNodeSelection;
        MapStarController.S.RaiseNodeDeselected += OnNullSelection;

        TitleText = SelectedTitle.GetComponent<Text> ();

        if (gameObject.activeInHierarchy == true) {
            gameObject.SetActive(false);
        }
    }
}

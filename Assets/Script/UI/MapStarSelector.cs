using UnityEngine;
using UnityEngine.UI;

public class MapStarSelector : MonoBehaviour {
    private static MapStarSelector s = null;
    public static MapStarSelector S {
        get {
            if (s == null) {
                s = GameObject.FindObjectOfType<MapStarSelector>();
                DontDestroyOnLoad(s.gameObject);
            }
            return s;
        }
    }

    public GameObject SelectedTitle = null;

    public System.Action RaiseSelectorEnabled { get; set; } // todo: make static
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
        MapStarController.RaiseNodeSelected += OnNodeSelection;
        MapStarController.RaiseNodeDeselected += OnNullSelection;

        TitleText = SelectedTitle.GetComponent<Text> ();

        if (gameObject.activeInHierarchy == true) {
            gameObject.SetActive(false);
        }
    }
}

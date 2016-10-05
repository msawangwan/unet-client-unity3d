using UnityEngine;
using UnityEngine.UI;

public class MapStarSelector : MonoBehaviour {
    public static MapStarSelector Instance = null;

    public GameObject SelectedTitle = null;

    public static System.Action RaiseSelectorEnabled { get; set; } // todo: make static
    public static System.Action RaiseSelectorDisabled { get; set; }

    private Text TitleText = null;

    void OnNullSelection () {
        gameObject.SetActive(false);
    }

    void OnNodeSelection (StarMapNode starNode) {
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
        Instance = CommonUtil.EnablePersistance ( this, gameObject );

        MapStarController.RaiseNodeSelected += OnNodeSelection;
        MapStarController.RaiseNodeDeselected += OnNullSelection;

        TitleText = SelectedTitle.GetComponent<Text> ();

        if (gameObject.activeInHierarchy == true) {
            gameObject.SetActive(false);
        }
    }
}

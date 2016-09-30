using UnityEngine;
using UnityEngine.UI;
using System;

public class MapStarSelector : MonoBehaviour {
    public static MapStarSelector s = null;

    public GameObject SelectedTitle = null;
    public event Action OnStarNodeSelected = null;

    private Text TitleText = null;

    void Start () {
        if (gameObject.activeInHierarchy == true) {
            gameObject.SetActive(false);
        }

        s = this;
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

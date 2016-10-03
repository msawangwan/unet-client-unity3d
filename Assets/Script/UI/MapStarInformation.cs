using UnityEngine;
using UnityEngine.UI;

public class MapStarInformation : MonoBehaviour {
	public static MapStarInformation S = null;

    public GameObject TextParentGameObject = null;

    private Text infoText = null;

    void OnNodeSelectDisplayInfo (MapStarNode star) {
		if (infoText == null) {
            return;
        } else {
            infoText.text = string.Format("star name: {0}\nfuel supply: {1}\no2 supply: {2}", star.StarNode.Name, star.StarNode.Fuel, star.StarNode.Oxygen);
            gameObject.transform.position = star.transform.position + new Vector3(4f, 0f, 0f);
            gameObject.SetActive(true);
        }
	}

	void OnNullSelection () {
        gameObject.SetActive(false);
    }

	void Start () {
        S = this;
        DontDestroyOnLoad(gameObject);

        MapStarController.S.RaiseNodeSelected += OnNodeSelectDisplayInfo;
        MapStarController.S.RaiseNodeDeselected += OnNullSelection;

        infoText = TextParentGameObject.GetComponent<Text>();

        if (gameObject.activeInHierarchy == true) {
            gameObject.SetActive(false);
        }
    }
}

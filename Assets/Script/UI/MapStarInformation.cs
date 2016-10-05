using UnityEngine;
using UnityEngine.UI;

public class MapStarInformation : MonoBehaviour {
    private static MapStarInformation s = null;
    public static MapStarInformation S { 
        get {
            if ( s == null ) {
                s = GameObject.FindObjectOfType<MapStarInformation>();
                DontDestroyOnLoad(s.gameObject);
            }
            return s;
        }
    }

    public GameObject TextParentGameObject = null;

    private Text infoText = null;

    private void OnNodeSelectDisplayInfo (StarMapNode star) {
		if (infoText == null) {
            return;
        } else {
            infoText.text = string.Format("star name: {0}\nfuel supply: {1}\no2 supply: {2}", star.StarNode.Name, star.StarNode.FuelSupply, star.StarNode.OxygenSupply);
            gameObject.transform.position = star.transform.position + new Vector3(4f, 0f, 0f);
            gameObject.SetActive(true);
        }
	}

	private void OnNullSelection () {
        gameObject.SetActive(false);
    }

    private void LoadSingletonInstance() {
        Debug.LogFormat ( gameObject, "loaded {0}", gameObject.name );
    }

	private void Start () {
        MapStarInformation.S.LoadSingletonInstance();

        MapStarController.RaiseNodeSelected += OnNodeSelectDisplayInfo;
        MapStarController.RaiseNodeDeselected += OnNullSelection;

        infoText = TextParentGameObject.GetComponent<Text>();

        if (gameObject.activeInHierarchy == true) {
            gameObject.SetActive(false);
        }
    }
}

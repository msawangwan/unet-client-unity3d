using UnityEngine;
using UnityEngine.UI;

public class StarDetailDisplayController : MonoBehaviour {
    public static StarDetailDisplayController StaticInstance = null;

    [System.Serializable]
    public class WorldCanvasGameObject {
        public GameObject StarLabel = null;
        public GameObject StarInfo = null;

        public bool PredicateAreNull { get { return StarLabel == null || StarInfo == null; } }

        public void RepositionRelativeToParent ( Vector3 offset ) {
            StarLabel.transform.localPosition = Vector3.zero;
            StarInfo.transform.localPosition = offset;
        }
    }

    [System.Serializable]
    public class WorldCanvasText {
        public Text StarLabelText = null;
        public Text StarInfoText = null;

        public bool PredicateAreNull { get { return StarLabelText == null || StarInfoText == null; } }

        public static System.Action RaiseOnUpdateUI { get; set; }
    }

    public WorldCanvasGameObject WorldCanvas = null;
    public WorldCanvasText WorldText = null;

    private Transform CachedTransform = null;

    private static readonly Vector3 InfoPanelPositionOffset = new Vector3 ( 4f, 0f, 0f );

    private void HandleOnSelection (Descriptor selected) {
        if ( selected != null ) {
            if (WorldCanvas.PredicateAreNull == false && WorldText.PredicateAreNull == false) {
                GameObject go = GameObjectUtil.GameObjectFromInterface ( selected );
                UpdateUI ( selected.NameField, selected.DescriptionField );
                UpdatePosition ( go.transform.position );
                ToggleCanvasGameObjectActiveState ( false );
            }
        } else {
            return;
        }
    }

    private void HandleOnClearSelection () {
        ToggleCanvasGameObjectActiveState ( true );
    }

    private void UpdatePosition ( Vector3 targetPosition ) {
        CachedTransform.position = targetPosition;
        WorldCanvas.RepositionRelativeToParent ( InfoPanelPositionOffset );
    }

    private void UpdateUI ( string title, string stats ) {
        UIMediator.UIData data = new UIMediator.UIData();
        data.Fields.Add(WorldText.StarLabelText, title);
        data.Fields.Add(WorldText.StarInfoText, stats);
        UIMediator.RaiseOnUIUpdated ( data );
    }

    private void ToggleCanvasGameObjectActiveState ( bool toggleIfActive ) {
        if ( toggleIfActive == true ) {
            WorldCanvas.StarLabel.SetActive ( false );
            WorldCanvas.StarInfo.SetActive ( false );
        } else {
            WorldCanvas.StarLabel.SetActive ( true );
            WorldCanvas.StarInfo.SetActive ( true );
        }
    }

    private bool PredicateAreAllActive () {
        return WorldCanvas.StarLabel.activeInHierarchy && WorldCanvas.StarInfo.activeInHierarchy;
    }

    private void Awake () {
        StaticInstance = CommonUtil.EnablePersistance ( this, gameObject );
    }

    private void Start () {
        CachedTransform = gameObject.transform;

        StarMapController.RaiseNodeSelected += HandleOnSelection;
        StarMapController.RaiseNodeDeselected += HandleOnClearSelection;

        ToggleCanvasGameObjectActiveState ( true );
    }
}

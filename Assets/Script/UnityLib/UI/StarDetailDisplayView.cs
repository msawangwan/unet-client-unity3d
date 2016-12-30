using UnityEngine;

public  class StarDetailDisplayView : MonoBehaviour {
    public static StarDetailDisplayView StaticInstance = null;

    private void HandleOnUIUpdated ( UIMediator.UIData data ) {
        if (data != null) {
            foreach (var field in data.Fields) { // iterate through key/value pairs
                field.Key.text = field.Value;
            }
        }
    }

    private void Awake () {
        // StaticInstance = CommonUtil.EnablePersistance ( this, gameObject );
    }

    private void Start () {
        UIMediator.RaiseOnUIUpdated += HandleOnUIUpdated;
    }
}

using UnityEngine;

public class Main : MonoBehaviour {
    public static Main StaticInstance = null;

    private void Awake () {
        StaticInstance = CommonUtil.EnablePersistance ( this, gameObject );
    }
}

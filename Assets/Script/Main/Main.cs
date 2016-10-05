using UnityEngine;

public class Main : MonoBehaviour {
    public static Main Instance = null;

    private void Awake () {
        Instance = CommonUtil.EnablePersistance ( this, gameObject );
    }
}

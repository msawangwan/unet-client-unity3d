using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController StaticInstance = null;

    private int tick = 0;

    public int UpdateTick { get { return tick; } }
    public bool IsControllerActive { get; private set; }

    public void EnableController () {
        IsControllerActive = true;
    }

    private void Awake () {
        StaticInstance = CommonUtil.EnablePersistance(this, gameObject);
    }

    private void Update () {
        if (IsControllerActive == true) {
            if (tick == 0) {
                
            }
        } else {
            return;
        }
    }
}

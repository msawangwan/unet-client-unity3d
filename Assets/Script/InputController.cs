using UnityEngine;

public class InputController : MonoBehaviour {
    void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.LogFormat("pressed up arrow test");
        }
    }
}

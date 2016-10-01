using UnityEngine;

public class PlasmaChainCannon : MonoBehaviour {
    public GameObject idleSprite = null;
    public GameObject attackSprite = null;
    public GameObject baseFaceUp = null;
    public GameObject baseFaceDown = null;

    //SpriteRenderer turretSpriteRenderer = null; // swap sprites or enable disable gameobjects?
    bool isIdle = true;

    void TrackTarget(Transform t, Vector3 target, float turnSpeed=215f) {
		float step = turnSpeed * Time.deltaTime;
		Vector3 ad = target - t.position;
		float ang = Mathf.Atan2(ad.y, ad.x) * 180/Mathf.PI;
		t.rotation = Quaternion.RotateTowards(t.rotation, Quaternion.Euler(0f,0f,ang-90), step);
	}

    void Update() {
        TrackTarget(gameObject.transform, MousePointer.ScreenCoordinates());
        if (Input.GetMouseButtonDown(0) == true) {
            ToggleState();
        }
    }

    void ToggleState() {
        if (isIdle == true) {
            attackSprite.SetActive(true);
            idleSprite.SetActive(false);
            isIdle = false;
        } else {
            attackSprite.SetActive(false);
            idleSprite.SetActive(true);
            isIdle = true;
        }
    }
}

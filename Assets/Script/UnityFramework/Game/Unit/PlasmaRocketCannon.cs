using UnityEngine;
using System.Collections;

public class PlasmaRocketCannon : MonoBehaviour {

    const string state = "state";
    bool firing = false;

    Animator animator = null;

    void Start () {
        animator = GetComponent<Animator>();
    }

    void TrackTarget(Transform t, Vector3 target, float turnSpeed=215f) {
		float step = turnSpeed * Time.deltaTime;
		Vector3 ad = target - t.position;
		float ang = Mathf.Atan2(ad.y, ad.x) * 180/Mathf.PI;
		t.rotation = Quaternion.RotateTowards(t.rotation, Quaternion.Euler(0f,0f,ang-90), step);
	}

    void Update() {
        // TrackTarget(gameObject.transform, MousePointer.ScreenCoordinates());
        if (Input.GetMouseButtonDown(0) == true && firing == false) {
            animator.SetInteger(state, 1);
            StartCoroutine(ReloadDelay(0.5f));
            firing = true;
        }
    }

    IEnumerator ReloadDelay (float delay) {
        yield return new WaitForSeconds (delay);
        animator.SetInteger(state, 0);
        firing = false;
    }
}

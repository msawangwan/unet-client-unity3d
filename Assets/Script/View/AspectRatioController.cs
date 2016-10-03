using UnityEngine;

public class AspectRatioController : MonoBehaviour {

    public float TargetWidth = 16.0f;
    public float TargetHeight = 9.0f;

    private float targetAspect { get { return TargetWidth / TargetHeight; } }
	private float windowAspect { get { return Screen.width / Screen.height; } }
	private float scaleHeight { get { return windowAspect / targetAspect; } }
	private float scaleWidth { get { return 1.0f / scaleHeight; } }

	void Start () {
        Camera cam = Camera.main;
        Rect rect = cam.rect;
        if (scaleHeight < 1.0f) {
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        } else {
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
        }
        cam.rect = rect;
    }

}

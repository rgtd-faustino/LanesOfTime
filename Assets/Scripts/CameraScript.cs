using UnityEngine;

public class CameraScript : MonoBehaviour {
    // 16:9 because 1920x1080
    public float referenceAspect = 16f / 9f;
    public float referenceSize = 5f;

    private void Awake() {
        Camera cam = GetComponent<Camera>();

        float currentAspect = (float)Screen.width / Screen.height;

        // same width on every screen
        cam.orthographicSize = referenceSize * referenceAspect / currentAspect;
    }
}

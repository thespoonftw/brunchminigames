using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CameraController : MonoBehaviour {

    public Camera cameraComponent;
    public GameObject cameraGameobject;

    private GameObject focusObject;
    private Vector3 focusPoint;

    private float shakeDurationRemaining;
    private float shakeIntensity;

    // Set target gameobject for camera
    public void SetFocus(GameObject focus) {
        this.focusObject = focus;
    }

    // Apply screenshake for a time
    public void Screenshake(float intensity, float duration) {
        shakeDurationRemaining = duration;
        shakeIntensity = intensity;
    }

    private void Start() {
        focusPoint = gameObject.transform.position;
        cameraGameobject = transform.GetChild(0).gameObject;
        cameraComponent = cameraGameobject.GetComponent<Camera>();
    }

    private void Update() {

        if (focusObject != null) {
            focusPoint = focusObject.transform.position;
        }

        var shakeOffset = new Vector3();
        if (shakeDurationRemaining > 0) {
            shakeDurationRemaining -= Time.deltaTime;
            if (shakeDurationRemaining >= 0) {
                shakeOffset = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity));
            }
        }

        transform.position = focusPoint + shakeOffset;

    }

}
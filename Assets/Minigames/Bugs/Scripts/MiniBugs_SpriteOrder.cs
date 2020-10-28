using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBugs_SpriteOrder : MonoBehaviour {
    public float CloseZ = 5f;
    public float FarZ = 7f;

    private float w;
    private float h;
    private Camera mainCamera;

    void Start() {
        mainCamera = Camera.main;
        w = mainCamera.orthographicSize * Screen.width / Screen.height;
        h = mainCamera.orthographicSize;
    }

    void Update() {
        Vector3 pos = transform.position;
        pos.z = Mathf.Lerp(CloseZ, FarZ, ((pos.y - mainCamera.transform.position.y) + h) / (2f * h));
        transform.position = pos;
    }
}

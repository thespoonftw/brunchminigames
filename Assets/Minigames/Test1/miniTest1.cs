﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniTest1 : MonoBehaviour {

    private float counter = 0;
    public GameObject gameCamera;

    void Start() {
        CameraManager.Instance.SetMainCamera(gameCamera.GetComponent<Camera>());
    }

    void Update() {
        counter += Time.deltaTime;
        if (counter > 3) {
            counter -= 3;
            CameraManager.Instance.MainCamera.Screenshake(1, 0.25f);
        }
    }
}

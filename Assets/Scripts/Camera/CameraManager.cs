using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CameraManager : Singleton<CameraManager> {

    // Get camera 0
    public CameraController MainCamera { get { return GetCamera(0); } }

    private Camera[] cameraArray = new Camera[4];
    private CameraController[] cameraControllerArray = new CameraController[4];

    // Get camera controller for player index
    public CameraController GetCamera(int index = 0) {
        return cameraControllerArray[index];
    }

    // Register main camera, clear other cameras
    public CameraController SetMainCamera(Camera camera) {
        cameraArray[0] = camera;
        cameraArray[1] = null;
        cameraArray[2] = null;
        cameraArray[3] = null;
        UpdateArrangement();
        var go = new GameObject("Main Camera Controller");
        camera.gameObject.transform.parent = go.transform;
        var controller = go.AddComponent<CameraController>();
        cameraControllerArray[0] = controller;
        cameraControllerArray[1] = null;
        cameraControllerArray[2] = null;
        cameraControllerArray[3] = null;
        return controller;
    }

    // Register player camera
    public CameraController SetPlayerCamera(Camera camera, int index) {
        if (index > 0) { camera.GetComponent<AudioListener>().enabled = false; }
        camera.gameObject.SetActive(true);        
        var go = new GameObject("Player " + index + " Camera Controller");
        camera.gameObject.transform.parent = go.transform;
        var component = go.AddComponent<CameraController>();
        cameraArray[index] = camera;
        UpdateArrangement();
        cameraControllerArray[index] = component;
        return component;
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    private void UpdateArrangement() {
        var cameraList = new List<Camera>();
        foreach (var c in cameraArray) {
            if (c != null) {
                cameraList.Add(c);
            }            
        }
        if (cameraList.Count == 1) {
            cameraList[0].rect = new Rect(0, 0, 1, 1);

        } else if (cameraList.Count == 2) {
            cameraList[0].rect = new Rect(0.25f, 0.5f, 0.5f, 0.5f);
            cameraList[1].rect = new Rect(0.25f, 0,    0.5f, 0.5f);

        } else if (cameraList.Count == 3) {
            cameraList[0].rect = new Rect(0.25f, 0.5f, 0.5f, 0.5f);
            cameraList[1].rect = new Rect(0,     0,    0.5f, 0.5f);
            cameraList[2].rect = new Rect(0.5f,  0,    0.5f, 0.5f);

        } else if (cameraList.Count == 4) {
            cameraList[0].rect = new Rect(0,    0.5f, 0.5f, 0.5f);
            cameraList[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            cameraList[2].rect = new Rect(0,    0,    0.5f, 0.5f);
            cameraList[3].rect = new Rect(0.5f, 0,    0.5f, 0.5f);
        }

    }



}
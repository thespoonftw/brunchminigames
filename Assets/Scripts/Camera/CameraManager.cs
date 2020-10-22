using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CameraManager : Singleton<CameraManager> {

    static Camera[] cameraArray = new Camera[4];

    public static CameraController GetCamera(int index = 0) {

        if (cameraArray[index] == null) {
            index = 0;
        }

        if (cameraArray[index].TryGetComponent(out CameraController c)) {
            return c;
        } else {
            return cameraArray[index].gameObject.AddComponent<CameraController>(); ;
        }

    }

    public static void SetMainCamera(Camera camera) {
        cameraArray[0] = camera;
        cameraArray[1] = null;
        cameraArray[2] = null;
        cameraArray[3] = null;
    }

    public static void SetPlayerCamera(Camera camera, int index) {
        cameraArray[index] = camera;
        camera.gameObject.SetActive(true);
        UpdateArrangement();
    }

    private static void UpdateArrangement() {
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
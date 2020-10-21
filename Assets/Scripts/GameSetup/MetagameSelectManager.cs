using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MetagameSelectManager : MonoBehaviour {

    public void Start() {
        
    }

    private void Update() {
        var gamepad = Gamepad.current;
        if (gamepad.aButton.wasPressedThisFrame) {
            LoadMetagame("metaLinear");   
        }
        if (gamepad.bButton.wasPressedThisFrame) {
            LoadMetagame("metaAsteroids");
        }
    }

    private void LoadMetagame(string metagameName) {
        var mm = MetagameManager.Instance;
        mm.Initialise(metagameName);
        Debug.Log("metagame selected: " + metagameName);
        SceneManager.LoadScene(metagameName);
    }

}

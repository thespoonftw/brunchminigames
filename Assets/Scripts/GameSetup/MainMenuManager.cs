using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    void Update() {
        var gamepad = Gamepad.current;
        if (gamepad.aButton.wasPressedThisFrame) {
            SceneManager.LoadScene("selectCharacter");
        }
    }
}

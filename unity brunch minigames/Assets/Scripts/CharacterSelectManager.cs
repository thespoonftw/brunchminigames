using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CharacterSelectManager : MonoBehaviour {
    private List<Gamepad> gamepads = new List<Gamepad>();

    public void Start() {
        Debug.Log("character selected!");
        // GameManager.Instance.StartMetagameSelection();
    }

    public void Update() {
        foreach (Gamepad g in Gamepad.all) {
            if (!gamepads.Contains(g) && g.aButton.wasPressedThisFrame) {
                gamepads.Add(g);

                Player p = PlayerManager.RegisterPlayer(g);
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            Player p = PlayerManager.RegisterPlayer(Keyboard.current);
        }

        if (gamepads.Count >= 1) {
            SceneManager.LoadScene("selectMetagame");
        }
    }
}

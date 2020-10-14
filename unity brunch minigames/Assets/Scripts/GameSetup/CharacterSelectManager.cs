using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour {

    private List<Gamepad> gamepads = new List<Gamepad>();
    [SerializeField] Text playerListText;

    public void Start() {
        playerListText.text = "";
    }

    private void FixedUpdate() {
        var stringBuilder = "";
        for (int i = 0; i < PlayerManager.PlayerCount; i++) {
            var p = PlayerManager.GetPlayer(0);
            var type = p.isKeyboard ? "Keyboard" : "Xbox";
            stringBuilder += "Player " + (i + 1) + ": " + type + "\n";
        }
        playerListText.text = stringBuilder;
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
        
        var gamepad = Gamepad.current;
        if (gamepad.bButton.wasPressedThisFrame && PlayerManager.PlayerCount > 0) {
            SceneManager.LoadScene("selectMetagame");
        }
    }
}

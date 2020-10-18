using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour {

    private List<Gamepad> gamepads = new List<Gamepad>();
    private List<Keyboard> keyboards = new List<Keyboard>();
    [SerializeField] Text playerListText;

    public void Start() {
        playerListText.text = "";
        
        // If PlayerManager.IsTestMode is set to false, it means we've gone via Character Select screen.
        // This means we have the actual list of players.
        // Otherwise, when asked PlayerManager will just use the current list of gamepads.
        PlayerManager.IsTestMode = false;
    }

    private void FixedUpdate() {
        var stringBuilder = "";
        for (int i = 0; i < PlayerManager.PlayerCount; i++) {
            var p = PlayerManager.GetPlayer(i);
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

        if (!keyboards.Contains(Keyboard.current)) {
            if (Keyboard.current.spaceKey.wasPressedThisFrame) {
                Player p = PlayerManager.RegisterPlayer(Keyboard.current);
            }
            keyboards.Add(Keyboard.current);
        }
        
        var gamepad = Gamepad.current;
        if (gamepad.bButton.wasPressedThisFrame && PlayerManager.PlayerCount > 0) {
            SceneManager.LoadScene("selectMetagame");
        }
    }
}

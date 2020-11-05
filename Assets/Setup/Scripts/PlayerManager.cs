using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour {
    public static Color[] PlayerColors = new Color[]{
        new Color(126f/255f, 243f/255f, 126f/255f, 1f),
        new Color(234f/255f, 63f/255f, 52f/255f, 1f),
        new Color(255f/255f, 247f/255f, 49f/255f, 1f),
        new Color(113f/255f, 43f/255f, 129f/255f, 1f)
    };
    public static Color[] PlayerSecondaryColors = new Color[]{
        new Color(255f/255f, 255f/255f, 255f/255f, 1f),
        new Color(255f/255f, 255f/255f, 255f/255f, 1f),
        new Color(239f/255f, 97f/255f, 0f/255f, 1f),
        new Color(255f/255f, 255f/255f, 255f/255f, 1f)
    };

    //public static event Action OnAPress;
    //public static event Action<float> OnXAxis;
    //public static event Action<float> OnYAxis;
    public static int PlayerCount { get { return players.Count; } }
    public static bool IsTestMode = true;

    private static List<Player> players = new List<Player>();
    private static int id = 0;

    public static Player RegisterPlayer(InputDevice input) {
        Player player = new Player(input, id);
        Debug.Log("registered a new player");
        id++;
        players.Add(player);
        
        return player;
    }

    public static Player GetPlayer(int index) {
        if (index >= players.Count) { return null; } 
        return players[index];
    }

    public static List<Player> GetPlayers(bool copy = true) {
        // If it's test mode then just cheat and initialise players with the current list of gamepads.
        if (IsTestMode) {
            players = new List<Player>();
            foreach (Gamepad g in Gamepad.all) {
                Player p = PlayerManager.RegisterPlayer(g);
            }
        }

        if (copy == true) {

            return new List<Player>(players);

        }
        else {

            return players;

        }

    }

    public static void DebugNumPlayers() {
        Debug.Log(players.Count);
    }

    void Update()  {
        foreach (Gamepad g in Gamepad.all) {
            // Debug.Log(g);
        }

        //if (input.getkeydown("joystick button 0")) {
        //    OnAPress?.Invoke();
        //}
        //if (Input.GetAxis("Horizontal") != 0) {
        //    OnXAxis?.Invoke(Input.GetAxis("Horizontal"));
        //}
        //if (Input.GetAxis("Vertical") != 0) {
        //    OnYAxis?.Invoke(Input.GetAxis("Vertical"));
        //}
    }
}

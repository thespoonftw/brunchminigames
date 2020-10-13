using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour {

    public static event Action OnAPress;
    public static event Action<float> OnXAxis;
    public static event Action<float> OnYAxis;

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
        return players[index];
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

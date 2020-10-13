﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player
{
    public int id;
    public bool isKeyboard;
    // public InputDevice input;
    public Gamepad gamepad;
    public Keyboard keyboard;

    public Player(InputDevice input, int id) {
        this.id = id;
        // this.input = input;
        this.isKeyboard = input.description.deviceClass == "Keyboard";
        if (this.isKeyboard) {
            this.keyboard = (Keyboard) input;
        } else {
            this.gamepad = (Gamepad) input;
        }
        Debug.Log("made a new player that's " + (this.isKeyboard ? "using a keyboard" : "not using a keyboard"));
    }

    public void OnActionButtonDown() {

    }

    // Get current axis this frame.
    public Vector3 GetInputAxis() {
        if (isKeyboard) {
            Vector3 dir = new Vector3();
            if (keyboard.leftArrowKey.isPressed) dir.x -= 1f;
            if (keyboard.rightArrowKey.isPressed) dir.x += 1f;
            if (keyboard.upArrowKey.isPressed) dir.y += 1f;
            if (keyboard.downArrowKey.isPressed) dir.y -= 1f;

            return dir;
        } else {
            return new Vector3(
                gamepad.leftStick.x.ReadValue(),
                gamepad.leftStick.y.ReadValue(),
                0f
            );
        }
    }
}

using System.Collections;
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

    public bool WasActionButtonPressedThisFrame() {
        return gamepad.aButton.wasPressedThisFrame;
    }

    public bool WasLeftPressedThisFrame() {
        if (isKeyboard) {
            return keyboard.leftArrowKey.wasPressedThisFrame;
        }
        else {
            return gamepad.leftStick.left.wasPressedThisFrame;
        }
    }

    public bool WasRightPressedThisFrame() {
        if (isKeyboard) {
            return keyboard.rightArrowKey.wasPressedThisFrame;
        }
        else {
            return gamepad.leftStick.right.wasPressedThisFrame;
        }
    }

    public bool WasUpPressedThisFrame() {
        if (isKeyboard) {
            return keyboard.upArrowKey.wasPressedThisFrame;
        }
        else {
            return gamepad.leftStick.up.wasPressedThisFrame;
        }
    }

    public bool WasDownPressedThisFrame() {
        if (isKeyboard) {
            return keyboard.downArrowKey.wasPressedThisFrame;
        }
        else {
            return gamepad.leftStick.down.wasPressedThisFrame;
        }
    }

    public bool IsLeftPressed() {
        if (isKeyboard) {
            return keyboard.leftArrowKey.isPressed;
        }
        else {
            return gamepad.leftStick.left.isPressed;
        }
    }

    public bool IsRightPressed() {
        if (isKeyboard) {
            return keyboard.rightArrowKey.isPressed;
        }
        else {
            return gamepad.leftStick.right.isPressed;
        }
    }

    public bool IsUpPressed() {
        if (isKeyboard) {
            return keyboard.upArrowKey.isPressed;
        }
        else {
            return gamepad.leftStick.up.isPressed;
        }
    }

    public bool IsDownPressed() {
        if (isKeyboard) {
            return keyboard.downArrowKey.isPressed;
        }
        else {
            return gamepad.leftStick.down.isPressed;
        }
    }


    public bool IsActionButtonPressed() {
        return gamepad.aButton.isPressed;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BallControl : MonoBehaviour {

    [SerializeField]
    public float speed;

    public bool ballComplete = false;
    public int xCoordinate;
    public int zCoordinate;
    public readonly float ballOffset = 0.5f;

    private GenerateMaze maze;
    private Transform[,,] walls;
    private int mazeWidth;
    private int mazeHeight;

    private void Start() {
        maze = transform.parent.GetComponent<GenerateMaze>();
        mazeWidth = maze.mazeWidth;
        mazeHeight = maze.mazeHeight;
        walls = maze.walls;
    }

    private void Update() {
        var gamepad = Gamepad.current;
        Vector3 movement;
        Vector3 newPosition;

        if (gamepad == null)
            return;

        if (gamepad.leftStick.left.wasPressedThisFrame) {
            movement = Vector3.left;
            newPosition = transform.localPosition + movement;
        }

        else if (gamepad.leftStick.right.wasPressedThisFrame) {
            movement = Vector3.right;
            newPosition = transform.localPosition + movement;
        }

        else if (gamepad.leftStick.up.wasPressedThisFrame) {
            movement = Vector3.forward;
            newPosition = transform.localPosition + movement;
        }

        else if (gamepad.leftStick.down.wasPressedThisFrame) {
            movement = Vector3.back;
            newPosition = transform.localPosition + movement;
        }

        else {
            movement = new Vector3(0.0f, 0.0f, 0.0f);
            newPosition = transform.localPosition + movement;
        }

        xCoordinate = (int)(Mathf.Max(transform.localPosition.x, newPosition.x) + mazeWidth / 2 - ballOffset);
        zCoordinate = (int)(Mathf.Max(transform.localPosition.z, newPosition.z) + mazeHeight / 2 - ballOffset);

        if (ballComplete == false && walls[xCoordinate, zCoordinate, (int)Mathf.Abs(movement.x)] == null) {
            transform.localPosition = newPosition;
        }

    }

    //void FixedUpdate() {
    //    var gamepad = Gamepad.current;
    //    Vector3 movement;

    //    if (gamepad == null)
    //        return;

    //    movement = new Vector3(gamepad.leftStick.ReadValue().x, 0, gamepad.leftStick.ReadValue().y);

    //    GetComponent<Rigidbody>().AddForce(movement * speed);

    //}
}

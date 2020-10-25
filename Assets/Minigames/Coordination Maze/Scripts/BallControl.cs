using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallControl : MonoBehaviour {

    [SerializeField]
    public float speed;

    public bool ballComplete = false;
    public int xCoordinate;
    public int zCoordinate;
    public readonly float ballOffset = 0.5f;
    public int playerCount = 0;
    public int seed = 0;

    private GameState gameState;
    private GenerateMaze maze;
    private Transform[,,] walls;
    private int mazeWidth;
    private int mazeHeight;
    private int inputCount = 4;

    List<Player> players = new List<Player>();
    List<Player> playersRng = new List<Player>();

    private void Start() {
        gameState = transform.parent.parent.GetComponent<GameState>();
        maze = transform.parent.GetComponent<GenerateMaze>();
        mazeWidth = maze.mazeWidth;
        mazeHeight = maze.mazeHeight;
        walls = maze.walls;
        players = PlayerManager.GetPlayers(copy:true);

        if (players.Count == 2) {
            players.Add(players[0]);
            players.Add(players[1]);
        }

        var rng = new System.Random(gameState.ballSeed);

        for (int i = 0; i < inputCount; i++) {
            var randomPlayer = rng.Next(players.Count);
            playersRng.Add(players[randomPlayer]);
            players.RemoveAt(randomPlayer);
        }

    }

    private void Update() {
        Vector3 movement;
        Vector3 newPosition;

        if (playersRng[0].WasLeftPressedThisFrame() == true) {
            movement = Vector3.left;
            newPosition = transform.localPosition + movement;
        }

        else if (playersRng[1].WasRightPressedThisFrame() == true) {
            movement = Vector3.right;
            newPosition = transform.localPosition + movement;
        }

        else if (playersRng[2].WasUpPressedThisFrame() == true) {
            movement = Vector3.forward;
            newPosition = transform.localPosition + movement;
        }

        else if (playersRng[3].WasDownPressedThisFrame() == true) {
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

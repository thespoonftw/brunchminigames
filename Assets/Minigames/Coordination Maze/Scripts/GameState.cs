using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class GameState : MonoBehaviour
{

    [SerializeField, Range(1,4)]
    const int mazeCount = 4;

    [SerializeField]
    Transform mazePrefab = default;

    [SerializeField]
    Vector3[] spawnPositions = default;

    public Transform[] mazes;
    public Transform[] balls;

    public readonly int mazeWidth = 10;
    public readonly int mazeHeight = 10;

    private Transform newMaze;
    private BallControl ballControl;
    private GenerateMaze mazeScript;

    // Awake is called for all scripts before Start is called on any script
    private void Awake() {

        mazes = new Transform[mazeCount];
        balls = new Transform[mazeCount];

        spawnPositions[0] = new Vector3(-((float)mazeWidth / 2 + 1.0f), 0, (float)mazeHeight / 2 + 1.0f);
        spawnPositions[1] = new Vector3((float)mazeWidth / 2 + 1.0f, 0, (float)mazeHeight / 2 + 1.0f);
        spawnPositions[2] = new Vector3(-((float)mazeWidth / 2 + 1.0f), 0, -((float)mazeHeight / 2 + 1.0f));
        spawnPositions[3] = new Vector3((float)mazeWidth / 2 + 1.0f, 0, -((float)mazeHeight / 2 + 1.0f));

        for (int i = 0; i < mazeCount; i++) {
            newMaze = Instantiate(mazePrefab);
            newMaze.name = "Maze_" + i;
            newMaze.SetParent(transform, false);
            newMaze.localPosition = spawnPositions[i];
            mazeScript = newMaze.GetComponent<GenerateMaze>();
            mazeScript.mazeID = i;
            mazeScript.mazeWidth = mazeWidth;
            mazeScript.mazeHeight = mazeHeight;
            mazes[i] = newMaze;

        }

    }

    // Update is called once per frame
    void Update()
    {

        int ballsComplete = 0;

        for (int i = 0; i < mazeCount; i++) {
            ballControl = balls[i].GetComponent<BallControl>();
            if (ballControl.xCoordinate >= mazeWidth - 1 && ballControl.zCoordinate >= mazeHeight - 1) {
                ballControl.ballComplete = true;
                ballsComplete += 1;
            }
        }

        if (ballsComplete == mazeCount) {
            Debug.Log("Game Won");
        }
    }
}

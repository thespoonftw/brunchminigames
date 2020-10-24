using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{

    [SerializeField]
    public int mazeID = default;

    [SerializeField, Range(5, 25)]
    public int mazeHeight = default;

    [SerializeField, Range(5, 25)]
    public int mazeWidth = default;

    [SerializeField]
    Transform mazeFloorPrefab = default;

    [SerializeField]
    Transform ballPrefab = default;

    [SerializeField]
    Transform wallPrefabInnerHorizontal = default;

    [SerializeField]
    Transform wallPrefabInnerVertical = default;

    [SerializeField]
    Transform wallPrefabOuterHorizontal = default;

    [SerializeField]
    Transform wallPrefabOuterVertical = default;

    private GameState gameState;

    float outerWallScaleY = 0.8f;
    float outerWallScaleZ = 0.4f;

    public Transform[,,] walls;
    List<Vector3> unusedCells = new List<Vector3>();
    List<Vector3> usedCells = new List<Vector3>();
    List<Vector3> currentPath = new List<Vector3>();

    Transform ball;
    Vector3 startPosition;

    Camera cam;
    Plane[] planes;
    Collider objCollider;
    GameObject obj;

    // Start is called before the first frame update
    void Start() {

        gameState = transform.parent.GetComponent<GameState>();

        float mazeTopZ = (float)mazeHeight / 2;
        float mazeBotZ = - (float)mazeHeight / 2;
        float mazeLeftX = - (float)mazeWidth / 2;
        float mazeRightX = (float)mazeWidth / 2;

        Transform mazeFloor = Instantiate(mazeFloorPrefab);
        mazeFloor.SetParent(transform, false);
        mazeFloor.localPosition = Vector3.zero;
        mazeFloor.localScale = new Vector3(mazeWidth + 2 * outerWallScaleZ, mazeFloor.localScale.y, mazeHeight + 2 * outerWallScaleZ);

        walls = new Transform[(int)mazeWidth + 1, (int)mazeHeight + 1, 2];

        Transform topWall = Instantiate(wallPrefabOuterHorizontal);
        topWall.SetParent(transform, false);
        topWall.localPosition = new Vector3(0.0f , outerWallScaleY / 2, (mazeHeight + outerWallScaleZ) / 2);
        topWall.localScale = new Vector3(mazeFloor.localScale.x, outerWallScaleY, outerWallScaleZ);
        Transform botWall = Instantiate(wallPrefabOuterHorizontal);
        botWall.SetParent(transform, false);
        botWall.localPosition = new Vector3(0.0f, outerWallScaleY / 2, -(mazeHeight + outerWallScaleZ) / 2);
        botWall.localScale = new Vector3(mazeFloor.localScale.x, outerWallScaleY, outerWallScaleZ);
        Transform leftWall = Instantiate(wallPrefabOuterVertical);
        leftWall.SetParent(transform, false);
        leftWall.localPosition = new Vector3(-(mazeWidth + outerWallScaleZ) / 2, outerWallScaleY / 2, 0.0f);
        leftWall.localScale = new Vector3(mazeFloor.localScale.z, outerWallScaleY, outerWallScaleZ);
        Transform rightWall = Instantiate(wallPrefabOuterVertical);
        rightWall.SetParent(transform, false);
        rightWall.localPosition = new Vector3((mazeWidth + outerWallScaleZ) / 2, outerWallScaleY / 2, 0.0f);
        rightWall.localScale = new Vector3(mazeFloor.localScale.z, outerWallScaleY, outerWallScaleZ);

        for (int i = 0; i <= mazeWidth; i++) {
            for (int j = 0; j <= mazeHeight; j++) {
                if (i < mazeWidth) {
                    Transform wallHorizontal = Instantiate(wallPrefabInnerHorizontal);
                    wallHorizontal.SetParent(transform, false);
                    wallHorizontal.localPosition = new Vector3(mazeLeftX + i + 0.5f, topWall.localPosition.y, mazeBotZ + j);
                    wallHorizontal.localScale = new Vector3(1.0f, topWall.localScale.y, 0.1f);
                    wallHorizontal.name = "wall_" + i + "_" + j + "_" + "_horizontal";
                    walls[i, j, 0] = wallHorizontal;
                }

                if (j < mazeHeight) {
                    Transform wallVertical = Instantiate(wallPrefabInnerVertical);
                    wallVertical.SetParent(transform, false);
                    wallVertical.localPosition = new Vector3(mazeLeftX + i, topWall.localPosition.y, mazeBotZ + j + 0.5f);
                    wallVertical.localScale = new Vector3(1.0f, topWall.localScale.y, 0.1f);
                    wallVertical.name = "wall_" + i + "_" + j + "_" + "_vert";
                    walls[i, j, 1] = wallVertical;
                }

                if (i < mazeWidth && j < mazeHeight) {
                    unusedCells.Add(new Vector3((float) i, 0.0f, (float) j));
                }

            }
        }

        Debug.Log((DateTime.Now.Ticks - DateTime.Today.Ticks) % 1000000);
        var rng = new System.Random();
        Vector3 movement = new Vector3 (0.0f, 0.0f, 0.0f);

        usedCells.Add(unusedCells[0]);
        unusedCells.Remove(unusedCells[0]);

        while (unusedCells.Count > 0) {
        
            //Start at a random new cell in the set of cells not in the maze
            Vector3 newCell = unusedCells[rng.Next(unusedCells.Count)];
            currentPath.Add(newCell);

            while (usedCells.Intersect(currentPath).Count() == 0) {
                //Choose a random direction to go in next
                switch (rng.Next(4)) {
                    case 0:
                        movement = Vector3.forward;
                        break;
                    case 1:
                        movement = Vector3.right;
                        break;
                    case 2:
                        movement = Vector3.back;
                        break;
                    case 3:
                        movement = Vector3.left;
                        break;
                }

                //Get the next cell based on movement in the random direction
                newCell += movement;

                if(newCell.x < 0 || newCell.z < 0 || newCell.x > mazeWidth - 1 || newCell.z > mazeHeight - 1) {
                    newCell -= movement;
                }
                else {
                    //If the new cell creates a loop, i.e the new cell is already in the current path,
                    //remove all cells back to and including the first instance of the new cell to
                    //remove the loop. Then add in the new cell
                    while (currentPath.Contains(newCell)) {
                        currentPath.RemoveAt(currentPath.Count - 1);
                    }
                    currentPath.Add(newCell);
                }
            
            }

            for (int i = 0; i < currentPath.Count - 1; i++) {
                movement = currentPath[i] - currentPath[i + 1];
                int xCoordinate = Mathf.Max((int)currentPath[i].x, (int)currentPath[i + 1].x);
                int zCoordinate = Mathf.Max((int)currentPath[i].z, (int)currentPath[i + 1].z);
                Destroy(walls[xCoordinate, zCoordinate, (int)Mathf.Abs(movement.x)].gameObject);
            }

            usedCells = usedCells.Union(currentPath).ToList();
            unusedCells = unusedCells.Except(currentPath).ToList();
            currentPath.Clear();

        }

        ball = Instantiate(ballPrefab);
        ball.SetParent(transform, false);
        gameState.balls[mazeID] = ball;

        startPosition = new Vector3(mazeLeftX + 0.5f, (mazeFloor.localScale.y + ball.localScale.y) / 2, mazeBotZ + 0.5f);
        ball.localPosition = startPosition;

        cam = Camera.main;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        obj = topWall.gameObject;
        objCollider = obj.GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update(){

        if (Mathf.Abs(ball.localPosition.x) > Mathf.Abs(startPosition.x) &&
            Mathf.Sign(ball.localPosition.x) == -Mathf.Sign(startPosition.x) &&
            Mathf.Abs(ball.localPosition.z) > Mathf.Abs(startPosition.z) &&
            Mathf.Sign(ball.localPosition.z) == -Mathf.Sign(startPosition.z)) {
            ball.GetComponent<Rigidbody>().drag = 1000;
            ball.GetComponent<Rigidbody>().angularDrag = 1000;

        }

    }
}

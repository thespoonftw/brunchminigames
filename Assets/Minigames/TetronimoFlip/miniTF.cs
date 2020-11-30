using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class miniTF_ShapeDesc
{
    [TextArea(3, 3)]
    public string shape;
    public Sprite sprite;
}

public class miniTF : MonoBehaviour {
    public GameObject TileObject;
    public float Spacing;
    public float TileScale;
    public float Width;
    public float Height;
    public Gradient[] ColorGradients;
    public miniTF_ShapeDesc[] Shapes;
    public GameObject PlayerObject;
    public Vector2[] PlayerPositions;
    public int ActiveTileTarget = 10;
    public bool DebugMarkersEnabled = false;
    public float NextShapeTime = 0.25f;
    public float NextFlipTime = 0.1f;
    public int FlipNumber = 15;

    private List<miniTF_Tile> tiles = new List<miniTF_Tile>();
    private float timer = 0f;
    private int tileIndex;
    private Vector3 topLeft;
    private Vector3 size;
    private bool placementFailed = false;

    private float nextShapeTimer = 0f;
    private bool stillPlacingShapes = true;
    private bool stillFlippingShapes = false;
    private float flipShapeTimer = 0f;

    private List<miniTF_ShapeDesc> startShapes;
    private List<miniTF_ShapeDesc> randomShapes;
    private List<miniTF_Shape> spawnedShapes;
    private int flipCount = 0;

    void Start() {
        Gradient colorGradient = ColorGradients[UnityEngine.Random.Range(0, ColorGradients.Length)];

        topLeft = new Vector3(
            -Width * Spacing * 0.5f,
            -Height * Spacing * 0.5f,
            0f
        );

        size = Vector3.one * Spacing * 0.5f;
        size.z = 0f;

        for (float x = 0; x < Width; x++) {
            for (float y = 0; y < Height; y++) {
                float t = new Vector3(x / Width, y / Height).magnitude;

                GameObject tile = GameObject.Instantiate(TileObject);
                tile.transform.localScale = Vector3.one * Spacing * TileScale;
                tile.transform.position = topLeft + new Vector3(x * Spacing, y * Spacing, 0f) + size;

                miniTF_Tile tf_tile = tile.GetComponent<miniTF_Tile>();
                tiles.Add(tf_tile);
                tf_tile.SetBackColor(colorGradient.Evaluate(t));
                tf_tile.x = (int)x;
                tf_tile.y = (int)y;
            }
        }

        // Spawn players:
        startShapes = new List<miniTF_ShapeDesc>(Shapes);
        randomShapes = new List<miniTF_ShapeDesc>();

        while (startShapes.Count > 0) {
            int index = UnityEngine.Random.Range(0, startShapes.Count);
            randomShapes.Add(startShapes[index]);
            startShapes.Remove(startShapes[index]);
        }

        // Pick a random spot.
        int start_x = Mathf.FloorToInt(Width * 0.5f);
        int start_y = UnityEngine.Random.Range(1, (int)Height - 2);

        // Place a random tetronimo at that point.
        ActivateTiles(start_x, 0, GetRandomShape());
    }

    List<miniTF_Tile> GetInactiveNeighbouringActive() {
        // Now, get a list of possible spawn points.
        // First, get all inactive tiles that neighbour an active tile.
        List<miniTF_Tile> inactiveNeighbouringActive = new List<miniTF_Tile>();
        for (int x = 0; x < (int)Width; x++) {
            for (int y = 0; y < (int)Height; y++) {
                miniTF_Tile tile = GetTile(x, y);

                if (!tile.TileIsActive) {
                    bool found = false;

                    // Add this tile, if left is active.
                    miniTF_Tile left = GetTile(x - 1, y);
                    if (left != null && left.TileIsActive) {
                        inactiveNeighbouringActive.Add(tile);
                        found = true;
                    }

                    // Same for right, up, down...
                    miniTF_Tile right = GetTile(x + 1, y);
                    if (!found && right != null && right.TileIsActive) {
                        inactiveNeighbouringActive.Add(tile);
                        found = true;
                    }

                    miniTF_Tile up = GetTile(x, y - 1);
                    if (!found && up != null && up.TileIsActive) {
                        inactiveNeighbouringActive.Add(tile);
                        found = true;
                    }

                    miniTF_Tile down = GetTile(x, y + 1);
                    if (!found && down != null && down.TileIsActive) {
                        inactiveNeighbouringActive.Add(tile);
                    }
                }
            }
        }
        return inactiveNeighbouringActive;
    }

    int ActiveTileCount() {
        int count = 0;
        for (int i = 0; i < tiles.Count; i++) {
            if (tiles[i].TileIsActive) {
                count++;
            }
        }
        return count;
    }

    bool CheckForClashes(int _x, int _y, miniTF_ShapeDesc shape) {
        String[] lines = shape.shape.Split('\n');
        for (int y = 0; y < lines.Length; y++) {
            for (int x = 0; x < lines[y].Length; x++) {
                if (lines[y][x] == 'A') {
                    miniTF_Tile tile = GetTile(_x + x, _y + 2 - y);
                    if (tile == null || tile.TileIsActive) {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    miniTF_ShapeDesc GetRandomShape() {
        int index = UnityEngine.Random.Range(0, Shapes.Length - 1);
        Debug.Log(index);
        return Shapes[index];
    }

    void ActivateTiles(int _x, int _y, miniTF_ShapeDesc shape) {
        String[] lines = shape.shape.Split('\n');
        for (int y = 0; y < lines.Length; y++) {
            for (int x = 0; x < lines[y].Length; x++) {
                if (lines[y][x] == 'A') {
                    miniTF_Tile tile = GetTile(_x + x, _y + 2 - y);
                    if (tile != null) {
                        tile.SetTileActive(true);
                        tile.ToggleShowing();
                    }
                }
            }
        }
    }

    private bool uh = false;

    public void CheckWin() {
        bool win = true;

        for (int x = 0; x < (int)Width; x++) {
            for (int y = 0; y < (int)Height; y++) {
                miniTF_Tile tile = GetTile(x, y);
                if (tile.TileIsActive && !tile.ShowingFront) {
                    win = false;
                }
            }
        }

        if (win) {
            Debug.Log("You win!!!");
        }
    }

    void Update() {
        if (stillPlacingShapes) {
            nextShapeTimer += Time.deltaTime;
            while (nextShapeTimer >= NextShapeTime && !placementFailed && ActiveTileCount() < ActiveTileTarget) {
                nextShapeTimer -= NextShapeTime;

                // Randomise the list of shapes.
                List<miniTF_ShapeDesc> randomShapeList = new List<miniTF_ShapeDesc>(Shapes);
                bool successfulPlacement = false;
                while (!successfulPlacement && randomShapeList.Count > 0) {
                    int shapeIndex = UnityEngine.Random.Range(0, randomShapeList.Count);
                    miniTF_ShapeDesc nextRandomShape = randomShapeList[shapeIndex];
                    randomShapeList.RemoveAt(shapeIndex);

                    Debug.Log("trying a random shape");

                    List<miniTF_Tile> inactiveNeighbouringActive = GetInactiveNeighbouringActive();
                    if (DebugMarkersEnabled) {
                        for (int i = 0; i < inactiveNeighbouringActive.Count; i++) {
                            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                            marker.transform.localScale = Vector3.one * 0.5f;
                            marker.transform.position = GetPosition(inactiveNeighbouringActive[i].x, inactiveNeighbouringActive[i].y);
                        }
                    }

                    while (!successfulPlacement && inactiveNeighbouringActive.Count > 0) {
                        int index = UnityEngine.Random.Range(0, inactiveNeighbouringActive.Count);

                        // Now pick one of the random inactive points.
                        miniTF_Tile nextTile = inactiveNeighbouringActive[index];
                        inactiveNeighbouringActive.RemoveAt(index);

                        String[] lines = nextRandomShape.shape.Split('\n');
                        for (int y = 0; y < lines.Length; y++) {
                            for (int x = 0; x < lines[y].Length; x++) {
                                // As y increases, we're going 'down' the shape that was described.
                                // e.g.
                                //y x>0 1 2
                                //v
                                //0   0 A A
                                //1   0 A 0
                                //2   0 A 0

                                if (!successfulPlacement && lines[y][x] == 'A') {
                                    // Then, this is a potential starting point,
                                    // because it contains an active square.

                                    Debug.Log(x + ", " + y + ": potential starting point");

                                    if (!CheckForClashes(nextTile.x - x, nextTile.y - 2 + y, nextRandomShape)) {
                                        ActivateTiles(nextTile.x - x, nextTile.y - 2 + y, nextRandomShape);
                                        successfulPlacement = true;

                                        if (DebugMarkersEnabled) {
                                            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                            marker.transform.position = GetPosition(nextTile.x, nextTile.y);
                                            successfulPlacement = true;
                                        }
                                    }
                                }

                                if (!successfulPlacement && lines[y][x] == 'A') {
                                    // Then this is a potential starting point. See if it would cause any clashes.
                                    if (false && !CheckForClashes(nextTile.x + x, nextTile.y - y, nextRandomShape)) {
                                        Debug.Log("no clashes");
                                        ActivateTiles(nextTile.x + (x - 1), nextTile.y - y, nextRandomShape);
                                        if (DebugMarkersEnabled) {
                                            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                            marker.transform.position = GetPosition(nextTile.x, nextTile.y);
                                            successfulPlacement = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (!successfulPlacement) {
                    placementFailed = true;
                }

                if (placementFailed || ActiveTileCount() >= ActiveTileTarget) {
                    stillPlacingShapes = false;
                    stillFlippingShapes = true;

                    // Create the players.
                    spawnedShapes = new List<miniTF_Shape>();
                    for (int i = 0; i < PlayerManager.GetPlayers().Count; i++) {
                        GameObject player = GameObject.Instantiate(PlayerObject);
                        miniTF_Shape s = player.GetComponent<miniTF_Shape>();
                        s.SetShape(randomShapes[i], PlayerManager.GetPlayer(i), this, PlayerPositions[i]);
                        spawnedShapes.Add(s);
                    }
                }
            }
        }

        if (stillFlippingShapes) {
            flipShapeTimer += Time.deltaTime;

            while (flipShapeTimer >= NextFlipTime) {
                flipShapeTimer -= NextFlipTime;
                int shapeIndex = UnityEngine.Random.Range(0, spawnedShapes.Count);
                spawnedShapes[shapeIndex].ToggleRandom();

                flipCount++;
                if (flipCount >= FlipNumber) {
                    stillFlippingShapes = false;
                }
            }
        }
    }

    public Vector3 GetPosition(int x, int y) {
        return topLeft + new Vector3((float) x * Spacing, (float) y * Spacing, 0f) + size;
    }

    public miniTF_Tile GetTile(int x, int y) {
        // 0,0 0,1 0,2
        if (x < 0 || x >= (int) Width || y < 0 || y >= (int) Height) {
            return null;
        }
        return tiles[x * (int) Height + y];
    }
}

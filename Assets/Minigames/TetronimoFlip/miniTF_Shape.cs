using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class miniTF_Shape : MonoBehaviour {
    private miniTF_ShapeDesc shape;
    private Player player;
    private int x = 2;
    private int y = 2;
    private miniTF grid;
    private List<Vector2> coveredSquares = new List<Vector2>();

    public float OnMoveAlpha;
    public float OnStillAlpha;
    public float AlphaLerpSpeed;
    public float AlphaLerpWait;

    private float alpha;
    private float alphaWaitTimer;
    private SpriteRenderer sprite;

    void Start() {

    }

    public void SetShape(miniTF_ShapeDesc shape, Player player, miniTF grid, Vector2 _pos) {
        this.player = player;
        this.shape = shape;
        this.grid = grid;
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = shape.sprite;
        sprite.color = Color.Lerp(PlayerManager.PlayerColors[player.id], Color.white, 0.5f);

        String[] lines = this.shape.shape.Split('\n');
        for (int y = 0; y < lines.Length; y++) {
            for (int x = 0; x < lines[y].Length; x++) {
                if (lines[y][x] == 'A') {
                    coveredSquares.Add(new Vector2(x - 1, 1 - y));
                }
            }
        }

        this.x = (int)_pos.x;
        this.y = (int)_pos.y;

        SetPos();
    }

    void SetPos() {
        Vector3 pos = this.grid.GetPosition(this.x, this.y);
        pos.z = transform.position.z;
        transform.position = pos;
    }

    public void ToggleRandom() {
        int temp_x = UnityEngine.Random.Range(1, (int)grid.Width-1);
        int temp_y = UnityEngine.Random.Range(1, (int)grid.Height-1);
        Toggle(temp_x, temp_y, false);
    }

    void Toggle(int _x, int _y, bool instant = false) {
        for (int i = 0; i < coveredSquares.Count; i++) {
            miniTF_Tile tile = grid.GetTile(
                _x + (int)coveredSquares[i].x,
                _y + (int)coveredSquares[i].y
            );
            if (tile != null) {
                tile.ToggleShowing(instant);
            }
        }
    }

    bool IsValidMove(int x, int y) {
        int nx = this.x + x;
        int ny = this.y + y;

        for (int i = 0; i < coveredSquares.Count; i++) {
            miniTF_Tile tile = grid.GetTile(
                nx + (int)coveredSquares[i].x,
                ny + (int)coveredSquares[i].y
            );
            if (tile == null) {
                return false;
            }
        }

        return true;
    }

    void BumpAlpha() {
        alpha = OnMoveAlpha;
        alphaWaitTimer = AlphaLerpWait;
    }

    void Update() {
        if (player.WasLeftPressedThisFrame()) {
            if (IsValidMove(-1, 0)) {
                BumpAlpha();
                this.x--;
            }
        }
        if (player.WasRightPressedThisFrame()) {
            if (IsValidMove(1, 0)) {
                BumpAlpha();
                this.x++;
            }
        }
        if (player.WasUpPressedThisFrame()) {
            if (IsValidMove(0, 1)) {
                BumpAlpha();
                this.y++;
            }
        }
        if (player.WasDownPressedThisFrame()) {
            if (IsValidMove(0, -1)) {
                BumpAlpha();
                this.y--;
            }
        }
        SetPos();

        if (player.WasActionButtonPressedThisFrame()) {
            BumpAlpha();
            Toggle(this.x, this.y);
            grid.CheckWin();
        }

        alphaWaitTimer -= Time.deltaTime;
        if (alphaWaitTimer <= 0f) {
            alpha = Mathf.Lerp(alpha, OnStillAlpha, AlphaLerpSpeed);
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniTF_Tile : MonoBehaviour {
    public GameObject FrontQuad;
    public GameObject BackQuad;
    public bool ShowingFront;
    public float RotateLerpSpeed;
    public bool TileIsActive = false;
    public int x;
    public int y;

    private float yRotation = 0f;

    void Start() {
        SetFrontColor(Color.black);
        // SetBackColor(Color.red);
        SetRotation(1f);
    }

    void SetRotation(float l) {
        yRotation = Mathf.Lerp(yRotation, ShowingFront ? 180f : 0f, l);
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            yRotation,
            transform.eulerAngles.z
        );
    }

    void Update() {
        SetRotation(RotateLerpSpeed);
    }

    public void SetFrontColor(Color c) {
        FrontQuad.GetComponent<Renderer>().material.color = new Color(c.r, c.g, c.b, 0.5f);
    }

    public void ToggleShowing(bool instant = false) {
        this.ShowingFront = !this.ShowingFront;
        if (instant) {
            SetRotation(1f);
        }
    }

    public void SetBackColor(Color c) {
        BackQuad.GetComponent<Renderer>().material.color = c;
    }

    public void SetTileActive(bool active) {
        TileIsActive = active;
        FrontQuad.SetActive(active);
        BackQuad.SetActive(active);
    }
}

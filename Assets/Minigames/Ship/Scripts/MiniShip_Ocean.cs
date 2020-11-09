using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniShip_Ocean : MonoBehaviour {
    public float ScrollSpeed = 1f;
    public float Resolution;
    public float Z = 0f;
    public float Margin = 5f;
    public float OffsetY = -6f;
    public float Acceleration = 0f;
    public float MinSpeedMod = 0.25f;
    public float MaxSpeedMod = 1.25f;

    private List<GameObject> waterObjects = new List<GameObject>();
    private float t = 0f;
    private float _t = 0f;
    private ObjectPool waterObjectPool;
    private float w;
    private float spacing;
    private float scrollSpeed = 0f;
    private float speedMod = 1f;

    void Start() {
        waterObjectPool = GetComponent<ObjectPool>();
        w = Camera.main.orthographicSize * Screen.width / Screen.height;
        Debug.Log(w);
        t = Camera.main.orthographicSize * Screen.width / Screen.height + Margin;
        spacing = w / Resolution;

        UpdateDots(true);
    }

    void UpdateDots(bool setup = false) {
        while (t > -(w + Margin)) {
            GameObject g = waterObjectPool.GetObject();
            g.transform.position = new Vector3(
                t,
                GetY(_t),
                Z
            );

            if (setup) {
                _t += spacing;
            }
            t -= spacing;
        }
    }

    void Update() {
        scrollSpeed = Mathf.MoveTowards(
            scrollSpeed,
            ScrollSpeed * speedMod,
            Acceleration * Time.deltaTime
        );

        t += Time.deltaTime * scrollSpeed;

        Vector3 move = Vector3.right * Time.deltaTime * scrollSpeed;
        foreach (GameObject g in waterObjectPool.GetAllObjects()) {
            g.transform.position = g.transform.position + move;
            if (g.transform.position.x > w + Margin) {
                g.SetActive(false);
            }
        }

        UpdateDots();

        _t += Time.deltaTime * scrollSpeed;
    }

    public float Wave1Freq = 0.1f;
    public float Wave1Amp = 5f;

    public float Wave2Freq = 0.5f;
    public float Wave2Amp = 1f;

    public float GetSlope(float t) {
        return Mathf.Cos(t * Wave1Freq);
    }

    public void SetSpeed(float s) {
        speedMod = Mathf.Lerp(MinSpeedMod, MaxSpeedMod, s);
    }

    public float GetCentralSlope() {
        return GetSlope(_t - (w + Margin));
    }

    public float GetCentralY() {
        // What t does the centre represent?
        return GetY(_t - (w + Margin));
    }

    public float GetY(float t) {
        return Mathf.Sin(t * Wave1Freq) * Wave1Amp +
            Mathf.Sin(t * Wave2Freq) * Wave2Amp +
            OffsetY;
    }
}

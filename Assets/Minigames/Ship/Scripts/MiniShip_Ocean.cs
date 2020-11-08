using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniShip_Ocean : MonoBehaviour {
    public float ScrollSpeed = 1f;
    public float Resolution;

    private List<GameObject> waterObjects = new List<GameObject>();
    private float t = 0f;
    private ObjectPool waterObjectPool;

    void Start() {
        waterObjectPool = GetComponent<ObjectPool>();
        float w = Camera.main.orthographicSize * Screen.width / Screen.height;
    }

    void Update() {
        t += Time.deltaTime * ScrollSpeed;
    }

    public float GetY() {
        return 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_Star : MonoBehaviour {
    public float MinSize;
    public float MaxSize;

    void Start() {
        float size = Random.Range(MinSize, MaxSize);
        transform.localScale = new Vector3(
            size,
            size,
            1f
        );
    }

    void Update() {
        
    }
}

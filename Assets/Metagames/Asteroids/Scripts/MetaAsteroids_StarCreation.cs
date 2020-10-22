using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_StarCreation : MonoBehaviour {
    public GameObject Star;
    public float Width;
    public float Height;
    public int Count;

    void Start() {
        for (int i = 0; i < Count; i++) {
            GameObject star = GameObject.Instantiate(Star);
            star.transform.position = new Vector3(
                Random.Range(-Width, Width),
                Random.Range(-Height, Height),
                10f
            );
        }
    }

    void Update() {
        
    }
}

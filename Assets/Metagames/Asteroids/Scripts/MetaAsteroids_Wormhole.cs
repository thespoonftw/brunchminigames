using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct MetaAsteroids_WormholeAppearance {
    public Gradient ColorGradient;
    public float Size;
    public float SizeVariance;
    public float Z;
    public int Chance;
    public float LaunchSpeed;
}

public class MetaAsteroids_Wormhole : MonoBehaviour {
    private MetaAsteroids_WormholePool pool;
    private List<int> chances = new List<int>();
    private float timer = 0f;

    public MetaAsteroids_WormholeAppearance[] PieceAppearances;
    public float Frequency;

    void Start() {
        pool = GameObject.Find("Wormhole Pool").GetComponent<MetaAsteroids_WormholePool>();
        for (int i = 0; i < PieceAppearances.Length; i++) {
            for (int j = 0; j < PieceAppearances[i].Chance; j++) {
                chances.Add(i);
            }
        }
    }

    void Update() {
        timer -= Time.deltaTime;
        while (timer <= 0f) {
            timer += (1f / Frequency);
            int index = chances[UnityEngine.Random.Range(0, chances.Count)];
            MetaAsteroids_WormholePiece piece = pool.GetPiece();
            piece.Begin(transform.position, transform.localScale.x, PieceAppearances[index]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHoovers_Spawner : MonoBehaviour {
    public float GridSize;
    public float OffsetVariance;
    public float NoiseExponent;
    public float NoiseDispersion;
    public float NoiseThreshold;
    public GameObject GrassObject;
    public Gradient GrassColorGradient;
    public float MinSize;
    public float MaxSize;
    public float SizeExponent;
    public float Margin;

    private float w;
    private float h;
    private List<GameObject> grasses = new List<GameObject>();
    private float seed;

    void Start() {
        w = Camera.main.orthographicSize * Screen.width / Screen.height;
        w -= Margin;
        h = Camera.main.orthographicSize;
        h -= Margin;

        GenerateGrass();
    }

    void ClearGrass() {
        foreach (GameObject g in grasses) {
            Destroy(g);
        }
        grasses = new List<GameObject>();
    }

    void GenerateGrass() {
        seed = (float)System.DateTime.Now.Second;

        int xSteps = Mathf.RoundToInt((2f * w) / GridSize);
        int ySteps = Mathf.RoundToInt((2f * h) / GridSize);

        for (int x = 0; x < xSteps; x++) {
            for (int y = 0; y < ySteps; y++) {
                float _x = ((float)x) / ((float)xSteps - 1f);
                float _y = ((float)y) / ((float)ySteps - 1f);

                float noise = Mathf.PerlinNoise(
                    seed + _x * NoiseDispersion,
                    seed + _y * NoiseDispersion
                );

                if (Mathf.Pow(noise, NoiseExponent) >= NoiseThreshold) {
                    Vector3 grassPosition = new Vector3(
                        Mathf.Lerp(-w, w, _x),
                        Mathf.Lerp(-h, h, _y),
                        0f
                    );

                    Vector3 offset = new Vector3(
                        Random.Range(-1f, 1f),
                        Random.Range(-1f, 1f),
                        0f
                    ).normalized * Random.Range(0f, OffsetVariance);

                    GameObject g = GameObject.Instantiate(GrassObject);
                    float s = Mathf.Lerp(MinSize, MaxSize, Mathf.Pow(Random.Range(0f, 1f), SizeExponent));
                    g.transform.localScale = new Vector3(s, s, 1f);
                    g.transform.position = grassPosition + offset;
                    grasses.Add(g);
                }
            }
        }
    }

    void Update() {
        // First, get the number of steps in x and y.

    }
}

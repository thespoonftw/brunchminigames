using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBugs_Snake : MonoBehaviour {
    public GameObject[] BodyPieces;
    public GameObject BodyObject;
    public GameObject ShadowObject;

    public float Speed = 0.1f;
    public float NoiseSpeed;
    public float RotateSpeed;
    public bool IsParent = false;
    public int SpawnSteps;
    public float SpawnStepDT;
    public int SpawnEveryXSteps;

    private GameObject head;
    private float perlinT = 0f;
    private float seed;
    private GameObject shadowObject;

    void Start() {
        if (IsParent) {
            transform.Rotate(0f, 0f, Random.Range(0f, 360f));
            seed = Random.Range(0f, 1000f);
            int spawnCounter = 0;

            for (int i = 0; i < SpawnSteps; i++) {
                Move(SpawnStepDT);

                spawnCounter++;
                if (spawnCounter == SpawnEveryXSteps) {
                    spawnCounter = 0;
                    // Create a body piece.
                    GameObject g = GameObject.Instantiate(BodyObject);
                    MiniBugs_Snake snake = g.GetComponent<MiniBugs_Snake>();
                    snake.transform.position = transform.position;
                    snake.transform.rotation = transform.rotation;
                    snake.InitRandom(perlinT, seed);
                    snake.IsParent = false;
                }
            }
        }

        shadowObject = GameObject.Instantiate(ShadowObject);
    }

    public void InitRandom(float t, float seed) {
        perlinT = t;
        this.seed = seed;
    }

    void Move(float dt) {
        perlinT += dt * NoiseSpeed;
        transform.Rotate(0f, 0f, (Mathf.PerlinNoise(seed, perlinT) - 0.5f) * RotateSpeed * dt);
        transform.position += transform.up * dt * Speed;

        Vector3 p = transform.position;
        p.z = 0f;
        if (p.magnitude > 27f) {
            float z = transform.position.z;
            Vector3 pos = transform.position * -1f;
            pos.z = z;
            transform.position = pos;
        }
    }

    void FixedUpdate() {
        Move(Time.deltaTime);
        float z = transform.position.z;
        Vector3 pos = transform.position - transform.position.normalized * 27f * 2f;
        pos.z = z;
        shadowObject.transform.position = pos;
        shadowObject.transform.rotation = transform.rotation;
    }
}

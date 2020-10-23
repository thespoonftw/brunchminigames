using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_ExplosionPiece : MonoBehaviour {
    public float LifeTimePerSize = 1f;
    public float SizePerVelocity = 1f;
    public Gradient ColorGradient;
    public float ColorGradientQuantise = 10f;

    private Vector3 velocity;
    private float lifeTime;
    private float targetLifeTime;
    private SpriteRenderer sprite;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Explode(Vector3 position, float size) {
        targetLifeTime = size * LifeTimePerSize;
        lifeTime = 0f;
        velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * (size / SizePerVelocity);
        transform.localScale = Vector3.one * size;
        transform.position = position;
    }

    void Update() {
        transform.position += velocity * Time.deltaTime;
        lifeTime += Time.deltaTime;

        float colorT = Mathf.Clamp01(lifeTime / targetLifeTime);
        if (ColorGradientQuantise > 0f) {
            colorT = Mathf.Round(colorT * ColorGradientQuantise) / ColorGradientQuantise;
        }
        sprite.color = ColorGradient.Evaluate(colorT);

        if (lifeTime >= targetLifeTime) {
            gameObject.SetActive(false);
        }
    }
}

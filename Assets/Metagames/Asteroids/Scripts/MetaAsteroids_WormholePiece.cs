using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_WormholePiece : MonoBehaviour {
    private Vector3 targetPosition;
    private Vector3 velocity;
    private MetaAsteroids_WormholeAppearance appearance;
    private float radius;
    private SpriteRenderer sprite;

    public float Gravity;
    public float Friction;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        Vector3 toTarget = targetPosition - transform.position;
        float mag = toTarget.magnitude;

        float a = (1 / Mathf.Pow(mag, 1.5f)) * (Gravity / transform.localScale.x);
        velocity += toTarget.normalized * a * Time.deltaTime;

        float colorT = Mathf.Clamp01(mag / radius);
        sprite.color = appearance.ColorGradient.Evaluate(1f - colorT);

        velocity = Vector3.MoveTowards(velocity, Vector3.zero, velocity.magnitude * Friction * Time.deltaTime);

        float m = velocity.magnitude * Time.deltaTime;
        if (mag < m + transform.localScale.x) {
            gameObject.SetActive(false);
        } else {
            transform.position += velocity * Time.deltaTime;
        }
    }

    public void Begin(Vector3 position, float radius, MetaAsteroids_WormholeAppearance appearance) {
        sprite = GetComponent<SpriteRenderer>();
        this.appearance = appearance;
        this.radius = radius;

        targetPosition = position + new Vector3(0f, 0f, appearance.Z);
        Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * radius;
        Vector3 direction = Quaternion.Euler(0f, 0f, 90f) * offset.normalized;
        velocity = direction * appearance.LaunchSpeed;

        transform.localScale = Vector3.one * (appearance.Size + Random.Range(-1f, 1f) * appearance.SizeVariance);

        transform.position = position + offset;

        sprite.color = appearance.ColorGradient.Evaluate(0f);
    }
}

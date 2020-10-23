using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_Asteroid : MonoBehaviour {
    public int Health;
    private int actualHealth;
    private Vector3 velocity;
    private MetaAsteroids_ExplosionManager explosionManager;

    private SpriteRenderer sprite;
    private Color targetColor;

    private float freezeTimer = 0f;

    public float FreezeTimeOnHit = 0.05f;

    void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        targetColor = sprite.color;
    }

    void Start() {
        explosionManager = GameObject.Find("Explosions").GetComponent<MetaAsteroids_ExplosionManager>();
    }

    void Update() {
        if (freezeTimer > 0f) {
            freezeTimer -= Time.deltaTime;
            return;
        }
        sprite.color = Color.Lerp(sprite.color, targetColor, 0.2f);
        transform.position += velocity * Time.deltaTime;
    }

    void OnEnable() {
        actualHealth = Health;
        sprite.color = targetColor;
    }

    public void ShootTowardsEarth(float v) {
        velocity = new Vector3(-transform.position.x, -transform.position.y, 0f) * v;
    }

    void Explode() {
        explosionManager.Explode(transform.position, 6, 0.4f);
        gameObject.SetActive(false);
    }

    public bool OnHitByBullet() {
        freezeTimer += FreezeTimeOnHit;
        actualHealth--;
        sprite.color = Color.white;
        if (actualHealth == 0) {
            Explode();
            return true;
        } else {
            return false;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        MetaAsteroids_Earth earth = col.gameObject.GetComponent<MetaAsteroids_Earth>();
        if (earth != null) {
            earth.OnHitByAsteroid();
            Explode();
        }
    }
}

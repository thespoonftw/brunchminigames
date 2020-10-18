using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_Asteroid : MonoBehaviour {
    public int Health;
    private int actualHealth;
    private Vector3 velocity;

    private SpriteRenderer sprite;
    private Color targetColor;

    private float freezeTimer = 0f;

    public float FreezeTimeOnHit = 0.05f;

    void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        targetColor = sprite.color;
    }

    void Start() {
        
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

    public bool OnHitByBullet() {
        freezeTimer += FreezeTimeOnHit;
        actualHealth--;
        sprite.color = Color.white;
        if (actualHealth == 0) {
            gameObject.SetActive(false);
            return true;
        } else {
            return false;
        }
    }
}

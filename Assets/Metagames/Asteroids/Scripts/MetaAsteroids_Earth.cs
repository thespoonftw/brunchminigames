using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_Earth : MonoBehaviour {
    public int Health = 1;

    private MetaAsteroids_ExplosionManager explosionManager;

    void Start() {
        explosionManager = GameObject.Find("Explosions").GetComponent<MetaAsteroids_ExplosionManager>();
    }

    void Update() {
        
    }

    void Explode() {
        explosionManager.Explode(transform.position, 6, 0.4f);
        gameObject.SetActive(false);
    }

    public void OnHitByAsteroid() {
        Health--;
        if (Health <= 0) {
            Explode();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_AsteroidsManager : MonoBehaviour {
    private ObjectPool smallPool;
    private ObjectPool mediumPool;
    private ObjectPool largePool;

    public float AsteroidFrequency = 0.1f;
    public float AsteroidSpeed = 1f;
    public bool Paused = true;

    private float asteroidTimer = 0f;
    private ObjectPool[] pools;

    void Start() {
        pools = GetComponents<ObjectPool>();
        smallPool = pools[0];
        mediumPool = pools[1];
        largePool = pools[2];
    }

    void Update() {
        if (!Paused) {
            asteroidTimer += Time.deltaTime;
            while (asteroidTimer >= 1f / AsteroidFrequency) {
                asteroidTimer -= (1f / AsteroidFrequency);
                SpawnAsteroid();
            }
        }
    }

    public void KillAll() {
        for (int i = 0; i < pools.Length; i++) {
            List<GameObject> asteroids = pools[i].GetAllObjects();
            for (int j = 0; j < asteroids.Count; j++) {
                if (asteroids[j].activeSelf) {
                    asteroids[j].GetComponent<MetaAsteroids_Asteroid>().Explode();
                }
            }
        }
    }

    public void SpawnAsteroid(int index=0) {
        GameObject g = pools[index].GetObject();
        g.transform.position = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * 20f;
        g.GetComponent<MetaAsteroids_Asteroid>().ShootTowardsEarth(AsteroidSpeed);
    }

    public GameObject GetSmallAsteroid() {
        return smallPool.GetObject();
    }

    public GameObject GetMediumAsteroid() {
        return mediumPool.GetObject();
    }

    public GameObject GetLargeAsteroid() {
        return largePool.GetObject();
    }
}

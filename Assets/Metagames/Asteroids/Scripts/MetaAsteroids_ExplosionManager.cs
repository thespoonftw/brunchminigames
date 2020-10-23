using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_ExplosionManager : MonoBehaviour {
    public GameObject ExplosionPrefab;
    public int InitialPoolSize = 0;

    private List<MetaAsteroids_ExplosionPiece> explosions = new List<MetaAsteroids_ExplosionPiece>();

    public MetaAsteroids_ExplosionPiece GetExplosionPiece() {
        for (int i = 0; i < explosions.Count; i++) {
            if (!explosions[i].gameObject.activeSelf) {
                explosions[i].gameObject.SetActive(true);
                return explosions[i];
            }
        }

        MetaAsteroids_ExplosionPiece e = AddExplosionPiece();
        e.gameObject.SetActive(true);
        return e;
    }

    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < InitialPoolSize; i++) {
            AddExplosionPiece();
        }
    }

    MetaAsteroids_ExplosionPiece AddExplosionPiece() {
        GameObject g = GameObject.Instantiate(ExplosionPrefab);
        g.SetActive(false);
        MetaAsteroids_ExplosionPiece e = g.GetComponent<MetaAsteroids_ExplosionPiece>();
        explosions.Add(e);
        return e;
    }

    void Update() {
        
    }

    public void Explode(Vector3 position, int count, float size) {
        for (int i = 0; i < count; i++) {
            MetaAsteroids_ExplosionPiece e = GetExplosionPiece();
            e.Explode(position, size);
        }
    }
}

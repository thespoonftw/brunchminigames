using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_WormholePool : MonoBehaviour {
    public GameObject WormholePrefab;
    public int InitialPoolSize = 0;

    private List<MetaAsteroids_WormholePiece> pieces = new List<MetaAsteroids_WormholePiece>();

    public MetaAsteroids_WormholePiece GetPiece() {
        for (int i = 0; i < pieces.Count; i++) {
            if (!pieces[i].gameObject.activeSelf) {
                pieces[i].gameObject.SetActive(true);
                return pieces[i];
            }
        }

        MetaAsteroids_WormholePiece e = AddPiece();
        e.gameObject.SetActive(true);
        return e;
    }

    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < InitialPoolSize; i++) {
            AddPiece();
        }
    }

    MetaAsteroids_WormholePiece AddPiece() {
        GameObject g = GameObject.Instantiate(WormholePrefab);
        g.SetActive(false);
        MetaAsteroids_WormholePiece e = g.GetComponent<MetaAsteroids_WormholePiece>();
        pieces.Add(e);
        return e;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct MetaAsteroids_Phase {
    public float bpm;
    public string contents;
}

public class MetaAsteroids_MetaGameManager : MonoBehaviour {
    public MetaAsteroids_Phase[] Phases;
    public GameObject Wormhole;

    private MetaAsteroids_AsteroidsManager asteroidsManager;
    private int phaseIndex = 0;
    private float phaseTimer = 0f;
    private float phaseTime = 0f;
    private MetaAsteroids_Phase currentPhase;
    private bool phasePlaying = false;

    void Start() {
        asteroidsManager = GameObject.Find("Asteroid Manager").GetComponent<MetaAsteroids_AsteroidsManager>();
        StartPhase(0);
    }

    void StartPhase(int index) {
        phaseIndex = 0;
        currentPhase = Phases[index];
        phaseTimer = 0f;
        phaseTime = 60f / currentPhase.bpm;
        phasePlaying = true;
    }

    void Update() {
        if (phasePlaying) {
            phaseTimer += Time.deltaTime;
            while (phaseTimer >= phaseTime) {
                Debug.Log("eval time");
                Debug.Log(phaseIndex);
                Debug.Log(currentPhase.contents.Length);
                phaseTimer -= phaseTime;
                EvaluatePhase(currentPhase.contents[phaseIndex]);
                phaseIndex++;
                if (phaseIndex >= currentPhase.contents.Length) {
                    EndPhase();
                }
            }
        }
    }

    void EndPhase() {
        phasePlaying = false;
    }

    void EvaluatePhase(char c) {
        switch (c) {
            case 's':
                asteroidsManager.SpawnAsteroid(0);
                break;
            case 'm':
                asteroidsManager.SpawnAsteroid(1);
                break;
            case 'l':
                asteroidsManager.SpawnAsteroid(2);
                break;
            case 'w':
                GameObject wormhole = GameObject.Instantiate(Wormhole);
                wormhole.transform.position = new Vector3(
                    UnityEngine.Random.Range(-1f, 1f),
                    UnityEngine.Random.Range(-1f, 1f),
                    0f
                ).normalized * 7.5f;
                break;
            default:

                break;
        }
    }
}

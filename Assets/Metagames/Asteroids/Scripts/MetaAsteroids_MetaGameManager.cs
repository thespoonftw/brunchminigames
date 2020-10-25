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
    public List<string> MinigameScenes;
    public GameObject Cutout;

    private MetaAsteroids_AsteroidsManager asteroidsManager;
    private int phaseIndex = 0;
    private float phaseTimer = 0f;
    private float phaseTime = 0f;
    private MetaAsteroids_Phase currentPhase;
    private bool phasePlaying = false;
    private int playersInWormhole = 0;
    private MetaAsteroids_Cutout cutout;

    void Start() {
        asteroidsManager = GameObject.Find("Asteroid Manager").GetComponent<MetaAsteroids_AsteroidsManager>();
        StartPhase(0);
        MetagameManager.OnMinigameEnd += MinigameEnd;
    }

    private void MinigameEnd(bool isVictorious) {
        playersInWormhole = 0;
        cutout.ZoomOut();
    }

    public void PlayerEnteredWormhole(MetaAsteroids_Wormhole wormhole) {
        playersInWormhole++;
        List<Player> players = PlayerManager.GetPlayers();
        Debug.Log(playersInWormhole);
        Debug.Log(players.Count);
        if (playersInWormhole == players.Count) {
            AllPlayersEnteredWormhole(wormhole);
        }
    }

    private void AllPlayersEnteredWormhole(MetaAsteroids_Wormhole wormhole) {
        cutout = GameObject.Instantiate(Cutout).GetComponent<MetaAsteroids_Cutout>();
        cutout.ZoomIn(wormhole.transform.position, 1f, this);
    }

    String GetNextMinigame() {
        return MinigameScenes[UnityEngine.Random.Range(0, MinigameScenes.Count - 1)];
    }

    public void ZoomComplete() {
        Debug.Log("zoom complete!");
        String s = GetNextMinigame();
        Debug.Log("Starting minigame: " + s);
        MetagameManager.Instance.LoadMinigame(s);
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
                // Debug.Log("eval time");
                // Debug.Log(phaseIndex);
                // Debug.Log(currentPhase.contents.Length);
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

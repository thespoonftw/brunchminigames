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
    public GameObject Cutout;

    private MetaAsteroids_AsteroidsManager asteroidsManager;
    private int phaseIndex = 0;
    private int currentPhaseIndex = 0;
    private float phaseTimer = 0f;
    private float phaseTime = 0f;
    private MetaAsteroids_Phase currentPhase;
    private bool phasePlaying = false;
    private int playersInWormhole = 0;
    private MetaAsteroids_Cutout cutout;
    private MetaAsteroids_Wormhole currentWormhole;
    private List<MetaAsteroids_Player> players = new List<MetaAsteroids_Player>();
    private bool waitingForNextPhase = false;
    private float nextPhaseTimer = 0f;
    private MinigameGetter minigameGetter;

    void Start() {
        asteroidsManager = GameObject.Find("Asteroid Manager").GetComponent<MetaAsteroids_AsteroidsManager>();
        StartPhase(currentPhaseIndex);
        MetagameManager.OnMinigameEnd += MinigameEnd;

        minigameGetter = Resources.Load<GameObject>("Minigame Getter").GetComponent<MinigameGetter>();
        Debug.Log(minigameGetter.GetNextMinigameScene());
    }

    public void RegisterPlayer(MetaAsteroids_Player player) {
        players.Add(player);
    }

    // Mini game just ended
    private void MinigameEnd(bool isVictorious) {
        playersInWormhole = 0;
        cutout.ZoomOut();
        currentWormhole.KillMe();
        // Respawn all players.
        foreach (MetaAsteroids_Player player in players) {
            player.gameObject.SetActive(true);
        }
    }

    public void PlayerEnteredWormhole(MetaAsteroids_Wormhole wormhole) {
        playersInWormhole++;
        List<Player> players = PlayerManager.GetPlayers();
        Debug.Log(playersInWormhole);
        Debug.Log(players.Count);
        if (playersInWormhole == players.Count) {
            AllPlayersEnteredWormhole(wormhole);
        }
        phasePlaying = false;
    }

    private void AllPlayersEnteredWormhole(MetaAsteroids_Wormhole wormhole) {
        cutout = GameObject.Instantiate(Cutout).GetComponent<MetaAsteroids_Cutout>();
        currentWormhole = wormhole;
        cutout.ZoomIn(wormhole.transform.position, 1f, this);
        asteroidsManager.KillAll();
    }

    String GetNextMinigame() {
        return minigameGetter.GetNextMinigameScene();
    }

    public void ZoomOutComplete() {
        Debug.Log("zoom out complete!");
        waitingForNextPhase = true;
        nextPhaseTimer = 1f;
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

    // TODO: Make this better (end animations, etc.)
    // TODO: Win/Lose a meta game?
    void GameComplete() {
        MetagameManager.Instance.EndMetagame();
    }

    void NextPhase() {
        currentPhaseIndex++;
        if (currentPhaseIndex >= Phases.Length) {
            GameComplete();
        } else {
            StartPhase(currentPhaseIndex);
        }
    }

    void Update() {
        if (waitingForNextPhase) {
            nextPhaseTimer -= Time.deltaTime;
            if (nextPhaseTimer <= 0f) {
                waitingForNextPhase = false;
                NextPhase();
            }
        }

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

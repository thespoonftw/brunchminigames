using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinearMetagame : MonoBehaviour {

    [ReadOnly] public int minigameIndex = 0;

    public List<string> minigameScenes;

    private void Start() {
        MetagameManager.Instance.LoadMinigame(minigameScenes[0]);
        MetagameManager.OnMinigameEnd += NextGame;
    }

    private void OnDestroy() {
        MetagameManager.OnMinigameEnd -= NextGame;
    }

    public void NextGame(bool isVictorious) {
        minigameIndex++;
        if (minigameIndex >= minigameScenes.Count) {
            MetagameManager.Instance.EndMetagame();
        }
        else {
            MetagameManager.Instance.LoadMinigame(minigameScenes[minigameIndex]);
        }
    }
}

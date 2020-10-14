using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinearMetagame : MonoBehaviour {

    private int minigameIndex = 0;

    public List<string> minigameScenes;

    private void Start() {
        MetagameManager.Instance.LoadMinigame(minigameScenes[0]);
        MetagameManager.OnEndMinigame += NextGame;
    }

    public void NextGame() {
        minigameIndex++;
        if (minigameIndex >= minigameScenes.Count) {
            MetagameManager.Instance.EndMetagame();
        }
        else {
            MetagameManager.Instance.LoadMinigame(minigameScenes[minigameIndex]);
        }
    }
}

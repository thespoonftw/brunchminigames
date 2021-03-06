﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinearMetagame : MonoBehaviour {

    private float nextGameDelay = 3f;
    public int minigameIndex = 0;

    public List<string> minigameScenes;

    private void Start() {
        minigameIndex = -1;
        StartCoroutine(NextMinigameAfterDelay());
        MetagameManager.OnMinigameEnd += MinigameEnd;
    }

    private void MinigameEnd(bool isVictorious) {
        StartCoroutine(NextMinigameAfterDelay());
    }

    IEnumerator NextMinigameAfterDelay() {
        yield return new WaitForSeconds(nextGameDelay);
        NextGame();
    }

    private void OnDestroy() {
        MetagameManager.OnMinigameEnd -= MinigameEnd;
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

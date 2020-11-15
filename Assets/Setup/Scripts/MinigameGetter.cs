using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MinigameData {
    public string SceneName;
    public int MinPlayers = 1;
    public int MaxPlayers = 4;
}

public class MinigameGetter : MonoBehaviour {
    public MinigameData[] Minigames;
    private List<MinigameData> currentMinigameList;
    private int currentMiniGameIndex = 0;

    void Start() {
        InitCurrentMiniGames();
    }

    void InitCurrentMiniGames() {
        currentMiniGameIndex = 0;
        currentMinigameList = new List<MinigameData>();
        int playerCount = PlayerManager.GetPlayers().Count;

        foreach (MinigameData data in Minigames) {
            if (playerCount >= data.MinPlayers && playerCount <= data.MaxPlayers) {
                currentMinigameList.Add(data);
            }
        }
    }

    public string GetNextMinigameScene() {
        string s = currentMinigameList[currentMiniGameIndex].SceneName;
        currentMiniGameIndex++;
        if (currentMiniGameIndex >= currentMinigameList.Count) {
            InitCurrentMiniGames();
        }
        return s;
    }
}

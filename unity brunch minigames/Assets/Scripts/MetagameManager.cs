using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetagameManager : Singleton<MetagameManager> {

    [SerializeField] MetagameData data;
    private int minigameIndex = 0;

    public void Initialise(MetagameData data) {
        this.data = data;
        DontDestroyOnLoad(gameObject);
        Debug.Log("metagame loaded!");
        LoadNextMinigame();
    }

    private void LoadNextMinigame() {
        Debug.Log("loading minigame: " + data.MinigameList[minigameIndex]);
        SceneManager.LoadScene(data.MinigameList[minigameIndex]);
    }

    public void EndMinigame() {
        Debug.Log("ending minigame: " + data.MinigameList[minigameIndex]);
        SceneManager.LoadScene(3);
        minigameIndex++;
        if (minigameIndex >= data.MinigameList.Count) {
            GameManager.Instance.EndMetagame();
        } else {
            LoadNextMinigame();
        }
    }
}

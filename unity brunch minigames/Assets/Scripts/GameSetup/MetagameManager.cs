using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetagameManager : Singleton<MetagameManager> {

    [ReadOnly] public string metagameScene;
    [ReadOnly] public string currentMinigameScene;

    public static event Action OnStartMetagame;
    public static event Action OnEndMetagame;
    public static event Action OnEndMinigame;

    public void Initialise(string metagameScene) {
        this.metagameScene = metagameScene;
        SceneManager.LoadScene(metagameScene);
        Debug.Log("metagame loaded: " + metagameScene);  
        OnStartMetagame?.Invoke();
    }

    public void EndMetagame() {
        OnEndMetagame?.Invoke();
        SceneManager.LoadScene("mainMenu");
        Destroy(gameObject);
    }

    public void LoadMinigame(string name) {
        currentMinigameScene = name;
        Debug.Log("Loading minigame: " + name);
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    public void EndMinigame() {
        SceneManager.UnloadSceneAsync(currentMinigameScene);
        currentMinigameScene = null;
        OnEndMinigame?.Invoke();
    }
}

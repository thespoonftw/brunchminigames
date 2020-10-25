using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetagameManager : Singleton<MetagameManager> {

    public string metagameScene;
    public string currentMinigameScene;
    private List<GameObject> disabledMetagameObjects = new List<GameObject>();

    public static event Action<bool> OnMinigameEnd; // subscribe to this from your metagame to know when a minigame ends. True = victory, False = defeat

    public void Initialise(string metagameScene) {
        DontDestroyOnLoad(gameObject);
        this.metagameScene = metagameScene;
        SceneManager.LoadScene(metagameScene);
        Debug.Log("metagame loaded: " + metagameScene);
    }

    // call this method from your metagame to go back to the main menu
    public void EndMetagame() {
        SceneManager.LoadScene("mainMenu");
        Destroy(gameObject);
    }

    // call this method from your metagame to load the next minigame
    public void LoadMinigame(string name) {
        currentMinigameScene = name;
        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects()) {
            if (go.activeSelf) { 
                disabledMetagameObjects.Add(go);
                go.SetActive(false);
            }
        }
        Debug.Log("Loading minigame: " + name);
        StartCoroutine(LoadMinigameAsync(name));
    }

    // call this method from your minigame when it ends
    public void EndMinigame(bool isVictorious) {
        if (GameManager.Instance.Mode == GameMode.TestMode) {
            Debug.Log("test mode victory: " + isVictorious);
            Time.timeScale = 0;
        }
        else {
            foreach (var go in disabledMetagameObjects) {
                go.SetActive(true);
            }
            SceneManager.UnloadSceneAsync(currentMinigameScene);
            currentMinigameScene = null;
            OnMinigameEnd?.Invoke(isVictorious);
        }
    }

    IEnumerator LoadMinigameAsync(string name) {
        yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
    }

    
}

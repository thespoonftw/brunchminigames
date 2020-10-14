using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode {
    Uninitialised,
    TestMode,
    PlayMode,
}

public class GameManager : Singleton<GameManager> {

    public GameMode Mode;

    void Start() {
        // start point for the whole game
        if (Mode == GameMode.TestMode) { return; }
        Mode = GameMode.PlayMode;
        gameObject.AddComponent<PlayerManager>();
        SceneManager.LoadScene("mainMenu");
        DontDestroyOnLoad(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    [SerializeField] MetagameData metagame1Data;

    public bool isGameInProgress;
    public bool isTestMode;

    void Start() {
        if (isTestMode) { return; }
        SceneManager.LoadScene(1); // load menu
        DontDestroyOnLoad(gameObject);
        PlayerManager.OnAPress += StartCharacterSelection;
    }

    public void StartCharacterSelection() {
        if (isGameInProgress) { return; }
        isGameInProgress = true;
        SceneManager.LoadScene(2); // load character selection
    }

    public void StartMetagameSelection() {
        SceneManager.LoadScene(3); // load metagame selection
    }

    public void StartMetagame() {
        var mm = MetagameManager.Instance;        
        DontDestroyOnLoad(mm);        
        SceneManager.LoadScene(4); // load metagame
        mm.Initialise(metagame1Data);
    }

    public void EndMetagame() {
        SceneManager.LoadScene(1);
        Destroy(MetagameManager.Instance.gameObject);
        isGameInProgress = false;
    }

}

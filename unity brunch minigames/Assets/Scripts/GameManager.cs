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
        SceneManager.LoadScene("mainMenu"); // load menu
        DontDestroyOnLoad(gameObject);
    }

    public void StartCharacterSelection() {
        if (isGameInProgress) { return; }
        isGameInProgress = true;
        SceneManager.LoadScene("selectCharacter"); // load character selection
    }

    public void StartMetagameSelection() {
        SceneManager.LoadScene("selectMetagame"); // load metagame selection
    }

    public void StartMetagame() {
        var mm = MetagameManager.Instance;
        DontDestroyOnLoad(mm);
        SceneManager.LoadScene("metagame1"); // load metagame
        mm.Initialise(metagame1Data);
    }

    public void EndMetagame() {
        SceneManager.LoadScene("mainMenu");
        Destroy(MetagameManager.Instance.gameObject);
        isGameInProgress = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : Singleton<MinigameManager> {

    public void Victory() {
        if (GameManager.Instance.isTestMode) {
            Debug.Log("test mode victory!");
            Time.timeScale = 0;
        } else {
            MetagameManager.Instance.EndMinigame();
        }
    }
}

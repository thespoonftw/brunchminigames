using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour {

    public int numberOfPlayers;
    public int difficulty; // 0 is easy

    void Start() {
        if (!GameManager.Instance.isGameInProgress) {
            GameManager.Instance.isTestMode = true;
            Debug.Log("begin test mode!");
            gameObject.AddComponent<PlayerManager>();
        }
    }
}


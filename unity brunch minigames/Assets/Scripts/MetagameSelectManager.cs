using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetagameSelectManager : Singleton<MetagameSelectManager> {

    public void Start() {
        Debug.Log("metagame selected!");
        GameManager.Instance.StartMetagame();
    }
}

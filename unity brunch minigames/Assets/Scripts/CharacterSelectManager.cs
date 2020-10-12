using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour {

    public void Start() {
        Debug.Log("character selected!");
        GameManager.Instance.StartMetagameSelection();
    }

}

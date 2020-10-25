using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatTimer : MonoBehaviour {

    public float timeRemaining;
    private Text text;

    private void Start() {
        text = GetComponentInChildren<Text>();
    }

    void Update() {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            text.text = ((int)timeRemaining).ToString();
            if (timeRemaining <= 0) {
                Defeat();
            }
        }
        

    }

    private void Defeat() {
        MetagameManager.Instance.EndMinigame(false);
    }
}

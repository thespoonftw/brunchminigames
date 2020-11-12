using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    public int NoTargetsRequired = 20;
    public int Score = 0;
    public Text TargetText;

    public bool wait = true;

    void Start() {
        
    }

    void Update() {

        if ((NoTargetsRequired - Score) > 0) {

            TargetText.text = "Targets remaining: " + (NoTargetsRequired - Score);

        }

        else {

            TargetText.text = "Targets remaining: 0";

        }

        WaitRoutine(wait);

    }


    IEnumerator WaitRoutine(bool w) {

        yield return new WaitWhile(() => w == true);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    public int NoTargetsRequired = 20;
    public int Score = 0;
    public Text TargetText;

    public bool wait = true;

    public float TimeLeft;

    void Start() {

        StartCoroutine(TextGenerate());

    }

    IEnumerator TextGenerate() {

        do {


            if ((NoTargetsRequired - Score) > 0) {

                TargetText.text = "Targets remaining: " + (NoTargetsRequired - Score);

            }

            else {

                TargetText.text = "Targets remaining: 0";

            }

            yield return new WaitWhile(() => wait == true);
            TimeLeft = GameObject.Find("Timer Text").GetComponent<TimeText>().TimeLeft;

        } while (TimeLeft > 0);

    }
}

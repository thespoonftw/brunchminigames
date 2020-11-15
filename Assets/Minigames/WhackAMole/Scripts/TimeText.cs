using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour {

    public int TotalGameTime;
    public Text TimerText;

    int GameTime;
    public int TimeLeft;

    void Start() {

        StartCoroutine(TimeTextGenerate());

    }


    IEnumerator TimeTextGenerate() {

        for (int i = 0; i <= TotalGameTime; i++) {

            TimeLeft = TotalGameTime - GameTime;

            if (TimeLeft > 0) {

                TimerText.text = "Time remaining: " + TimeLeft + "s";

                GameTime++;

            }

            else {

                TimerText.text = "Time up!";

            }

            yield return new WaitForSeconds(1);

        }

    }
}

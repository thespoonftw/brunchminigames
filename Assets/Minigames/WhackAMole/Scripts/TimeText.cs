using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour {

    public int TotalGameTime;
    public Text TimerText;

    int GameTime;
    int TimeLeft;

    void Start() {
        
    }

    void FixedUpdate() {

        TimeLeft = TotalGameTime - GameTime;

        if (TimeLeft > 0) {

            TimerText.text = "Time remaining: " + TimeLeft + "s";

            GameTime++;

        }

        else {

            TimerText.text = "Time up!";

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHoovers_SlimeCounter : MonoBehaviour {
    private int slimeCount = 0;
    private bool started = false;
    private bool complete = false;

    void Start() {
        
    }

    void Update() {
        
    }

    public void PlusSlime() {
        slimeCount++;
        started = true;
    }

    public void MinusSlime() {
        slimeCount--;
        if (started && slimeCount == 0) {
            Debug.Log("you win!!!!");
        }
    }
}

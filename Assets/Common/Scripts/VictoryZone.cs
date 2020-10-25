using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        MetagameManager.Instance.EndMinigame(true);
    }
}

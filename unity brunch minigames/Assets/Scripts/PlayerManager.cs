using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour {

    public static event Action OnAPress;
    public static event Action<float> OnXAxis;
    public static event Action<float> OnYAxis;



    void Update()  {
        if (Input.GetKeyDown("joystick button 0")) {
            OnAPress?.Invoke();
        }
        if (Input.GetAxis("Horizontal") != 0) {
            OnXAxis?.Invoke(Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Vertical") != 0) {
            OnYAxis?.Invoke(Input.GetAxis("Vertical"));
        }
    }
}

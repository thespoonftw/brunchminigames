using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject Player;
    public float CameraMovementSpeed;
    
    public Vector3 CameraOffset;
    //Default position of Camera relative to Player

    void Start() {
        
    }

    void Update() {

        float tdelta = Time.deltaTime;
        //time since last frame

        Vector3 PlayerPosition = Player.transform.position;
        //Player position vector

        Vector3 CameraPosition = transform.position;
        //Current Camera positon

        Vector3 CameraPositionTarget = PlayerPosition + CameraOffset;
        //Target for Camera position

        CameraPositionTarget.y = CameraOffset.y;
        //Set y component to default positon to avoid vertical movement

        Vector3 CameraPositionDifference = CameraPositionTarget - CameraPosition;

        if (CameraPositionDifference.magnitude > 0.1f) {

            transform.position = Vector3.Lerp(CameraPosition, CameraPositionTarget, CameraMovementSpeed * tdelta);
            //Interpolate Camera position with Playerposition

        }
        //If statement checks for deadzone

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFollower : MonoBehaviour {
    // Three radii determine how we handle joystick input.
    // Within this radius, nothing happens.
    public float DeadZoneRadius = 0.1f;
    // Within this radius the player turns.
    public float TurnZoneRadius = 0.25f;
    // Within this radius the player moves.
    public float MoveZoneRadius = 0.8f;

    // These variables determine how quick players rotate and move.
    public float MinTurnSpeed = 0f;
    public float MaxTurnSpeed = 1f;
    public float MinMoveSpeed = 0f;
    public float MaxMoveSpeed = 6f;
    public float MoveMod = 1f;
    public float TurnMod = 1f;

    public void TowardsPoint(Vector3 pos) {
        TowardsPointLocal((pos - transform.position).normalized);
    }

    public void TowardsPointLocal(Vector3 input) {
        float mag = input.magnitude;

        float rotateMag = Mathf.Clamp01((mag - DeadZoneRadius) / (TurnZoneRadius - DeadZoneRadius));
        float moveMag = Mathf.Clamp01((mag - TurnZoneRadius) / (MoveZoneRadius - TurnZoneRadius));

        float rotateSpeed = Mathf.Lerp(MinTurnSpeed, MaxTurnSpeed, rotateMag);
        rotateSpeed *= TurnMod;

        float angle = Mathf.Atan2(-input.y, -input.x) * Mathf.Rad2Deg + 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);

        float moveSpeed = Mathf.Lerp(MinMoveSpeed, MaxMoveSpeed, moveMag);
        moveSpeed *= MoveMod;
        transform.position = transform.position + transform.up * moveSpeed * Time.deltaTime * 1f;
    }
}

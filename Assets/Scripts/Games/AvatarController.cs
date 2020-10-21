using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour {

    private Player player;

    public bool offsetBy45;
    public float DeadZoneRadius = 0.1f; // Within this radius, nothing happens.
    public float MoveSpeed;

    private float RADIANS_TO_DEG = 180f / Mathf.PI;

    void Start() {
        player = GetComponent<PlayerControlComponent>().GetPlayer();
       //GetComponent<SpriteRenderer>().color = ShipOutlineColors[player.id];
    }

    void Update() {
        Vector3 input = player.GetInputAxis();
        if (offsetBy45) { input = Quaternion.Euler(0, 0, -45) * input; }
        float mag = input.magnitude;
        if (mag < DeadZoneRadius) { return; }
        transform.position = transform.position + new Vector3(input.x * Time.deltaTime * MoveSpeed, 0, input.y * Time.deltaTime * MoveSpeed);
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(input.x, input.y) * RADIANS_TO_DEG, 0);

        /*
        float rotateMag = Mathf.Clamp01((mag - DeadZoneRadius) / (TurnZoneRadius - DeadZoneRadius));
        float moveMag = Mathf.Clamp01((mag - TurnZoneRadius) / (MoveZoneRadius - TurnZoneRadius));
        float rotateSpeed = Mathf.Lerp(MinTurnSpeed, MaxTurnSpeed, rotateMag);
        float angle = Mathf.Atan2(-input.y, -input.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);
        float moveSpeed = Mathf.Lerp(MinMoveSpeed, MaxMoveSpeed, moveMag);
        transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime * -1f;
        */
    }

}

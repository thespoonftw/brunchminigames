﻿using System.Collections;
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
       //GetComponent<SpriteRenderer>().color = ShipOutlineColors[player.id]; // colors should be implemented at some point
    }

    void Update() {
        Vector3 input = player.GetInputAxis();
        if (offsetBy45) { input = Quaternion.Euler(0, 0, -45) * input; }
        float mag = input.magnitude;
        if (mag < DeadZoneRadius) { return; }
        transform.position = transform.position + new Vector3(input.x * Time.deltaTime * MoveSpeed, 0, input.y * Time.deltaTime * MoveSpeed);
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(input.x, input.y) * RADIANS_TO_DEG, 0);
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour {

    private Player player;

    public bool offsetBy45;
    public bool canSprint;

    public float DeadZoneRadius = 0.1f; // Within this radius, nothing happens.
    public float MoveSpeed;
    public float SprintSpeed;

    private Rigidbody rb;

    private float RADIANS_TO_DEG = 180f / Mathf.PI;

    void Start() {
        player = GetComponent<PlayerControlComponent>().GetPlayer();
        rb = GetComponent<Rigidbody>();
       //GetComponent<SpriteRenderer>().color = ShipOutlineColors[player.id]; // colors should be implemented at some point
    }

    void FixedUpdate() {
        Vector3 input = player.GetInputAxis();
        if (offsetBy45) { input = Quaternion.Euler(0, 0, -45) * input; }
        float mag = input.magnitude;
        if (mag < DeadZoneRadius) { return; }
        var speed = player.IsActionButtonPressed() && canSprint ? SprintSpeed : MoveSpeed;
        rb.velocity = new Vector3(input.x * speed, rb.velocity.y, input.y * speed);
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(input.x, input.y) * RADIANS_TO_DEG, 0);
    }

}

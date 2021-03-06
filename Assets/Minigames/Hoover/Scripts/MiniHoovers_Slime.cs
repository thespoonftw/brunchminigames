﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHoovers_Slime : MonoBehaviour {
    private MiniHoovers_Player targetPlayer;

    public float Wave1SpeedMin;
    public float Wave1SpeedMax;
    private float wave1Speed;
    private float wave1T = 0f;

    public float Wave1SizeChangeMin;
    public float Wave1SizeChangeMax;

    public float Wave2SpeedMin;
    public float Wave2SpeedMax;
    private float wave2Speed;
    private float wave2T = 0f;

    public float Wave2SizeChangeMin;
    public float Wave2SizeChangeMax;
    public GameObject Back;
    public float OutlineThickness = 0.25f;
    public GameObject SlimeLeftover;

    private Vector3 startScale;
    private MiniHoovers_SlimeCounter slimeCounter;
    private Vector3 offset;
    private bool claimed;

    void Awake() {
        slimeCounter = Camera.main.GetComponent<MiniHoovers_SlimeCounter>();
        wave1Speed = Random.Range(Wave1SpeedMin, Wave1SpeedMax);
        wave2Speed = Random.Range(Wave2SpeedMin, Wave2SpeedMax);
        wave1T = Random.Range(0, 10f);
        wave2T = Random.Range(0, 10f);

        // When this scale is 1, scale of child is 1.2
        // When this scale is 2, 

        // so scale is 1 + 0.2 / scale.
    }

    void Start() {
        startScale = transform.localScale;
    }

    void Update() {
        if (targetPlayer != null) {
            offset = Vector3.Lerp(offset, Vector3.zero, 0.1f);
            Vector3 pos = targetPlayer.transform.position + offset;
            pos.z = transform.position.z;
            transform.position = pos;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.25f, 0.1f);

            if (offset.magnitude < transform.localScale.x * 0.5f) {
                targetPlayer.OnEat();
                Destroy(gameObject);
            }
        } else {
            wave1T += wave1Speed * Time.deltaTime;
            wave2T += wave2Speed * Time.deltaTime;

            float s1 = Mathf.Lerp(
                Wave1SizeChangeMin,
                Wave1SizeChangeMax,
                (Mathf.Sin(wave1T) + 1f) * 0.5f
            );

            float s2 = Mathf.Lerp(
                Wave2SizeChangeMin,
                Wave2SizeChangeMax,
                (Mathf.Sin(wave2T) + 1f) * 0.5f
            );

            transform.localScale = startScale * (s1 + s2);
        }

        Back.transform.localScale = Vector3.one * (1f + OutlineThickness / transform.localScale.x);
    }

    public void GetSucked(MiniHoovers_Player player, bool spawned) {
        if (claimed) return;
        claimed = true;

        if (!spawned) {
            Destroy(gameObject);
            slimeCounter.MinusSlime();
            return;
        }

        GameObject leftover = GameObject.Instantiate(SlimeLeftover);
        leftover.transform.localScale = transform.localScale;
        leftover.transform.position = transform.position + Vector3.forward * 0.1f;

        offset = transform.position - player.transform.position;
        offset.z = 0f;
        targetPlayer = player;
        slimeCounter.MinusSlime();
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
    }
}

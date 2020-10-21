using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_Shoot : MonoBehaviour {
    private Player player;
    private MetaAsteroids_Player playerAsteroids;
    private ObjectPool bulletPool;

    public enum SingleShotTypes { Shotgun };
    public SingleShotTypes ShotTypeSingle = SingleShotTypes.Shotgun;

    public enum MultiShotTypes { MachineGun };
    public MultiShotTypes ShotTypeMulti = MultiShotTypes.MachineGun;

    // Variables to do with shooting
    public float BulletSpawnDistance;
    public float SinglePressTime = 0.15f;

    private float heldTimer = 0f;
    private bool isHeld = false;
    private bool heldShooting = false;
    private float multiShotTimer = 0f;

    private float blockShotTimer = 0f;

    void Start() {
        player = GetComponent<PlayerControlComponent>().GetPlayer();
        bulletPool = GameObject.Find("Bullet Pool").GetComponent<ObjectPool>();
        playerAsteroids = GetComponent<MetaAsteroids_Player>();
    }

    void Update() {
        if (blockShotTimer > 0f) {
            blockShotTimer -= Time.deltaTime;
            if (blockShotTimer < 0f) blockShotTimer = 0f;
        }

        if (!isHeld && player.IsActionButtonPressed()) {
            isHeld = true;
            heldTimer = 0f;
        }

        if (isHeld) {
            heldTimer += Time.deltaTime;
            if (player.IsActionButtonDown()) {
                if (heldTimer > SinglePressTime) {
                    if (!heldShooting) {
                        MultiShootStart();
                        heldShooting = true;
                    } else {
                        MultiShootUpdate();
                    }
                }
            } else {
                if (heldTimer <= SinglePressTime) {
                    SingleShoot();
                }

                heldShooting = false;
                isHeld = false;
                playerAsteroids.MoveMod = 1f;
            }
        }
    }

    void MultiShootStart() {
        switch (ShotTypeMulti) {
            case MultiShotTypes.MachineGun:
                MultiShot_MG_Start();
                break;
            default:
                Debug.LogError("Multi Shoot Start Case not supported yet");
                break;
        }
    }

    void MultiShootUpdate() {
        switch (ShotTypeMulti) {
            case MultiShotTypes.MachineGun:
                MultiShot_MG_Update();
                break;
            default:
                Debug.LogError("Multi Shoot Update Case not supported yet");
                break;
        }
    }

    void SingleShoot() {
        switch (ShotTypeSingle) {
            case SingleShotTypes.Shotgun:
                SingleShot_Shotgun();
                break;
            default:
                Debug.LogError("Multi Shoot Update Case not supported yet");
                break;
        }
    }

    void Fire(float spread = 0f, float speed = 0f) {
        GameObject b = bulletPool.GetObject();
        MetaAsteroids_Bullet bullet = b.GetComponent<MetaAsteroids_Bullet>();

        Vector3 forward = -transform.right;
        forward += transform.up * Random.Range(-1f, 1f) * spread;

        bullet.Shoot(
            transform.position - transform.right * BulletSpawnDistance,
            forward.normalized,
            speed,
            playerAsteroids
        );
    }

    // Multishots

    // Machine Gun
    [Header("MultiShot MG")]
    public float MultiShot_MG_Spread = 0.1f;
    public float MultiShot_MG_Frequency = 8f;
    public float MultiShot_MG_BulletSpeed = 30f;
    public float MultiShot_MG_MoveMod = 0.5f;
    void MultiShot_MG_Start() {
        multiShotTimer = 1f / MultiShot_MG_Frequency;
        MultiShot_MG();
        playerAsteroids.MoveMod = MultiShot_MG_MoveMod;
    }
    void MultiShot_MG_Update() {
        multiShotTimer -= Time.deltaTime;
        while (multiShotTimer < 0f) {
            multiShotTimer += (1f / MultiShot_MG_Frequency);
            MultiShot_MG();
        }
    }
    void MultiShot_MG() {
        Fire(MultiShot_MG_Spread, MultiShot_MG_BulletSpeed);
    }

    // Singleshots

    // Shotgun
    [Header("MultiShot MG")]
    public float SingleShot_Shotgun_Shots = 5f;
    public float SingleShot_Shotgun_Spread = 0.25f;
    public float SingleShot_Shotgun_Frequency = 1f;
    public float SingleShot_Shotgun_BulletSpeed = 30f;
    void SingleShot_Shotgun() {
        if (blockShotTimer == 0f) {
            blockShotTimer = (1f / SingleShot_Shotgun_Frequency);
            for (float i = 0f; i < SingleShot_Shotgun_Shots; i += 1f) {
                Fire(SingleShot_Shotgun_Spread, SingleShot_Shotgun_BulletSpeed);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_Player : MonoBehaviour
{
    public Color[] ShipPlayerColors;
    public Color[] ShipOutlineColors;
    public GameObject BackSprite;

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

    // Wormhole factors
    public float WormholeEntranceTime = 0.5f;
    private float wormholeEntranceTimer = 0f;

    private Player player;

    private float freezeTimer = 0f;
    private MetaAsteroids_ExplosionManager explosionManager;
    private MetaAsteroids_Wormhole targetWormhole;
    private MetaAsteroids_MetaGameManager metaGame;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControlComponent>().GetPlayer();
        GetComponent<SpriteRenderer>().color = ShipOutlineColors[player.id];
        BackSprite.GetComponent<SpriteRenderer>().color = ShipPlayerColors[player.id];
        explosionManager = GameObject.Find("Explosions").GetComponent<MetaAsteroids_ExplosionManager>();
        metaGame = Camera.main.GetComponent<MetaAsteroids_MetaGameManager>();
        metaGame.RegisterPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeTimer > 0f) {
            freezeTimer -= Time.deltaTime;
            return;
        }

        if (targetWormhole != null) {
            // Move into wormhole.
            transform.position = Vector3.Lerp(transform.position, targetWormhole.transform.position, 0.025f);
            transform.Rotate(0f, 0f, 1000f * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetWormhole.transform.position) < 0.01f) {
                // Enter the wormhole.
                wormholeEntranceTimer += Time.deltaTime;
                if (wormholeEntranceTimer >= WormholeEntranceTime) {
                    gameObject.SetActive(false);
                    metaGame.PlayerEnteredWormhole(targetWormhole);
                    targetWormhole = null;
                }
            }
        } else {
            // Move freely.
            Vector3 input = player.GetInputAxis();
            float mag = input.magnitude;

            float rotateMag = Mathf.Clamp01((mag - DeadZoneRadius) / (TurnZoneRadius - DeadZoneRadius));
            float moveMag = Mathf.Clamp01((mag - TurnZoneRadius) / (MoveZoneRadius - TurnZoneRadius));

            float rotateSpeed = Mathf.Lerp(MinTurnSpeed, MaxTurnSpeed, rotateMag);
            rotateSpeed *= TurnMod;

            float angle = Mathf.Atan2(-input.y, -input.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);

            float moveSpeed = Mathf.Lerp(MinMoveSpeed, MaxMoveSpeed, moveMag);
            moveSpeed *= MoveMod;
            transform.position = transform.position + transform.right * moveSpeed * Time.deltaTime * -1f;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        MetaAsteroids_Asteroid asteroid = col.gameObject.GetComponent<MetaAsteroids_Asteroid>();
        if (asteroid != null && asteroid.gameObject.activeSelf) {
            OnHitAsteroid();
        } else {
            MetaAsteroids_Bullet bullet = col.gameObject.GetComponent<MetaAsteroids_Bullet>();
            if (bullet != null && bullet.gameObject.activeSelf) {
                OnHitByBullet();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        MetaAsteroids_Wormhole wormhole = col.gameObject.GetComponent<MetaAsteroids_Wormhole>();
        if (wormhole != null && wormhole.gameObject.activeSelf) {
            targetWormhole = wormhole;
            wormholeEntranceTimer = 0f;
        }
    }

    void OnHitByBullet() {
        Debug.Log("OnHitByBullet!");
        gameObject.SetActive(false);
        explosionManager.Explode(transform.position, 6, 0.2f);
    }

    void OnHitAsteroid() {
        Debug.Log("OnHitAsteroid!");
        gameObject.SetActive(false);
        explosionManager.Explode(transform.position, 6, 0.2f);
    }
}

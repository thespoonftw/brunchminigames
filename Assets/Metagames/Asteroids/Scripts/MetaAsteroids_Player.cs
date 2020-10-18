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
    public float MaxMoveSpeed = 10f;
    public float MoveModWhileShooting = 0.5f;
    public float TurnModWhileShooting = 0.5f;

    // Variables to do with shooting
    public float BulletSpawnDistance;
    public float BulletShootSpeed;
    public float SinglePressTime = 0.15f;
    public float ShootFrequency = 8f;
    public float FreezeOnMinorHit = 0.01f;
    public float FreezeOnMajorHit = 0.025f;

    private Player player;
    private ObjectPool bulletPool;

    public float heldTimer = 0f;
    private bool isHeld = false;
    private float shootTimer = 0f;
    private bool heldShooting = false;

    private float freezeTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControlComponent>().GetPlayer();
        GetComponent<SpriteRenderer>().color = ShipOutlineColors[player.id];
        BackSprite.GetComponent<SpriteRenderer>().color = ShipPlayerColors[player.id];

        bulletPool = GameObject.Find("Bullet Pool").GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeTimer > 0f) {
            freezeTimer -= Time.deltaTime;
            return;
        }

        Vector3 input = player.GetInputAxis();
        float mag = input.magnitude;

        float rotateMag = Mathf.Clamp01((mag - DeadZoneRadius) / (TurnZoneRadius - DeadZoneRadius));
        float moveMag = Mathf.Clamp01((mag - TurnZoneRadius) / (MoveZoneRadius - TurnZoneRadius));

        float rotateSpeed = Mathf.Lerp(MinTurnSpeed, MaxTurnSpeed, rotateMag);
        rotateSpeed *= heldShooting ? TurnModWhileShooting : 1f;

        float angle = Mathf.Atan2(-input.y, -input.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);

        float moveSpeed = Mathf.Lerp(MinMoveSpeed, MaxMoveSpeed, moveMag);
        moveSpeed *= heldShooting ? MoveModWhileShooting : 1f;
        transform.position = transform.position + transform.right * moveSpeed * Time.deltaTime * -1f;

        if (!isHeld && player.IsActionButtonPressed()) {
            isHeld = true;
            heldTimer = 0f;
            shootTimer = 0f;
        }

        if (isHeld) {
            heldTimer += Time.deltaTime;
            if (player.IsActionButtonDown()) {
                if (heldTimer > SinglePressTime) {
                    heldShooting = true;

                    while (shootTimer < (1f / ShootFrequency)) {
                        shootTimer += (1f / ShootFrequency);
                        Shoot(0.1f);
                    }
                    shootTimer -= Time.deltaTime;
                }
            } else {
                if (heldTimer <= SinglePressTime) {
                    SingleShoot();
                }

                heldShooting = false;
                isHeld = false;
            }
        }
    }

    void SingleShoot() {
        // Shoot a shotgun style spread.
        Shoot(0.25f);
        Shoot(0.25f);
        Shoot(0.25f);
    }

    void Shoot(float spread = 0f) {
        GameObject b = bulletPool.GetObject();
        MetaAsteroids_Bullet bullet = b.GetComponent<MetaAsteroids_Bullet>();

        Vector3 forward = -transform.right;
        forward += transform.up * Random.Range(-1f, 1f) * spread;

        bullet.Shoot(
            transform.position - transform.right * BulletSpawnDistance,
            forward.normalized,
            BulletShootSpeed,
            this
        );
    }

    public void BigFreeze() {
        freezeTimer += FreezeOnMajorHit;
    }

    public void SmallFreeze() {
        freezeTimer += FreezeOnMinorHit;
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("OnCollisionEnter2D");
    }
}

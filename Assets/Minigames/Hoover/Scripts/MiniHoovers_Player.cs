using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHoovers_Player : MonoBehaviour {
    public float RotateSpeed = 1f;
    public float MoveSpeed = 10f;
    public float InitialBoostSpeed;
    public float Friction;
    public float FrictionExponent;
    public GameObject Visible;
    public GameObject Pointer;
    public float BounceVelocity;
    public float AirParticleFrequency;

    private Player player;
    private Rigidbody2D r;
    private Vector3 startScale;
    private ObjectPool airParticlePool;
    private float timeBetweenAirParticles;
    private float airParticleTimer = 0f;
    private float airParticleSpawnRadius;

    void Start() {
        player = GetComponent<PlayerControlComponent>().GetPlayer();
        Visible.GetComponent<SpriteRenderer>().color = PlayerManager.PlayerColors[player.id];
        Visible.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerManager.PlayerSecondaryColors[player.id];

        Pointer.GetComponent<SpriteRenderer>().color = PlayerManager.PlayerColors[player.id];
        Pointer.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerManager.PlayerSecondaryColors[player.id];

        r = GetComponent<Rigidbody2D>();
        r.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        startScale = Visible.transform.localScale;

        airParticlePool = GetComponent<ObjectPool>();
        timeBetweenAirParticles = 1f / AirParticleFrequency;
        airParticleSpawnRadius = GetComponent<CircleCollider2D>().radius * 2f;
    }

    void ApplyFriction(float target) {
        float f = Friction * Mathf.Pow(r.velocity.magnitude, FrictionExponent);
        r.velocity = Vector3.MoveTowards(
            r.velocity,
            r.velocity.normalized * target,
            f * Time.deltaTime
        );
    }

    void Update() {
        Visible.transform.localScale = Vector3.Lerp(Visible.transform.localScale, startScale, 0.1f);

        if (player.WasActionButtonPressedThisFrame()) {
            // Set initial boost speed.
            r.constraints = RigidbodyConstraints2D.FreezeRotation;
            r.velocity = r.velocity + (Vector2) transform.up * InitialBoostSpeed;
        } else if (player.IsActionButtonPressed()) {
            // Friction to move speed (or lerp)
            if (r.velocity.magnitude < MoveSpeed) {
                r.velocity = Vector3.Lerp(
                    r.velocity,
                    transform.up * MoveSpeed,
                    0.2f
                );
            } else {
                ApplyFriction(MoveSpeed);
            }

            if (MoveSpeed == 0f) {
                transform.Rotate(0f, 0f, RotateSpeed * Time.deltaTime);
            }
        } else {
            // Friction to zero.
            ApplyFriction(0f);
            // Rotate.
            transform.Rotate(0f, 0f, RotateSpeed * Time.deltaTime);
            // r.velocity = Vector3.Lerp(r.velocity, Vector3.zero, 0.2f);
        }

        airParticleTimer += Time.deltaTime;
        while (airParticleTimer >= timeBetweenAirParticles) {
            airParticleTimer -= timeBetweenAirParticles;
            GameObject g = airParticlePool.GetObject();
            g.GetComponent<MiniHoovers_AirParticle>().SetTarget(gameObject, airParticleSpawnRadius);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        MiniHoovers_Slime slime = col.gameObject.GetComponent<MiniHoovers_Slime>();
        if (slime == null) {
            Visible.transform.localScale *= 1.25f;
            Vector3 normal = (Vector3)col.GetContact(0).normal;
            if (player.IsActionButtonPressed() && MoveSpeed > 0f) {
                transform.up = transform.up - normal * 2f * Vector3.Dot(transform.up, normal);
                r.velocity = transform.up * BounceVelocity;
            } else {
                MiniHoovers_Player player = col.gameObject.GetComponent<MiniHoovers_Player>();
                if (player != null) {
                    // Bounce away.
                    r.constraints = RigidbodyConstraints2D.FreezeRotation;
                    Vector3 fromPlayer = (transform.position - player.transform.position).normalized;
                    r.velocity = fromPlayer * BounceVelocity;
                } else {
                    r.velocity += col.GetContact(0).normal * BounceVelocity;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        MiniHoovers_Slime slime = col.gameObject.GetComponent<MiniHoovers_Slime>();
        if (slime != null) {
            slime.GetSucked(this);
        }
    }
}

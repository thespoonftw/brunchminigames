using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBugs_Woodlouse : MonoBehaviour {
    private float airVelocity = 0f;
    private MiniBugs_SpriteBug spriteBug;
    private bool balledUp = false;
    private bool grounded = true;
    private float rotationSpeed;
    private Vector3 groundVelocityDirection;
    private float stamina = 0f;
    private Vector3 groundVelocity = new Vector3(0f, 0f, 0f);

    public float Gravity = 9.8f;
    public float Bounciness = 0.2f;
    public float BallUpJumpPower = 2f;
    public GameObject BallForm;
    public GameObject NonBallForm;
    public bool Go = false;
    public float SettleVelocity = 0.01f;
    public float MaxStamina;
    public float RotationSpeed = 1000f;
    public float GroundAcceleration = 10f;
    public float TouchingGroundAcceleration = 5f;
    public float BalledUpFriction;
    public float UnBalledUpFriction;

    void Start() {
        spriteBug = GetComponent<MiniBugs_SpriteBug>();
        stamina = MaxStamina;
    }

    void Update() {
        bool touchingGround = false;

        if (Go && !balledUp) {
            BallUp();
        }

        // Bounce etc.
        if (!grounded) {
            airVelocity -= Gravity * Time.deltaTime;
            float nextAltitude = spriteBug.Altitude + airVelocity * Time.deltaTime;
            if (nextAltitude <= 0f) {
                nextAltitude = -nextAltitude;
                airVelocity *= -Bounciness;
                touchingGround = true;
            }

            if (Mathf.Abs(airVelocity) > SettleVelocity) {
                spriteBug.Altitude += airVelocity * Time.deltaTime;
            } else {
                spriteBug.Altitude = 0f;
                airVelocity = 0f;
                grounded = false;
                Go = false;
            }
        }

        if (balledUp) {
            stamina -= groundVelocityDirection.magnitude * Time.deltaTime;
            if (stamina <= 0f) {
                UnBallUp();
            }

            BallForm.transform.Rotate(
                0f,
                0f,
                (groundVelocityDirection.x > 0f ? -1f : 1f) * RotationSpeed * Time.deltaTime
            );
            if (grounded) {
                groundVelocity += groundVelocityDirection * GroundAcceleration * Time.deltaTime;
            }

            if (touchingGround) {
                groundVelocity += groundVelocityDirection * TouchingGroundAcceleration * airVelocity;
            }
        }

        Vector3 nextPosition = transform.position + groundVelocity * Time.deltaTime;
        if (nextPosition.x > 34f || nextPosition.x < -34f) {
            groundVelocity.x *= -1f;
            groundVelocityDirection.x *= -1f;
            nextPosition.x = transform.position.x;
        }
        if (nextPosition.y > 19.5f || nextPosition.y < -19.5f) {
            groundVelocity.y *= -1f;
            groundVelocityDirection.y *= -1f;
            nextPosition.y = transform.position.y;
        }
        transform.position = nextPosition;

        groundVelocity = Vector3.MoveTowards(
            groundVelocity,
            Vector3.zero,
            groundVelocity.magnitude * (balledUp ? BalledUpFriction : UnBalledUpFriction) * Time.deltaTime
        );

        if (groundVelocity.x != 0f) {
            NonBallForm.transform.localScale = new Vector3(
                groundVelocity.x > 0f ? 1f : -1f,
                NonBallForm.transform.localScale.y,
                NonBallForm.transform.localScale.z
            );
        }
    }

    public bool IsBalledUp() {
        return balledUp;
    }

    void OnTriggerEnter2D(Collider2D col) {
        MiniBugs_Woodlouse w = col.gameObject.GetComponent<MiniBugs_Woodlouse>();
        if (w != null && w.IsBalledUp() && w != this && !balledUp) {
            BallUp();
        }
    }

    void UnBallUp() {
        Debug.Log("un ball up");
        grounded = true;
        spriteBug.Altitude = 0f;
        airVelocity = 0f;
        Go = false;
        balledUp = false;
        BallForm.SetActive(false);
        NonBallForm.SetActive(true);
    }

    void BallUp() {
        airVelocity = BallUpJumpPower;
        stamina = MaxStamina;
        balledUp = true;
        grounded = false;
        spriteBug.Altitude += airVelocity * Time.deltaTime;
        BallForm.SetActive(true);
        NonBallForm.SetActive(false);

        groundVelocityDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
    }
}

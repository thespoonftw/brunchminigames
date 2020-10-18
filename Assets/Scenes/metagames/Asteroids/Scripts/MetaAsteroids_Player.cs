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

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControlComponent>().GetPlayer();
        GetComponent<SpriteRenderer>().color = ShipOutlineColors[player.id];
        BackSprite.GetComponent<SpriteRenderer>().color = ShipPlayerColors[player.id];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = player.GetInputAxis();
        float mag = input.magnitude;

        float rotateMag = Mathf.Clamp01((mag - DeadZoneRadius) / (TurnZoneRadius - DeadZoneRadius));
        float moveMag = Mathf.Clamp01((mag - TurnZoneRadius) / (MoveZoneRadius - TurnZoneRadius));

        float rotateSpeed = Mathf.Lerp(MinTurnSpeed, MaxTurnSpeed, rotateMag);
        Debug.Log(rotateSpeed);

        float angle = Mathf.Atan2(-input.y, -input.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);

        transform.position = transform.position + transform.right * Mathf.Lerp(MinMoveSpeed, MaxMoveSpeed, moveMag) * Time.deltaTime * -1f;
    }
}

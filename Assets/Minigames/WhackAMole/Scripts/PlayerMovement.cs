using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Mathf;
using System.Linq;

public class PlayerMovement : MonoBehaviour {

    public float playerspeed;
    public float rotatespeed;

    private Player player;

    public List<GameObject> Targets;

    void Start() {

        player = GetComponent<PlayerControlComponent>().GetPlayer();
        Targets = GameObject.Find("Setup").GetComponent<TargetSpawner>().Targets;

    }

    void Update() {

        float tdelta = Time.deltaTime;
        //time since last frame

        float horizontal = player.GetInputAxis().x;
        float vertical = player.GetInputAxis().y;
        //Get inputs: vertical = x, horizontal = z

        Vector3 InputDirection = new Vector3(horizontal, 0, vertical);
        //Direction of input

        float Magnitude = InputDirection.magnitude;

        if (Magnitude > 0.1f) {

            transform.position += tdelta * playerspeed * new Vector3(vertical, 0, -horizontal)/Magnitude;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(InputDirection), rotatespeed * tdelta);
            //Rotate player character direction smoothly towards movement direction

        }
        //If statement checks for deadzone


        if (player.WasActionButtonPressedThisFrame()) {

            foreach(var t in Targets.ToList()) {

                Vector3 PlayertoTarget = transform.position - t.transform.position;

                PlayertoTarget.y = 0;

                if (PlayertoTarget.magnitude < 1f && t != null) {

                    Destroy(t);
                    Targets.Remove(t);

                    GameObject.Find("Score Text").GetComponent<ScoreText>().Score += 1;
                    GameObject.Find("Score Text").GetComponent<ScoreText>().wait = false;

                }

            }

        }

    }

}

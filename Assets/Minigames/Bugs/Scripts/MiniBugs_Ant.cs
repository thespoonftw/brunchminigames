using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointFollower))]
public class MiniBugs_Ant : MonoBehaviour {
    private PointFollower pointFollower;
    private Player player;

    public float MinControlThreshold;
    public float MaxControlThreshold;
    public float MinControlMagnitude;

    private float controlThreshold = 0f;

    public void AssignPlayer(Player p) {
        player = p;
    }

    void Start() {
        pointFollower = GetComponent<PointFollower>();
    }

    void Update() {
        bool addControlThreshold = false;

        if (player != null) {
            Vector3 input = player.GetInputAxis();
            if (input.magnitude >= MinControlMagnitude) {
                addControlThreshold = true;
            }
        }

        if (addControlThreshold) {
            controlThreshold += Time.deltaTime;
        } else {
            controlThreshold -= Time.deltaTime;
        }
        controlThreshold = Mathf.Clamp(controlThreshold, 0f, MaxControlThreshold);

        if (player != null && controlThreshold >= MinControlThreshold) {
            MovePlayer();
        } else {
            // Wander around using AI.
        }
    }

    void MovePlayer() {
        pointFollower.TowardsPointLocal(player.GetInputAxis());
    }

    void MoveAI() {

    }
}

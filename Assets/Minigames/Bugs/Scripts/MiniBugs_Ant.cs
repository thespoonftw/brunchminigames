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
    public float DistanceBetweenPointPositions = 2f;
    public float NextPointDistance = 3f;
    public float PointSearchDistance = 10f;

    private float controlThreshold = 0f;
    private Vector3 lastPointPosition;
    private MiniBugs_AntPoint currentAntPoint;
    private MiniBugs_AntPointManager antPointManager;

    public void AssignPlayer(Player p) {
        player = p;
    }

    void Start() {
        pointFollower = GetComponent<PointFollower>();
        antPointManager = GameObject.Find("Ant Points").GetComponent<MiniBugs_AntPointManager>();
        lastPointPosition = transform.position;
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
            if (Vector3.Distance(lastPointPosition, transform.position) >= DistanceBetweenPointPositions) {
                // Create.
                currentAntPoint = antPointManager.AddPoint(transform.position, currentAntPoint);
                lastPointPosition = transform.position;
            }
        } else {
            // Wander around using AI.
            if (currentAntPoint == null) {
                currentAntPoint = antPointManager.GetNearestAntPoint(transform.position, PointSearchDistance);
            }

            if (currentAntPoint != null) {
                pointFollower.TowardsPoint(currentAntPoint.Position);
                Vector3 toTarget = currentAntPoint.Position - transform.position;
                toTarget.z = 0f;
                if (toTarget.magnitude <= NextPointDistance) {
                    currentAntPoint = currentAntPoint.GetNextPoint();
                }
            }
        }
    }

    void MovePlayer() {
        pointFollower.TowardsPointLocal(player.GetInputAxis());
    }

    void MoveAI() {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBugs_SpriteBug : MonoBehaviour {
    public GameObject Shadow;
    public float ShadowZTarget = 7f;
    public bool UseShadowTarget = false;
    public GameObject[] Bugs;
    public float Altitude = 0f;

    private List<Vector3> bugAnchorPoints = new List<Vector3>();

    void Start() {
        foreach (GameObject bug in Bugs) {
            bugAnchorPoints.Add(new Vector3(
                bug.transform.localPosition.x,
                bug.transform.localPosition.y,
                bug.transform.localPosition.z
            ));
        }
    }

    void Update() {
        for (var i = 0; i < Bugs.Length; i++) {
            GameObject bug = Bugs[i];
            bug.transform.localPosition = bugAnchorPoints[i] + Vector3.up * Altitude;
        }

        if (UseShadowTarget) {
            Shadow.transform.localPosition = new Vector3(
                Shadow.transform.localPosition.x,
                Shadow.transform.localPosition.y,
                ShadowZTarget - transform.position.z
            );
        }
    }
}

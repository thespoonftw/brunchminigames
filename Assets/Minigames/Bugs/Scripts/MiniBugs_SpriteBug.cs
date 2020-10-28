using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBugs_SpriteBug : MonoBehaviour {
    public GameObject Shadow;
    public float ShadowZTarget = 7f;
    public bool UseShadowTarget = false;
    public GameObject[] Bugs;
    public float Altitude = 0f;
    public bool BlobShadow = true;

    private List<Vector3> bugAnchorPoints = new List<Vector3>();
    private Vector3 nonBlobShadowOffset;

    void Start() {
        foreach (GameObject bug in Bugs) {
            bugAnchorPoints.Add(new Vector3(
                bug.transform.localPosition.x,
                bug.transform.localPosition.y,
                bug.transform.localPosition.z
            ));
        }

        if (!BlobShadow) {
            nonBlobShadowOffset = new Vector3(
                Shadow.transform.localPosition.x,
                Shadow.transform.localPosition.y,
                Shadow.transform.localPosition.z
            );
            Shadow.transform.parent = null;
        }
    }

    void Update() {
        for (var i = 0; i < Bugs.Length; i++) {
            GameObject bug = Bugs[i];
            bug.transform.localPosition = bugAnchorPoints[i] + Vector3.up * Altitude;
        }

        if (!BlobShadow) {
            Shadow.transform.rotation = transform.rotation;
            Shadow.transform.position = transform.position + nonBlobShadowOffset;
        }

        if (UseShadowTarget) {
            if (BlobShadow) {
                Shadow.transform.localPosition = new Vector3(
                    Shadow.transform.localPosition.x,
                    Shadow.transform.localPosition.y,
                    ShadowZTarget - transform.position.z
                );
            } else {
                Shadow.transform.position = new Vector3(
                    Shadow.transform.position.x,
                    Shadow.transform.position.y,
                    ShadowZTarget
                );
            }
        }
    }
}

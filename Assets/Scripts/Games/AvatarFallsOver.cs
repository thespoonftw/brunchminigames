using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarFallsOver : MonoBehaviour {

    private float maxAngle = 5f;

    void Start() {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    private void Update() {
        var x = Mathf.Abs(((transform.rotation.eulerAngles.x + 180) % 360) - 180);
        var z = Mathf.Abs(((transform.rotation.eulerAngles.x + 180) % 360) - 180);
        if (z > maxAngle || x > maxAngle) {
            GetComponent<AvatarController>().enabled = false;
        } else {
            GetComponent<AvatarController>().enabled = true;
        }
    }
}

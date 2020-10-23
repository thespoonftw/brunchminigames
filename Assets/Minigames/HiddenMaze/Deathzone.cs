using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiddenMaze {
    public class Deathzone : MonoBehaviour {

        private Vector3 reviveSpot;

        private void Start() {
            reviveSpot = transform.parent.transform.position;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponentInParent<AvatarController>()) {
                other.transform.parent.transform.position = reviveSpot;
                other.transform.parent.transform.rotation = Quaternion.identity;
            }
        }
    }
}


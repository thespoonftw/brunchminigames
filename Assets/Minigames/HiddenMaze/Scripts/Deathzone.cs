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
            var controller = other.GetComponentInParent<AvatarController>();
            if (controller != null) {
                other.transform.parent.transform.position = reviveSpot;
                other.transform.parent.transform.rotation = Quaternion.identity;
                controller.enabled = true;
                other.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiddenMaze {
    public class FakeTile : MonoBehaviour {

        private bool isFake;
        private Rigidbody rb;
        private Vector3 originalPosition;

        public void SetAsFake() {
            originalPosition = transform.position;
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            isFake = true;
        }

        private void OnCollisionEnter(Collision collision) {
            if (!isFake) { return; }
            rb.useGravity = true;
            StartCoroutine(RespawnTime());
        }

        IEnumerator RespawnTime() {
            yield return new WaitForSeconds(3);
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            transform.position = originalPosition;
            transform.rotation = Quaternion.identity;
        }
    }
}



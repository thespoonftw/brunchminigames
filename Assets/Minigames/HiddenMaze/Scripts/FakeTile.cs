using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiddenMaze {
    public class FakeTile : MonoBehaviour {

        private bool isFake;
        private Rigidbody rb;
        private Vector3 originalPosition;
        private GameObject tile;
        private float respawnTime = 0;

        private void OnTriggerEnter(Collider other) {
            if (!isFake) { return; }
            var controller = other.GetComponentInParent<AvatarController>();
            if (controller == null) { return; }
            controller.enabled = false;
            other.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            respawnTime = 3;
        }

        public void SetAsFake(GameObject tile) {
            this.tile = tile;
            originalPosition = tile.transform.position;
            rb = tile.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
            isFake = true;
        }

        private void Update() {
            if (respawnTime > 0) {
                respawnTime -= Time.deltaTime;
                if (respawnTime <= 0) {
                    ResetTile();
                }
            }
        }

        private void ResetTile() {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            tile.transform.position = originalPosition;
            tile.transform.rotation = Quaternion.identity;
        }

    }
}



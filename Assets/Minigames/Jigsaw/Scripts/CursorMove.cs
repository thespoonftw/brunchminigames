using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jigsaw {
    public class CursorMove : MonoBehaviour {

        private Player player;
        public float DeadZoneRadius = 0.1f; // Within this radius, nothing happens.
        public float moveSpeed;
        public float maxHeight;
        public float maxWidth;
        public bool isXZplane;

        void Start() {
            player = GetComponent<PlayerControlComponent>().GetPlayer();
            //GetComponent<SpriteRenderer>().color = ShipOutlineColors[player.id]; // colors should be implemented at some point
        }

        void Update() {
            Vector3 input = player.GetInputAxis();
            float mag = input.magnitude;
            if (mag < DeadZoneRadius) { return; }

            var dx = input.x * moveSpeed * Time.deltaTime;
            var dy = input.y * moveSpeed * Time.deltaTime;

            if (transform.position.x + dx < maxWidth && transform.position.x + dx > -maxWidth) {
                transform.position += new Vector3(dx, 0, 0);
            }
            if (!isXZplane) {
                if (transform.position.y + dy < maxHeight && transform.position.y + dy > -maxHeight) {
                    transform.position += new Vector3(0, dy, 0);
                }
            } else {
                if (transform.position.z + dy < maxHeight && transform.position.z + dy > -maxHeight) {
                    transform.position += new Vector3(0, 0, dy);
                }
            }
        }
    }

}


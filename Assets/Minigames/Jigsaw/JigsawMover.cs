using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jigsaw {
    public class JigsawMover : MonoBehaviour {

        public bool rotationMode;
        private Player player;
        public float DeadZoneRadius = 0.1f; // Within this radius, nothing happens.
        public float DeadTurnRadius = 0.3f; // Within this radius, cannot turn
        public float moveSpeed;
        public float maxHeight;
        public float maxWidth;
        private float RAD_TO_DEG = 180 / Mathf.PI;

        void Start() {
            rotationMode = GameObject.Find("Setup").GetComponent<JigsawSetup>().rotationMode;
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
            if (transform.position.y + dy < maxHeight && transform.position.y + dy > -maxHeight) {
                transform.position += new Vector3(0, dy, 0);
            }

            if (!rotationMode || mag < DeadTurnRadius) { return; }
            var angle = (Mathf.Atan2(input.y, input.x) * RAD_TO_DEG + 180) % 360;
            if (angle < 45 || angle >= 315) {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            } if (angle >= 45 && angle < 135) {
                transform.rotation = Quaternion.Euler(0, 0, 180);
            } if (angle >= 135 && angle < 225) {
                transform.rotation = Quaternion.Euler(0, 0, 270);
            } if (angle >= 225 && angle < 315) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

           

        }
    }

}


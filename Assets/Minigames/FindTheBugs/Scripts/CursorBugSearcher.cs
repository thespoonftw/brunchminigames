using FindTheBugs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangeMe {
    public class CursorBugSearcher : MonoBehaviour {

        public int lives;

        private Player player;

        //private float cooldown = 5f;
        //private float cooldownRemaining = 0;
        private TextMesh textMesh;
        public FindTheBugsSetup setup;

        void Start() {
            player = GetComponent<PlayerControlComponent>().GetPlayer();
            textMesh = GetComponentInChildren<TextMesh>();
            textMesh.text = lives.ToString();
            setup = GameObject.Find("Setup").GetComponent<FindTheBugsSetup>();
        }

        void Update() {

            if (player.WasActionButtonPressedThisFrame() && Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 5f, 1 << 9)) {
                var hidingPlace = hit.collider.GetComponentInParent<HidingPlace>();
                var result = hidingPlace.TryRevealAnt();
                if (!result) {
                    lives--;
                    if (lives <= 0) {
                        setup.KillPlayer(gameObject);
                    } else {
                        textMesh.text = lives.ToString();
                    }
                }
            }

            /*
            if (cooldownRemaining <= 0 && player.WasActionButtonPressedThisFrame()) {
                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 5f, 1 << 9)) {
                    var hidingPlace = hit.collider.GetComponentInParent<HidingPlace>();
                    hidingPlace.Reveal();
                    cooldownRemaining = cooldown;
                }                
            }
            if (cooldownRemaining > 0) {
                cooldownRemaining -= Time.deltaTime;
                
                if (cooldownRemaining <= 0) {
                    textMesh.text = "";
                } else {
                    textMesh.text = ((int)cooldownRemaining + 1).ToString();
                }
            }
            */
        }

    }
}

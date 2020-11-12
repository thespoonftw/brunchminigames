using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FindTheBugs {
    public class AntController : MonoBehaviour {

        private enum AntState {
            EnteringMap,
            MovingIntoBox,
            Hidden,
        }

        public float minSize;
        public float maxSize;
        public float minSpeed;
        public float maxSpeed;
        public List<Color> colors;

        private GameObject destination;
        private AntState currentStage = AntState.EnteringMap;        

        public void Initialise(GameObject destination) {
            this.destination = destination;
            var sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.color = colors[UnityEngine.Random.Range(0, colors.Count)];
            var nav = GetComponent<NavMeshAgent>();
            nav.destination = this.destination.transform.position;
            var scale = UnityEngine.Random.Range(minSize, maxSize);
            transform.localScale = new Vector3(scale, scale, scale);
            nav.speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        }

        private void Update() {
            if (currentStage == AntState.EnteringMap) {
                var dist = Vector3.Distance(transform.position, destination.transform.position);
                if (dist < 1f) {
                    StartCoroutine(StartMovingIntoBox());
                }

            } else if (currentStage == AntState.MovingIntoBox) {
                var dist = Vector3.Distance(transform.position, destination.transform.position);
                if (dist < 0.1f) {
                    currentStage = AntState.Hidden;
                    destination.GetComponent<HidingPlace>().HideAnt(gameObject);
                } else {
                    gameObject.transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, 0.3f * Time.deltaTime);
                }
            }
            
        }

        IEnumerator StartMovingIntoBox() {
            yield return new WaitForSeconds(0.5f);
            GetComponent<NavMeshAgent>().enabled = false;
            transform.rotation = Quaternion.LookRotation(destination.transform.position - transform.position, Vector3.up);
            currentStage = AntState.MovingIntoBox;
        }
    }
}

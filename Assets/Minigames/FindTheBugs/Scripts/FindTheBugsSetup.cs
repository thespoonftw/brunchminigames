using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace FindTheBugs {
    public class FindTheBugsSetup : MonoBehaviour {

        public int numberOfAnts;
        //public int searchTime;
        public Transform antSpawnLocation;
        public Transform antSpawnLocation2;
        public GameObject hidingPlacesParent;
        public GameObject antPrefab;
        //public DefeatTimer defeatTimer;
        //public Text timeRemainingText;
        //public Text bugsRemainingText;
        public Text bannerText;

        private bool isSpawnLeftSide = false;
        private float timeUntilNextAnt = 0.5f;
        private List<GameObject> locations = new List<GameObject>();
        public int antsYetToHide;
        public int antsYetToBeFound;
        private int deadPlayers = 0;

        void Start() {
            //defeatTimer.enabled = false;
            //defeatTimer.timeRemaining = searchTime;
            //bugsRemainingText.text = numberOfAnts.ToString();
            //timeRemainingText.text = searchTime.ToString();
            foreach (Transform t in hidingPlacesParent.transform) {
                locations.Add(t.gameObject);
            }
            antsYetToHide = numberOfAnts;
            antsYetToBeFound = numberOfAnts;
        }

        private void Update() {
            if (numberOfAnts > 0 && timeUntilNextAnt <= 0) {
                timeUntilNextAnt += 0.5f;
                numberOfAnts -= 1;
                var pos = isSpawnLeftSide ? antSpawnLocation : antSpawnLocation2;
                var go = Instantiate(antPrefab, pos.position, pos.rotation, transform);
                isSpawnLeftSide = !isSpawnLeftSide;
                var locationIndex = UnityEngine.Random.Range(0, locations.Count);
                go.GetComponent<AntController>().Initialise(locations[locationIndex]);
                locations.RemoveAt(locationIndex);

            } else if (numberOfAnts > 0 && timeUntilNextAnt > 0) {
                timeUntilNextAnt -= Time.deltaTime;
            }
        }

        public void StartHunting() {
            //defeatTimer.enabled = true;
            UpdateBanner();
            GetComponent<PlayerControlSetup>().enabled = true;
        }

        public void UpdateBanner() {
            bannerText.text = "Find " + antsYetToBeFound.ToString() + " bugs!";
        }

        public void KillPlayer(GameObject player) {
            player.SetActive(false);
            deadPlayers++;
            if (deadPlayers == GetComponent<PlayerControlSetup>().Players.Count) {
                MetagameManager.Instance.EndMinigame(false);
            }
        }

    }
}

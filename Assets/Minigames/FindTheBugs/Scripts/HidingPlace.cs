﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindTheBugs {
    public class HidingPlace : MonoBehaviour {

        public GameObject cross;
        public GameObject sprite;

        private GameObject hiddenAnt;

        private FindTheBugsSetup setup;

        private void Start() {
            setup = GameObject.Find("Setup").GetComponent<FindTheBugsSetup>();
        }

        public void HideAnt(GameObject ant) {
            setup.antsYetToHide--;
            if (setup.antsYetToHide == 0) {
                setup.StartHunting();
            }
            hiddenAnt = ant;
            hiddenAnt.SetActive(false);
        }

        public bool TryRevealAnt() {
            var result = false;
            if (hiddenAnt == null) {
                cross.SetActive(true);                
            } else {
                result = true;
                hiddenAnt.SetActive(true);
                setup.antsYetToBeFound--;
                setup.UpdateBanner();
                if (setup.antsYetToBeFound == 0) { MetagameManager.Instance.EndMinigame(true); }
            }
            sprite.SetActive(false);
            StopCoroutine(RevertAfterDelay());
            StartCoroutine(RevertAfterDelay());
            return result;
        }

        IEnumerator RevertAfterDelay() {
            yield return new WaitForSeconds(3f);
            cross.SetActive(false);
            sprite.SetActive(true);
            if (hiddenAnt != null) {
                Destroy(hiddenAnt);
                hiddenAnt = null;
            }
        }
    }
}

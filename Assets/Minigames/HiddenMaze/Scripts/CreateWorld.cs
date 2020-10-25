using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiddenMaze {
    public class CreateWorld : MonoBehaviour {

        public int numberOfTraps;

        public GameObject endPrefab;
        public GameObject trapPrefab;
        public GameObject cornerPrefab;
        public GameObject victoryGO;

        private List<int> path;
        private List<bool> isRightTurn;

        void Start() {
            while (true) {
                FindPath();
                if (path != null) { break; }
            }
            path.RemoveAt(0);

            var isCornerPiece = false;
            var previousLoc = new Vector3(0, 0, 0);
            for (int i = 0; i < path.Count; i++) {
                var x = ((path[i] / 1000) - 100) * 10;
                var z = ((path[i] % 1000) - 100) * 10;
                var newLoc = new Vector3(x, 0, z);
                var rotation = Quaternion.LookRotation(newLoc - previousLoc, Vector3.up);
                previousLoc = newLoc;
                if (i == path.Count - 1) {
                    Instantiate(endPrefab, newLoc, rotation);
                    victoryGO.transform.position = newLoc;
                } else if (isCornerPiece) {
                    if (isRightTurn[i]) { rotation *= Quaternion.Euler(Vector3.up * -90); }
                    Instantiate(cornerPrefab, newLoc, rotation);
                } else {
                    Instantiate(trapPrefab, newLoc, rotation);
                }
                isCornerPiece = !isCornerPiece;
            }
        }

        private void FindPath() {
            var currentCoord = 100100;
            var currentFacing = 1;
            path = new List<int>() { currentCoord };
            isRightTurn = new List<bool>() { false };

            for (int i = 0; i < numberOfTraps; i++) {
                if (currentFacing == 0) {
                    currentCoord += 1;
                } else if (currentFacing == 1) {
                    currentCoord += 1000;
                } else if (currentFacing == 2) {
                    currentCoord -= 1;
                } else if (currentFacing == 3) {
                    currentCoord -= 1000;
                }

                if (path.Contains(currentCoord)) { path = null;  return; }
                path.Add(currentCoord);                

                if (currentFacing == 0) {
                    currentCoord += 1;
                }
                else if (currentFacing == 1) {
                    currentCoord += 1000;
                }
                else if (currentFacing == 2) {
                    currentCoord -= 1;
                }
                else if (currentFacing == 3) {
                    currentCoord -= 1000;
                }
                if (path.Contains(currentCoord)) { path = null; return; }

                var random = Random.Range(0, 2);
                if (random == 0) {
                    isRightTurn.Add(false);
                    random = -1; ; 
                } else {
                    isRightTurn.Add(true);
                }
                isRightTurn.Add(false);
                path.Add(currentCoord);
                currentFacing += random;
                currentFacing = currentFacing % 4;
            }
        }
    }
}

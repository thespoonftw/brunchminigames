using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HiddenMaze {
    public class TrapController : MonoBehaviour {

        public Transform[] tiles;
        public GameObject tilePrefab;
        public GameObject triggerPrefab;

        void Start() {
            var permutationIndex = Random.Range(0, 18);
            for (int i = 0; i<9; i++) {
                var tile = Instantiate(tilePrefab, tiles[i].position, Quaternion.identity, transform);
                var trigger = Instantiate(triggerPrefab, tiles[i].position, Quaternion.identity, transform);
                if (permutations[permutationIndex][i] == 0) {
                    trigger.GetComponent<FakeTile>().SetAsFake(tile);
                }
            }
        }

        private List<int[]> permutations = new List<int[]>() {

            new int[9] { 1, 0, 0,    1, 1, 0,    1, 1, 0 },
            new int[9] { 1, 0, 0,    1, 1, 1,    0, 1, 1 },
            new int[9] { 1, 0, 0,    1, 1, 1,    1, 0, 1 },

            new int[9] { 0, 1, 0,    1, 1, 0,    1, 1, 0 },
            new int[9] { 0, 1, 0,    0, 1, 1,    0, 1, 1 },
            new int[9] { 0, 1, 0,    1, 1, 1,    1, 0, 1 },

            new int[9] { 0, 0, 1,    1, 1, 1,    1, 1, 0 },
            new int[9] { 0, 0, 1,    0, 1, 1,    0, 1, 1 },
            new int[9] { 0, 0, 1,    1, 1, 1,    1, 0, 1 },

            new int[9] { 1, 1, 0,    1, 0, 0,    1, 0, 0 },
            new int[9] { 1, 1, 0,    0, 1, 0,    0, 1, 0 },
            new int[9] { 1, 1, 0,    0, 1, 1,    0, 0, 1 },

            new int[9] { 0, 1, 1,    1, 1, 0,    1, 0, 0 },
            new int[9] { 0, 1, 1,    0, 1, 0,    0, 1, 0 },
            new int[9] { 0, 1, 1,    0, 0, 1,    0, 0, 1 },

            new int[9] { 1, 0, 1,    1, 1, 1,    1, 0, 0 },
            new int[9] { 1, 0, 1,    1, 1, 1,    0, 1, 0 },
            new int[9] { 1, 0, 1,    1, 1, 1,    0, 0, 1 },

        };
    }

}

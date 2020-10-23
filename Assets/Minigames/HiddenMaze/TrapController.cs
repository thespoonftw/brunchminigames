using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HiddenMaze {
    public class TrapController : MonoBehaviour {

        public Transform[] tiles;
        public GameObject tilePrefab;

        void Start() {
            var permutationIndex = Random.Range(0, 18);
            for (int i = 0; i<9; i++) {
                var tile = Instantiate(tilePrefab, tiles[i].position, Quaternion.identity, transform);
                if (permutations[permutationIndex][i] == 0) {
                    tile.GetComponent<FakeTile>().SetAsFake();
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

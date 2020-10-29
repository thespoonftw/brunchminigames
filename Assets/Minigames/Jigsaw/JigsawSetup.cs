using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jigsaw {
    public class JigsawSetup : MonoBehaviour {

        [Header("Difficulty settings")]
        public bool rotationMode;
        public int gridsize;
        public Texture2D image;

        [Header("prefabs - dont touch")]
        public GameObject jigsawSlotPrefab;        
        public GameObject jigsawPieceParentGO;
        public bool[] correctPieces;

        void Start() {

            correctPieces = new bool[gridsize * gridsize];
            var scale = 5f / gridsize;

            for (int x = 0; x < gridsize; x++) {
                for (int y = 0; y < gridsize; y++) {                    
                    var xOffset = (x - ((gridsize - 1) / 2f)) * scale;
                    var yOffset = (y - ((gridsize - 1) / 2f)) * scale;
                    var go = Instantiate(jigsawSlotPrefab, new Vector3(xOffset, yOffset, 0), Quaternion.identity, transform);                    
                    go.transform.localScale = new Vector3(scale, scale, scale);
                }
            }

            jigsawPieceParentGO.GetComponent<JigsawPieceParent>().SpawnPieces(gridsize, rotationMode, image);            
        }

        public void TryToFitPiece(GameObject piece) {
            int i = 0;
            foreach (Transform p in transform) {
                if (Vector2.Distance(piece.transform.position, p.position) < 1.5f / gridsize) {
                    piece.transform.position = p.position;
                    if (piece.GetComponent<JigsawPiece>().pieceNumber == i && Mathf.Abs((piece.transform.rotation.eulerAngles.z + 180) % 360 - 180) < 5f) { 
                        correctPieces[i] = true;
                        CheckForVictory();
                    }
                    
                    break;
                }
                i++;
            }
        }

        public void CheckForVictory() {
            bool temp = true;
            foreach(var b in correctPieces) {
                if (!b) { temp = false; }
            }
            if (temp) { MetagameManager.Instance.EndMinigame(true); }
        }
    }
}


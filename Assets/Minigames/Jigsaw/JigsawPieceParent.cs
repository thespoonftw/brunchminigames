using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jigsaw {
    public class JigsawPieceParent : MonoBehaviour {

        public List<GameObject> jigsawPieces;
        public GameObject jigsawPiecePrefab;

        public void SpawnPieces(int gridsize, bool isRandomRotation, Texture2D image) {
            var scale = 5f / gridsize;
            var numberOfPieces = gridsize * gridsize;

            Vector2 column1Start = new Vector2(-5, 2.5f);
            Vector2 column2Start = new Vector2(-3.5f, 2.5f);
            Vector2 column3Start = new Vector2(3.5f, 2.5f);
            Vector2 column4Start = new Vector2(5, 2.5f);
            var columnScale = 20f / numberOfPieces;
            var numberList = Enumerable.Range(0, numberOfPieces).ToList();
            numberList.Shuffle();

            var locations = new List<Vector2>();

            if (numberOfPieces > 4) {
                for (int i = 0; i < numberOfPieces; i++) {
                    var level = i / 4;
                    if (i % 4 == 0) {
                        locations.Add(column1Start + new Vector2(0, (level + 0.25f) * -columnScale));
                    }
                    else if (i % 4 == 1) {
                        locations.Add(column2Start + new Vector2(0, (level + 0.75f) * -columnScale));
                    }
                    else if (i % 4 == 2) {
                        locations.Add(column3Start + new Vector2(0, (level + 0.25f) * -columnScale));
                    }
                    else {
                        locations.Add(column4Start + new Vector2(0, (level + 0.75f) * -columnScale));
                    }
                }
            }
            else {
                locations.Add(new Vector3(-4, 2));
                locations.Add(new Vector3(-4, -2));
                locations.Add(new Vector3(4, 2));
                locations.Add(new Vector3(4, -2));
            }

            var currentZOffset = 0f;
            jigsawPieces = new List<GameObject>();
            for (int i = 0; i < numberOfPieces; i++) {
                var rotation = isRandomRotation ? Quaternion.Euler(0, 0, 90 * Random.Range(0, 4)) : Quaternion.identity;
                currentZOffset -= 0.1f;
                var piece = Instantiate(jigsawPiecePrefab, new Vector3(locations[i].x, locations[i].y, currentZOffset), rotation, transform);
                jigsawPieces.Add(piece);
                piece.transform.localScale = new Vector3(scale, scale, scale);
                piece.GetComponent<JigsawPiece>().SetTexture(image, numberList[i], gridsize);
            }
        }

        public void MoveToTop(GameObject piece) {
            jigsawPieces.Remove(piece);
            jigsawPieces.Add(piece);
            var z = 0f;
            foreach (var p in jigsawPieces) {
                p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, z);
                z -= 0.1f;
            }

        }
    }
}


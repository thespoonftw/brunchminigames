using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jigsaw {
    public class CursorJigsawPickup : MonoBehaviour {

        public JigsawPieceParent jigsawPieceParent;
        public Player player;
        //public List<GameObject> pieces => jigsawPieceParent.jigsawPieces;

        private GameObject currentlyCarriedPiece;
        private GameObject jigsawFrameParent;

        void Start() {
            jigsawFrameParent = GameObject.Find("Setup");
            jigsawPieceParent = GameObject.Find("JigsawPieceParent").GetComponent<JigsawPieceParent>();
            player = GetComponent<PlayerControlComponent>().GetPlayer();
        }

        void Update() {
            if (player.WasActionButtonPressedThisFrame()) {
                
                if (currentlyCarriedPiece == null) {

                    var hits = Physics2D.RaycastAll(transform.position, transform.up, 0.3f);
                    foreach (var h in hits) {
                        var jigsawPiece = h.collider.GetComponent<JigsawPiece>();
                        if (jigsawPiece != null && !jigsawPiece.isHeld) {
                            jigsawFrameParent.GetComponent<JigsawSetup>().correctPieces[jigsawPiece.pieceNumber] = false;
                            jigsawPieceParent.MoveToTop(jigsawPiece.gameObject);
                            jigsawPiece.isHeld = true;
                            currentlyCarriedPiece = h.collider.gameObject;
                            jigsawPiece.transform.parent = transform;
                            break;
                        }
                    }

                } else {
                    currentlyCarriedPiece.GetComponent<JigsawPiece>().isHeld = false;
                    currentlyCarriedPiece.transform.parent = jigsawPieceParent.transform;
                    jigsawFrameParent.GetComponent<JigsawSetup>().TryToFitPiece(currentlyCarriedPiece);
                    currentlyCarriedPiece = null;

                }

                /*
                Debug.Log("press");
                foreach (var p in pieces) {
                    if (Mathf.Abs(p.transform.position.x - x) < 1 && Mathf.Abs(p.transform.position.y - y) < 1) {
                        Debug.Log("got!");
                        break;
                    }
                    
                }
                */
            }
        }
    }
}



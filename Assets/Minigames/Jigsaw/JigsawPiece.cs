using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jigsaw {
    public class JigsawPiece : MonoBehaviour {

        public int pieceNumber;

        public bool isHeld;

        public void SetTexture(Texture2D texture, int pieceNumber, int gridSize) {
            this.pieceNumber = pieceNumber;

            float rectSize = texture.width / gridSize;
            var x = (pieceNumber / gridSize) * rectSize;
            var y = (pieceNumber % gridSize) * rectSize;

            

            var newSprite = Sprite.Create(texture, new Rect(x, y, rectSize, rectSize), new Vector2(0.5f, 0.5f), rectSize);
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }
}


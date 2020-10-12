using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour {

    public float speed;

    void Start() {
        PlayerManager.OnXAxis += MoveX;
        PlayerManager.OnYAxis += MoveY;
    }

    void OnDestroy() {
        PlayerManager.OnXAxis -= MoveX;
        PlayerManager.OnYAxis -= MoveY;
    }

    private void MoveX(float x) {
        transform.position += new Vector3(x * Time.deltaTime * speed, 0, 0);
    }

    private void MoveY(float y) {
        transform.position += new Vector3(0, 0, y * Time.deltaTime * speed);
    }


}

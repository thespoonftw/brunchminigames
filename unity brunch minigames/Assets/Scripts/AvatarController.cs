using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour {

    public int PlayerIndex;
    public float speed;

    private Player player;

    void Start() {
        PlayerManager.OnXAxis += MoveX;
        PlayerManager.OnYAxis += MoveY;

        player = PlayerManager.GetPlayer(PlayerIndex);
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

    void Update() {
        transform.position += player.GetInputAxis() * Time.deltaTime * speed;
    }
}

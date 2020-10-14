using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour {

    public int PlayerIndex;
    public float speed;

    private Player player;

    void Start() {
        player = PlayerManager.GetPlayer(PlayerIndex);
        if (player == null) { Destroy(gameObject); }
    }

    /*
    private void MoveX(float x) {
        transform.position += new Vector3(x * Time.deltaTime * speed, 0, 0);
    }

    private void MoveY(float y) {
        transform.position += new Vector3(0, 0, y * Time.deltaTime * speed);
    }
    */

    void Update() {
        var input = player.GetInputAxis() * Time.deltaTime * speed;
        transform.position += new Vector3(input.x, 0, input.y);
    }
}

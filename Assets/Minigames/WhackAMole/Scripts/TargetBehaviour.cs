using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEngine.Mathf;

public class TargetBehaviour : MonoBehaviour {

    public List<GameObject> Players;

    void Start() {

    }

    void Update() {
        /*
        Vector3 PlayertoTarget = transform.position - Player.transform.position;

        PlayertoTarget.y = 0;

        //Debug.Log(PlayertoTarget.magnitude);

        if (PlayertoTarget.magnitude < 1f) {

            
            Debug.Log("Press x to interact");

            if (Input.GetKey(KeyCode.X)) {

                Destroy(gameObject);

                GameObject.Find("Score Text").GetComponent<ScoreText>().Score += 1;
                GameObject.Find("Score Text").GetComponent<ScoreText>().wait = false;

                //Debug.Log("Score++");

            }
            

        }
*/
    }

}





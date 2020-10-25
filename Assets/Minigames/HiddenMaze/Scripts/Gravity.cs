using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiddenMaze {
    public class Gravity : MonoBehaviour {

        void Update() {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -20, 0));
        }
    }

}


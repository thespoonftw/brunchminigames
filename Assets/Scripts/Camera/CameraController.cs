using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CameraController : MonoBehaviour {

    private GameObject focus;

    public void SetFocus(GameObject focus) {
        this.focus = focus;
    }

    private void Update() {
        if (focus != null) {
            transform.position = focus.transform.position;
        }        
    }

}
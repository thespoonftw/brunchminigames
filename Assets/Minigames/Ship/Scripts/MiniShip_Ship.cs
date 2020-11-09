using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniShip_Ship : MonoBehaviour {
    private MiniShip_Ocean ocean;
    public float RotateSpeed = 1f;

    void Start() {
        ocean = Camera.main.GetComponent<MiniShip_Ocean>();        
    }

    void Update() {
        float slope = ocean.GetCentralSlope();

        transform.right = Vector3.MoveTowards(transform.right, new Vector3(1f, -slope, 0f).normalized, RotateSpeed);
        ocean.SetSpeed(transform.rotation.z + Mathf.PI * 0.125f);

        float targetY = ocean.GetCentralY();
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(0f, targetY, transform.position.z),
            1f
        );
    }
}

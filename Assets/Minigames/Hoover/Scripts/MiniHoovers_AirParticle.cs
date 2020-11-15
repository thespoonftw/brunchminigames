using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHoovers_AirParticle : MonoBehaviour {
    public float MinSize = 0.25f;
    public float MaxSize = 0.4f;
    public float MinSpeed = 1f;
    public float MaxSpeed = 2f;
    public float MinStartT = 0f;
    public float MaxStartT = 0.25f;

    private GameObject targetPlayer;
    private float t = 0f;
    private Vector3 offset;
    private float offsetMag;
    private float speed;
    private float radius;

    void Start() {
        float sizeT = Random.Range(0f, 1f);
        transform.localScale = Vector3.one * Mathf.Lerp(MinSize, MaxSize, sizeT);
        speed = Mathf.Lerp(MaxSpeed, MinSpeed, sizeT);
    }

    public void SetTarget(GameObject player, float r) {
        targetPlayer = player;
        radius = r;
        t = 1f - Random.Range(MinStartT, MaxStartT);
        offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
        SetPosition();
    }

    void SetPosition() {
        offsetMag = radius * t;
        Vector3 pos = offset * offsetMag;
        pos.z = 0.01f;

        transform.position = targetPlayer.transform.position + pos;
    }

    void Update() {
        if (targetPlayer != null) {
            t = Mathf.MoveTowards(t, 0f, speed * Time.deltaTime);
            SetPosition();
            if (t == 1f) {
                gameObject.SetActive(false);
            }
        }
    }
}

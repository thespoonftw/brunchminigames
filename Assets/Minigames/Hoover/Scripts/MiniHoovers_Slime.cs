using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHoovers_Slime : MonoBehaviour {
    private MiniHoovers_Player targetPlayer;

    public float Wave1SpeedMin;
    public float Wave1SpeedMax;
    private float wave1Speed;
    private float wave1T = 0f;

    public float Wave1SizeChangeMin;
    public float Wave1SizeChangeMax;

    public float Wave2SpeedMin;
    public float Wave2SpeedMax;
    private float wave2Speed;
    private float wave2T = 0f;

    public float Wave2SizeChangeMin;
    public float Wave2SizeChangeMax;

    private Vector3 startScale;
    private MiniHoovers_SlimeCounter slimeCounter;

    void Start() {
        slimeCounter = Camera.main.GetComponent<MiniHoovers_SlimeCounter>();
        slimeCounter.PlusSlime();
        wave1Speed = Random.Range(Wave1SpeedMin, Wave1SpeedMax);
        wave2Speed = Random.Range(Wave2SpeedMin, Wave2SpeedMax);
        wave1T = Random.Range(0, 10f);
        wave2T = Random.Range(0, 10f);
        startScale = transform.localScale;
    }

    void Update() {
        if (targetPlayer != null) {
            Vector3 pos = Vector3.Lerp(transform.position, targetPlayer.transform.position, 0.1f);
            pos.z = transform.position.z;
            transform.position = pos;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.25f, 0.1f);

            Vector3 toTarget = transform.position - targetPlayer.transform.position;
            toTarget.z = 0f;
            if (toTarget.magnitude < 0.1f) {
                Destroy(gameObject);
            }
        } else {
            wave1T += wave1Speed * Time.deltaTime;
            wave2T += wave2Speed * Time.deltaTime;

            float s1 = Mathf.Lerp(
                Wave1SizeChangeMin,
                Wave1SizeChangeMax,
                (Mathf.Sin(wave1T) + 1f) * 0.5f
            );

            float s2 = Mathf.Lerp(
                Wave2SizeChangeMin,
                Wave2SizeChangeMax,
                (Mathf.Sin(wave2T) + 1f) * 0.5f
            );

            transform.localScale = startScale * (s1 + s2);
        }
    }

    public void GetSucked(MiniHoovers_Player player) {
        targetPlayer = player;
        slimeCounter.MinusSlime();
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
    }
}

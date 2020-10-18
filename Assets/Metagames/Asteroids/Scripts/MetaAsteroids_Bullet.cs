using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_Bullet : MonoBehaviour
{
    private Vector3 direction = new Vector3(0f, 0f, 0f);
    private float speed = 0f;

    public float DespawnDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shoot(Vector3 position, Vector3 direction, float speed) {
        transform.position = position;
        this.direction = direction;
        this.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (transform.position.magnitude > DespawnDistance) {
            gameObject.SetActive(false);
        }
    }
}

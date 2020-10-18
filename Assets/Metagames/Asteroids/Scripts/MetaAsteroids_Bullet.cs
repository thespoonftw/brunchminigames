using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_Bullet : MonoBehaviour
{
    private Vector3 direction = new Vector3(0f, 0f, 0f);
    private float speed = 0f;
    private MetaAsteroids_Player player;

    public float DespawnDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shoot(Vector3 position, Vector3 direction, float speed, MetaAsteroids_Player player) {
        transform.position = position;
        this.direction = direction;
        this.speed = speed;
        this.player = player;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (transform.position.magnitude > DespawnDistance) {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        MetaAsteroids_Asteroid asteroid = col.gameObject.GetComponent<MetaAsteroids_Asteroid>();
        if (asteroid != null) {
            gameObject.SetActive(false);
            if (asteroid.OnHitByBullet()) {
                player.BigFreeze();
            } else {
                player.SmallFreeze();
            }
        }
    }
}

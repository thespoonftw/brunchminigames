using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAsteroids_Cutout : MonoBehaviour {
    private float targetSize;
    private float size;
    private List<GameObject> edges = new List<GameObject>();
    private List<Vector3> directions = new List<Vector3>();
    private float completeTimer = 0f;
    private MetaAsteroids_MetaGameManager meta;
    private bool firstZoom = true;
    private bool zoomPaused = false;
    private bool complete = false;
    private bool zoomingOut = false;

    public GameObject Edge;
    public float CompleteTime = 1f;

    void Start() {
        // top left
        directions.Add(new Vector3(-1f, 1f, 0f));
        edges.Add(GameObject.Instantiate(Edge));
        // top
        directions.Add(new Vector3(0f, 1f, 0f));
        edges.Add(GameObject.Instantiate(Edge));
        // top right
        directions.Add(new Vector3(1f, 1f, 0f));
        edges.Add(GameObject.Instantiate(Edge));
        // right
        directions.Add(new Vector3(1f, 0f, 0f));
        edges.Add(GameObject.Instantiate(Edge));
        // bottom right
        directions.Add(new Vector3(1f, -1f, 0f));
        edges.Add(GameObject.Instantiate(Edge));
        // bottom
        directions.Add(new Vector3(0f, -1f, 0f));
        edges.Add(GameObject.Instantiate(Edge));
        // bottom left
        directions.Add(new Vector3(-1f, -1f, 0f));
        edges.Add(GameObject.Instantiate(Edge));
        // left
        directions.Add(new Vector3(-1f, 0f, 0f));
        edges.Add(GameObject.Instantiate(Edge));

        PositionEdges();
    }

    void Update() {
        if (!zoomPaused) {
            size = Mathf.Lerp(size, targetSize, 0.025f);
            transform.localScale = new Vector3(size, size, 1f);
        }

        if (!zoomPaused && Mathf.Abs(size - targetSize) <= 0.01f) {
            if (zoomingOut) {
                Destroy(gameObject);
                for (int i = 0; i < edges.Count; i++) {
                    Destroy(edges[i].gameObject);
                }
                } else if (firstZoom) {
                firstZoom = false;
                targetSize = 0f;
                zoomPaused = true;
            } else if (!complete) {
                complete = true;
                meta.ZoomComplete();
            }
        }

        if (!firstZoom) {
            completeTimer += Time.deltaTime;
            if (completeTimer >= CompleteTime) {
                zoomPaused = false;
            }
        }

        PositionEdges();
    }

    void PositionEdges() {
        for (int i = 0; i < edges.Count; i++) {
            // edges[i].transform.localScale = transform.localScale;
            edges[i].transform.position = transform.position + new Vector3(
                directions[i].x * (transform.localScale.x * 8f + edges[i].transform.localScale.x * 8f),
                directions[i].y * (9f/16f) * (transform.localScale.x * 8f + edges[i].transform.localScale.x * 8f),
                0f
            );
        }
    }

    public void ZoomIn(Vector3 target, float size, MetaAsteroids_MetaGameManager meta) {
        transform.position = new Vector3(target.x, target.y, transform.position.z);
        targetSize = size;
        this.size = transform.localScale.x;
        PositionEdges();
        this.meta = meta;
    }

    public void ZoomOut() {
        targetSize = 100f;
        zoomingOut = true;
        firstZoom = true;
        zoomPaused = false;
        complete = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject PooledObject;
    public int InitialPoolSize = 0;

    private List<GameObject> prefabs = new List<GameObject>();

    public GameObject GetObject() {
        for (int i = 0; i < prefabs.Count; i++) {
            if (!prefabs[i].activeSelf) {
                prefabs[i].SetActive(true);
                return prefabs[i];
            }
        }

        GameObject g = AddObject();
        g.SetActive(true);
        return g;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < InitialPoolSize; i++) {
            AddObject();
        }
    }

    GameObject AddObject() {
        GameObject g = GameObject.Instantiate(PooledObject);
        g.SetActive(false);
        prefabs.Add(g);
        return g;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlSetup : MonoBehaviour {
    // PlayerPrefab is a GameObject with a PlayerControlComponent script attached to it.
    public GameObject PlayerPrefab;
    public Camera CameraPrefab;
    public Transform[] SpawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        List<Player> players = PlayerManager.GetPlayers();
        int index = 0;
        foreach (Player p in players) {
            Debug.Log("player");
            GameObject g = GameObject.Instantiate(PlayerPrefab);
            g.SetActive(true);

            Transform spawn = null;
            // If spawn positions are specified, use them.
            if (index < SpawnPositions.Length) {
                spawn = SpawnPositions[index];

            // If you've specified some spawn positions, but not enough, we'll use the last in the list but also complain.
            } else if (SpawnPositions.Length > 0) {
                spawn = SpawnPositions[SpawnPositions.Length - 1];
                Debug.LogError("Ran out of spawn positions for the number of players, using the last one.");
            }

            g.transform.position = spawn.position;
            g.transform.rotation = spawn.rotation;

            // If a camera prefab is set, register it in the CameraManager
            if (CameraPrefab != null) {
                var cameraGameobject = Instantiate(CameraPrefab.gameObject, CameraPrefab.transform.position, CameraPrefab.transform.rotation);
                var cameraController = CameraManager.Instance.SetPlayerCamera(cameraGameobject.GetComponent<Camera>(), index);
                cameraController.SetFocus(g);
            }

            g.name = "Player " + p.id;

            // Access the PlayerControlComponent and tell it which Player to get inputs from.
            PlayerControlComponent control = g.GetComponent<PlayerControlComponent>();
            if (control != null) {
                control.SetPlayer(p);
            } else {
                Debug.LogError("PlayerPrefab instantiated by PlayerControlSetup needs a PlayerControlComponent script attached!");
            }

            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Draws little spheres so you can see where the spawn points are
    private void OnDrawGizmos() {
        foreach (var t in SpawnPositions) {
            Gizmos.DrawSphere(t.position, 0.25f);
        }
    }
}

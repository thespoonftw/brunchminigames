using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlSetup : MonoBehaviour {
    // PlayerPrefab is a GameObject with a PlayerControlComponent script attached to it.
    public GameObject PlayerPrefab;
    public Transform[] SpawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        List<Player> players = PlayerManager.GetPlayers();
        int index = 0;
        foreach (Player p in players) {
            GameObject g = GameObject.Instantiate(PlayerPrefab);

            // If spawn positions are specified, use them.
            if (index < SpawnPositions.Length) {
                g.transform.position = SpawnPositions[index].position;
                g.transform.rotation = SpawnPositions[index].rotation;
            } else if (SpawnPositions.Length > 0) {
                // If you've specified some spawn positions, but not enough, we'll use the last in the list but also complain.
                g.transform.position = SpawnPositions[SpawnPositions.Length - 1].position;
                Debug.LogError("Ran out of spawn positions for the number of players, using the last one.");
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

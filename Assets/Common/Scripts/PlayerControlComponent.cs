using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlComponent : MonoBehaviour
{
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPlayer(Player p) {
        player = p;
    }

    public Player GetPlayer() {
        return player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

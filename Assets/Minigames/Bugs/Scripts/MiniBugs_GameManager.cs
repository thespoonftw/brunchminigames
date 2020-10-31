using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public struct MiniBugs_BugSpawnType {
    public GameObject Prefab;
    public int Min;
    public int Max;
    public int MinPerClump;
    public int MaxPerClump;
    public float ClumpSize;
}

public class MiniBugs_GameManager : MonoBehaviour {
    private float w;
    private float h;
    private List<GameObject> extantBugs = new List<GameObject>();
    private List<GameObject> assignedBugs = new List<GameObject>();

    public MiniBugs_BugSpawnType[] BugSpawnTypes;
    
    void Start() {
        w = Camera.main.orthographicSize * Screen.width / Screen.height;
        h = Camera.main.orthographicSize;

        for (int i = 0; i < BugSpawnTypes.Length; i++) {
            MiniBugs_BugSpawnType bugSpawnType = BugSpawnTypes[i];
            int clumps = UnityEngine.Random.Range(bugSpawnType.Min, bugSpawnType.Max);
            for (int c = 0; c < clumps; c++) {
                int perClump = UnityEngine.Random.Range(
                    bugSpawnType.MinPerClump,
                    bugSpawnType.MaxPerClump
                );

                Vector3 clumpCentre = new Vector3(
                    UnityEngine.Random.Range(-w, w),
                    UnityEngine.Random.Range(-h, h),
                    5f
                );

                for (int p = 0; p < perClump; p++) {
                    GameObject bug = GameObject.Instantiate(bugSpawnType.Prefab);
                    bug.transform.position = clumpCentre + new Vector3(
                        UnityEngine.Random.Range(-1f, 1f),
                        UnityEngine.Random.Range(-1f, 1f),
                        0f
                    ).normalized * UnityEngine.Random.Range(0f, bugSpawnType.ClumpSize);

                    extantBugs.Add(bug);
                }
            }
        }

        AssignPlayers();
    }

    void AssignPlayers() {
        List<Player> players = PlayerManager.GetPlayers();
        foreach (Player p in players) {
            List<GameObject> unassigned = extantBugs.Except(assignedBugs).ToList();
            GameObject bug = unassigned[UnityEngine.Random.Range(0, unassigned.Count)];
            assignedBugs.Add(bug);
            if (bug.GetComponent<MiniBugs_Ant>() != null) {
                bug.GetComponent<MiniBugs_Ant>().AssignPlayer(p);
            }
        }
    }

    void Update() {
        
    }
}

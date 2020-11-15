using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;
using System.Linq;

public class TargetSpawner : MonoBehaviour {

    public List<GameObject> Players => GetComponent<PlayerControlSetup>().Players;
    public GameObject TargetPrefab;
    public List<GameObject> Targets;

    public float MinDistFromPlayer;
    //Minimum distance between Target and Player

    public float TargetCreateThreshold;
    //Random number must exceed this to succesfully create a Target on tick

    public float TargetCreatePeriod;
    //Number of seconds between each TargetCreate check

    public float TimeLeft;

    void Start() {

        StartCoroutine(TargetSpawn());

    }
    /*
    void Update() {

        if(Targets.Count > 10) { return; }

        //Debug.Log(Players.Count);

        float rand = Random.value;

        //Debug.Log(rand);

        if (rand > TargetCreateThreshold) {

            Vector3 TargetPosition;

            var Distances = new List<float>();

            do {

                float xcoord = Floor(Random.value * 20f) - 10f;
                float zcoord = Floor(Random.value * 20f) - 10f;
                //Independently create x and z coords - round to give pre-determined positions

                TargetPosition = new Vector3(xcoord, 0.4f, zcoord);
                //Proposed Target position

                
                foreach (var p in Players) {

                    Distances.Add(Vector3.Distance(p.transform.position, TargetPosition));

                }
                //Get distance from Players to Proposed Target position
                

            } while (Distances.Min() < MinDistFromPlayer);

            var Target = Instantiate(TargetPrefab, TargetPosition, Quaternion.identity);
            //Create Target from Prefab

            Targets.Add(Target);

            //Target.GetComponent<TargetBehaviour>().Players = Players;
            //Attach TargetBehaviour script to Target

            //Debug.Log(TargetPosition);


        }


    }
    */
    IEnumerator TargetSpawn() {

        do {

            Debug.Log("Target spawn corountine started.");

            if (Targets.Count < 10) {

                float rand = Random.value;

                Debug.Log(rand);

                if (rand > TargetCreateThreshold) {

                    Vector3 TargetPosition;

                    var Distances = new List<float>();

                    do {

                        float xcoord = Floor(Random.value * 20f) - 10f;
                        float zcoord = Floor(Random.value * 20f) - 10f;
                        //Independently create x and z coords - round to give pre-determined positions

                        TargetPosition = new Vector3(xcoord, 0.4f, zcoord);
                        //Proposed Target position


                        foreach (var p in Players) {

                            Distances.Add(Vector3.Distance(p.transform.position, TargetPosition));

                        }
                        //Get distance from Players to Proposed Target position


                    } while (Distances.Min() < MinDistFromPlayer);

                    var Target = Instantiate(TargetPrefab, TargetPosition, Quaternion.identity);
                    //Create Target from Prefab

                    Targets.Add(Target);

                }
            }

            yield return new WaitForSeconds(1);
            Debug.Log("Targetcreate check done");
            TimeLeft = GameObject.Find("Timer Text").GetComponent<TimeText>().TimeLeft;

        } while (TimeLeft > 0);
    }

}

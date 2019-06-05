using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour {

    [Space (10)]
    [Header ("╔═══════════════[References]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public WaypointManager _WaypointManager;
    [Space (10)]
    public GameObject CollectiblePrefab;
    [Space (10)]
    [Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (50)]
    [Header ("╔═══════════════[Variables]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public int CurrentWaypointID;
    public int ColorID;
    [Space (10)]
    public string CollectibleType;
    public int Spawned = 0;
    [Space (10)]
    public int[] AlreadySpawned = new int[4];
    [Space (10)]
    public bool hasSpawned = false;

    void LateUpdate () {
        if (!hasSpawned) {
            switch (SpawnerManager.Instance.hasSpawned[SpawnerManager.Instance.currentSceneID]) {
                case false:
                    // print ("Generating Spawn in " + SpawnerManager.Instance.currentScene);
                    CurrentSceneUpdate (SpawnerManager.Instance.currentSceneID);
                    MainSpawnerController (false);
                    SpawnerManager.Instance.hasSpawned[SpawnerManager.Instance.currentSceneID] = true;
                    hasSpawned = true;
                    break;
                case true:
                    //  print ("Loading Spawn in " + SpawnerManager.Instance.currentScene);
                    CurrentSceneUpdate (SpawnerManager.Instance.currentSceneID);
                    MainSpawnerController (true);
                    hasSpawned = true;
                    break;
            }
        }
    }

    void CurrentSceneUpdate (int SceneID) {
        switch (SceneID) {
            case 0: //Golden-Liquor
                CollectibleType = "Flower";
                break;
            case 1: //Bank
                CollectibleType = "File";
                break;
            case 2: //Barber-Shop
                CollectibleType = "Shampoo";
                break;
            case 3: //Bowling
                CollectibleType = "Trophy";
                break;
            case 4: //Hotel
                CollectibleType = "Painting";
                break;
            case 5: //Coffee Shop
                CollectibleType = "Coffee";
                break;
            case 6: //Police Station
                CollectibleType = "Wanted";
                break;
        }
    }

    void MainSpawnerController (bool load) {
        if (!load) {
            while (Spawned != 4) {
                if (Spawned == 0) {
                    AttemptSpawn ("White");
                } else if (Spawned == 1) {
                    AttemptSpawn ("Red");
                } else if (Spawned == 2) {
                    AttemptSpawn ("Blue");
                } else if (Spawned == 3) {
                    AttemptSpawn ("Yellow");
                }
            }
        } else {
            while (Spawned != 4) {
                if (Spawned == 0) {
                    SpawnCollectible ("White", -2, SpawnerManager.Instance.CollectiblePositions[CollectibleType][0], SpawnerManager.Instance.CollectibleRotations[CollectibleType][0]);
                } else if (Spawned == 1) {
                    SpawnCollectible ("Red", -2, SpawnerManager.Instance.CollectiblePositions[CollectibleType][1], SpawnerManager.Instance.CollectibleRotations[CollectibleType][1]);
                } else if (Spawned == 2) {
                    SpawnCollectible ("Blue", -2, SpawnerManager.Instance.CollectiblePositions[CollectibleType][2], SpawnerManager.Instance.CollectibleRotations[CollectibleType][2]);
                } else if (Spawned == 3) {
                    SpawnCollectible ("Yellow", -2, SpawnerManager.Instance.CollectiblePositions[CollectibleType][3], SpawnerManager.Instance.CollectibleRotations[CollectibleType][3]);
                }
            }
        }
    }

    void AttemptSpawn (string color) {
        int CurrentAttemptSpawn = Random.Range (0, 8);

        if (!ValidateSpawn (CurrentAttemptSpawn)) {
            AttemptSpawn (color);
        } else {
            SpawnCollectible (color, CurrentAttemptSpawn);
        }
    }

    bool ValidateSpawn (int CurrentAttempt) {
        if ((CurrentAttempt != AlreadySpawned[0]) && (CurrentAttempt != AlreadySpawned[1]) && (CurrentAttempt != AlreadySpawned[2])) {
            return true;
        } else {
            return false;
        }
    }

    void SpawnCollectible (string color, int position, Vector3 savedPosition = default (Vector3), Vector3 savedRotation = default (Vector3)) {

        //print ("Spawning " + color + " " + CollectibleType + " in position " + position + "    [" + savedPosition + "]" + "  with rotation  [" + savedRotation + "]");
        GameObject InstanciatedObject;
        InstanciatedObject = Instantiate (CollectiblePrefab);

        if (position == -2) {
            if (!Menu.Instance.HasBeenCollected (CollectibleType, color)) {
                InstanciatedObject.GetComponent<Collectible> ().Type (CollectibleType, color, savedPosition, savedRotation);
            }
        } else {
            InstanciatedObject.GetComponent<Collectible> ().Type (CollectibleType, color, _WaypointManager.GetComponent<WaypointManager> ().path_objs[position].transform.position, _WaypointManager.GetComponent<WaypointManager> ().path_objs[position].transform.rotation.eulerAngles);

            if (AlreadySpawned[0] == -1) {
                AlreadySpawned[0] = position;
                SpawnerManager.Instance.CollectiblePositions[CollectibleType][0] = _WaypointManager.GetComponent<WaypointManager> ().path_objs[position].transform.position;
                SpawnerManager.Instance.CollectibleRotations[CollectibleType][0] = _WaypointManager.GetComponent<WaypointManager> ().path_objs[position].transform.rotation.eulerAngles;

            } else if (AlreadySpawned[1] == -1) {
                AlreadySpawned[1] = position;
                SpawnerManager.Instance.CollectiblePositions[CollectibleType][1] = _WaypointManager.GetComponent<WaypointManager> ().path_objs[position].transform.position;
                SpawnerManager.Instance.CollectibleRotations[CollectibleType][1] = _WaypointManager.GetComponent<WaypointManager> ().path_objs[position].transform.rotation.eulerAngles;

            } else if (AlreadySpawned[2] == -1) {
                AlreadySpawned[2] = position;
                SpawnerManager.Instance.CollectiblePositions[CollectibleType][2] = _WaypointManager.GetComponent<WaypointManager> ().path_objs[position].transform.position;
                SpawnerManager.Instance.CollectibleRotations[CollectibleType][2] = _WaypointManager.GetComponent<WaypointManager> ().path_objs[position].transform.rotation.eulerAngles;

            } else if (AlreadySpawned[3] == -1) {
                AlreadySpawned[3] = position;
                SpawnerManager.Instance.CollectiblePositions[CollectibleType][3] = _WaypointManager.GetComponent<WaypointManager> ().path_objs[position].transform.position;
                SpawnerManager.Instance.CollectibleRotations[CollectibleType][3] = _WaypointManager.GetComponent<WaypointManager> ().path_objs[position].transform.rotation.eulerAngles;
            }
        }
        Spawned++;
    }

    void ResetAlreadySpawned () {
        for (int i = 0; i < 4; i++) {
            AlreadySpawned[i] = -1;
        }
    }
}
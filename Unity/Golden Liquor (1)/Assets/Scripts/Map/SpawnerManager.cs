using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerManager : Singleton<SpawnerManager> {

    [Space (10)]
    [Header ("╔═══════════════[Variables]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public bool[] hasSpawned = new bool[7];
    public int currentSceneID;
    [Space (10)]
    public string currentScene;
    [Space (10)]
    [Header ("╔═══════════════[Stored Positions]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public Dictionary<string, Vector3[]> CollectiblePositions = new Dictionary<string, Vector3[]> () { { "Painting", new Vector3[4] }, { "Flower", new Vector3[4] }, { "Trophy", new Vector3[4] }, { "Shampoo", new Vector3[4] }, { "File", new Vector3[4] }, { "Coffee", new Vector3[4] }, { "Wanted", new Vector3[4] }, };
    public Dictionary<string, Vector3[]> CollectibleRotations = new Dictionary<string, Vector3[]> () { { "Painting", new Vector3[4] }, { "Flower", new Vector3[4] }, { "Trophy", new Vector3[4] }, { "Shampoo", new Vector3[4] }, { "File", new Vector3[4] }, { "Coffee", new Vector3[4] }, { "Wanted", new Vector3[4] }, };
    //public Dictionary<string, Transform[]> CollectiblePositions = new Dictionary<string, Transform[]> () { { "Painting", new Transform[4] }, { "Flower", new Transform[4] }, { "Trophy", new Transform[4] }, { "Shampoo", new Transform[4] }, { "File", new Transform[4] }, { "Coffee", new Transform[4] }, { "Wanted", new Transform[4] } };
    [Space (10)]
    [Header ("╔═══════════════[Check Dictionary]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public bool CheckDictionaryNow = false;

    public void CheckDictionary () {
        for (int i = 0; i < 4; i++) {
            print ("=====================================================");
            print (i + " Painting " + CollectiblePositions["Painting"][i]);
            print (i + " Flower " + CollectiblePositions["Flower"][i]);
            print (i + " Trophy " + CollectiblePositions["Trophy"][i]);
            print (i + " Shampoo " + CollectiblePositions["Shampoo"][i]);
            print (i + " File " + CollectiblePositions["File"][i]);
            print (i + " Coffee " + CollectiblePositions["Coffee"][i]);
            print (i + " Wanted " + CollectiblePositions["Wanted"][i]);
            print ("=====================================================");
            print (i + " Painting " + CollectibleRotations["Painting"][i]);
            print (i + " Flower " + CollectibleRotations["Flower"][i]);
            print (i + " Trophy " + CollectibleRotations["Trophy"][i]);
            print (i + " Shampoo " + CollectibleRotations["Shampoo"][i]);
            print (i + " File " + CollectibleRotations["File"][i]);
            print (i + " Coffee " + CollectibleRotations["Coffee"][i]);
            print (i + " Wanted " + CollectibleRotations["Wanted"][i]);
            print ("=====================================================");
        }
    }

    void Update () {
        if (CheckDictionaryNow) {
            CheckDictionary ();
            CheckDictionaryNow = false;
        }
        currentScene = SceneManager.GetActiveScene ().name;

        switch (currentScene) {
            case "Golden-Liquor":
                currentSceneID = 0;

                break;
            case "Bank":
                currentSceneID = 1;

                break;
            case "Barber-Shop":
                currentSceneID = 2;

                break;
            case "Bowling":
                currentSceneID = 3;

                break;
            case "Hotel":
                currentSceneID = 4;

                break;
                // case "":
                //     currentSceneID = 5;

                //     break;
            case "Police-Station":
                currentSceneID = 6;

                break;
        }
    }
}
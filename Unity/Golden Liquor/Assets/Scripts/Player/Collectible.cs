using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    [Space (10)]
    [Header ("╔═══════════════[Variables]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public bool CanBeCollected = false;
    [Space (10)]
    public GameObject SelectedCollectible;
    [Space (10)]
    public GameObject[] Collectible_Type; // 7 types [Painting/Flower/Trophy/Shampoo/File/Coffee/Wanted]
    [Space (10)]
    public string Collectible_TypeString;
    [Space (10)]
    public int Collectible_Color; // 4 colors [White/Red/Blue/Yellow]
    [Space (10)]
    [Header ("╔═══════════════[References]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public GameObject[] Painting = new GameObject[4];
    [Space (10)]
    public GameObject[] Flower = new GameObject[4];
    [Space (10)]
    public GameObject[] Trophy = new GameObject[4];
    [Space (10)]
    public GameObject[] Shampoo = new GameObject[4];
    [Space (10)]
    public GameObject[] File = new GameObject[4];
    [Space (10)]
    public GameObject[] Coffee = new GameObject[4];
    [Space (10)]
    public GameObject[] Wanted = new GameObject[4];

    public void Type (string type, string color, Vector3 SpawnPosition, Vector3 SpawnRotation) {
        switch (type) {
            case "Painting":
                Collectible_Type = Painting;
                break;
            case "Flower":
                Collectible_Type = Flower;
                break;
            case "Trophy":
                Collectible_Type = Trophy;
                break;
            case "Shampoo":
                Collectible_Type = Shampoo;
                break;
            case "File":
                Collectible_Type = File;
                break;
            case "Coffee":
                Collectible_Type = Coffee;
                break;
            case "Wanted":
                Collectible_Type = Wanted;
                break;
        }

        switch (color) {
            case "White":
                Collectible_Color = 0;
                break;
            case "Red":
                Collectible_Color = 1;
                break;
            case "Blue":
                Collectible_Color = 2;
                break;
            case "Yellow":
                Collectible_Color = 3;
                break;
        }
        Collectible_TypeString = type;

        SelectedCollectible = Instantiate (Collectible_Type[Collectible_Color], SpawnPosition, Quaternion.Euler (SpawnRotation));
        SelectedCollectible.transform.parent = gameObject.transform;
    }

    public void AttemptCollect () {
        if (CanBeCollected) {
            //print (Collectible_TypeString + "   " + Collectible_Color);
            Menu.Instance.ItemType (Collectible_TypeString, Collectible_Color, true);
            gameObject.SetActive (false);
        } else {
            print ("Get closer to the object or try a new one");
        }
    }

    public void TouchingPlayer (bool touching) {
        CanBeCollected = touching;
    }
}
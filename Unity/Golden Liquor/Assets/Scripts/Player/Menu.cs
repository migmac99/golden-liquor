using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : Singleton<Menu> {
    [Space (10)]
    [Header ("╔═══════════════[Variables]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    bool[] Collectible_Type; // 7 types [Painting/Flower/Trophy/Shampoo/File/Coffee/Wanted]
    [Space (10)]
    public GameObject CurrentCamera;
    [Space (10)]
    public Vector3 CameraTransformMargins;
    [Space (10)]
    public Vector3 CameraRotationMargins;
    [Space (10)]
    [Header ("╔═══════════════[Storage]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public bool[] FlowerCollected = new bool[4];
    [Space (10)]
    public bool[] FileCollected = new bool[4];
    [Space (10)]
    public bool[] ShampooCollected = new bool[4];
    [Space (10)]
    public bool[] TrophyCollected = new bool[4];
    [Space (10)]
    public bool[] PaintingCollected = new bool[4];
    [Space (10)]
    public bool[] CoffeeCollected = new bool[4];
    [Space (10)]
    public bool[] WantedCollected = new bool[4];
    [Space (10)]
    [Header ("╔═══════════════[References]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public GameObject[] FlowerCheck = new GameObject[4];
    [Space (10)]
    public GameObject[] FileCheck = new GameObject[4];
    [Space (10)]
    public GameObject[] ShampooCheck = new GameObject[4];
    [Space (10)]
    public GameObject[] TrophyCheck = new GameObject[4];
    [Space (10)]
    public GameObject[] PaintingCheck = new GameObject[4];
    [Space (10)]
    public GameObject[] CoffeeCheck = new GameObject[4];
    [Space (10)]
    public GameObject[] WantedCheck = new GameObject[4];

    void Start () {

    }

    void Update () {
        if (CurrentCamera != null) {
            transform.position = CurrentCamera.transform.position + CameraTransformMargins;
            transform.rotation = Quaternion.Euler (CurrentCamera.transform.rotation.eulerAngles.x + CameraRotationMargins.x, CurrentCamera.transform.rotation.eulerAngles.y + CameraRotationMargins.y, CurrentCamera.transform.rotation.eulerAngles.z + CameraRotationMargins.z);
            RefreshMenu ();
        }
    }

    public void RefreshMenu () {
        for (int i = 0; i < 4; i++) {
            FlowerCheck[i].SetActive (FlowerCollected[i]);
            FileCheck[i].SetActive (FileCollected[i]);
            ShampooCheck[i].SetActive (ShampooCollected[i]);
            TrophyCheck[i].SetActive (TrophyCollected[i]);
            PaintingCheck[i].SetActive (PaintingCollected[i]);
            FlowerCheck[i].SetActive (FlowerCollected[i]);
            CoffeeCheck[i].SetActive (CoffeeCollected[i]);
            WantedCheck[i].SetActive (WantedCollected[i]);
        }
    }

    public void UpdateCameraTarget () {
        CurrentCamera = GameObject.FindGameObjectWithTag ("MainCamera");
    }

    public void ItemType (string type, int color, bool collected) {
        switch (type) {
            case "Painting":
                Collectible_Type = PaintingCollected;
                break;
            case "Flower":
                Collectible_Type = FlowerCollected;
                break;
            case "Trophy":
                Collectible_Type = TrophyCollected;
                break;
            case "Shampoo":
                Collectible_Type = ShampooCollected;
                break;
            case "File":
                Collectible_Type = FileCollected;
                break;
            case "Coffee":
                Collectible_Type = CoffeeCollected;
                break;
            case "Wanted":
                Collectible_Type = WantedCollected;
                break;
        }
        Collectible_Type[color] = collected;
    }
}
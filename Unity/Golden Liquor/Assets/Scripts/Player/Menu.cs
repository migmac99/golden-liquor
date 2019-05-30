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
    bool[] CheckerType;
    int CheckerColor;
    [Space (10)]
    public GameObject CurrentCamera;
    [Space (10)]
    public Vector3 CameraTransformMargins;
    [Space (10)]
    public Vector3 CameraRotationMargins;
    [Space (10)]
    public string MenuState;
    public float MenuShowSpeed;
    [Space (10)]
    public bool showMenu;
    float overTimeStart = 0f;
    float overTime;
    [Space (10)]
    public Vector3 HiddenPositionStart;
    public Vector3 HiddenPosition;
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
        MenuState = "Hidden";
    }

    void Update () {
        if (CurrentCamera != null) {
            HiddenPosition = CurrentCamera.transform.position + CameraTransformMargins + HiddenPositionStart;
            transform.rotation = Quaternion.Euler (CurrentCamera.transform.rotation.eulerAngles.x + CameraRotationMargins.x, CurrentCamera.transform.rotation.eulerAngles.y + CameraRotationMargins.y, CurrentCamera.transform.rotation.eulerAngles.z + CameraRotationMargins.z);
            ToggleMenu ();
        }

        if (Input.GetKeyDown (KeyCode.Tab)) {
            if ((MenuState != "Show") && (MenuState != "Hide"))
                showMenu = !showMenu;
        }
    }

    public void ToggleMenu () {
        if (showMenu) {
            if (MenuState == "Hidden") {
                MenuState = "Show";
                RefreshMenu ();
            }
        } else {
            if (MenuState == "Showing") {
                MenuState = "Hide";
            }
        }

        if (MenuState == "Hidden") {
            transform.position = HiddenPosition;
            overTime = overTimeStart;
        } else if (MenuState == "Show") {
            if (overTime < 1) {
                overTime += Time.deltaTime * MenuShowSpeed;
            } else {
                MenuState = "Showing";
            }
            transform.position = Vector3.Lerp (HiddenPosition, CurrentCamera.transform.position + CameraTransformMargins, overTime);
        } else if (MenuState == "Showing") {
            transform.position = CurrentCamera.transform.position + CameraTransformMargins;
            overTime = overTimeStart;
        } else if (MenuState == "Hide") {
            if (overTime < 1) {
                overTime += Time.deltaTime * MenuShowSpeed;
            } else {
                MenuState = "Hidden";
            }
            transform.position = Vector3.Lerp (CurrentCamera.transform.position + CameraTransformMargins, HiddenPosition, overTime);
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

    public bool HasBeenCollected (string type, string color) {
        switch (type) {
            case "Painting":
                CheckerType = PaintingCollected;
                break;
            case "Flower":
                CheckerType = FlowerCollected;
                break;
            case "Trophy":
                CheckerType = TrophyCollected;
                break;
            case "Shampoo":
                CheckerType = ShampooCollected;
                break;
            case "File":
                CheckerType = FileCollected;
                break;
            case "Coffee":
                CheckerType = CoffeeCollected;
                break;
            case "Wanted":
                CheckerType = WantedCollected;
                break;
        }

        switch (color) {
            case "White":
                CheckerColor = 0;
                break;
            case "Red":
                CheckerColor = 1;
                break;
            case "Blue":
                CheckerColor = 2;
                break;
            case "Yellow":
                CheckerColor = 3;
                break;
        }

        return CheckerType[CheckerColor];
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : Singleton<Menu> {
    [Space (10)]
    [Header ("╔═══════════════[Variables]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    [Range (0f, 5f)] public float ChatBox_Time;
    [Space (20)]
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
    public GameObject CurrentBorder;
    public GameObject OldBorder;
    [Space (10)]
    public string SelectedType;
    public string SelectedColor;
    [Space (10)]
    [Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (50)]
    [Header ("╔═══════════════[Storage]══════════════════════════════════════════════════════════════════════════════════════════")]
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
    [Space (20)]
    public bool[] Tattoo = new bool[3];
    [Space (10)]
    [Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (50)]
    [Header ("╔═══════════════[References]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public GameObject ChatboxCanvas;
    [Space (20)]
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
    [Space (20)]
    public GameObject[] Tattoos = new GameObject[3];
    [Space (10)]
    [Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (50)]
    [Header ("╔═══════════════[NPC]══════════════════════════════════════════════════════════════════════════════════════════")]
    [Space (10)]
    public string[] ManagerItemType = new string[3];
    [Space (10)]
    public string[] ManagerItemColor = new string[3];
    [Space (10)]
    public string[] SpeakeasyBuilding = new string[3];

    private string BuildingName;

    void Start () {
        MenuState = "Hidden";
        for (int i = 0; i < 3; i++) {
            GenerateSpeakeasy (Random.Range (0, 6), i);
        }
    }

    void Update () {
        if (CurrentCamera != null) {
            HiddenPosition = CurrentCamera.transform.position + CameraTransformMargins + HiddenPositionStart;
            transform.rotation = Quaternion.Euler (CurrentCamera.transform.rotation.eulerAngles.x + CameraRotationMargins.x, CurrentCamera.transform.rotation.eulerAngles.y + CameraRotationMargins.y, CurrentCamera.transform.rotation.eulerAngles.z + CameraRotationMargins.z);
            ToggleMenu ();
        }

        if (Input.GetKey (KeyCode.Tab)) {
            if ((SceneManager.GetActiveScene ().name != "StartMenu") && (SceneManager.GetActiveScene ().name != "GameOverLose") && (SceneManager.GetActiveScene ().name != "GameOverWin") && (SceneManager.GetActiveScene ().name != "Preload")) {
                showMenu = true;
            }
        } else { showMenu = false; }

    }

    public void ToggleMenu () {
        if (showMenu) {
            if (MenuState == "Hidden") {
                MenuState = "Show";
                transform.position = HiddenPosition;
                overTime = overTimeStart;
                RefreshMenu ();
            }
        } else {
            if (MenuState == "Showing") {
                MenuState = "Hide";
                transform.position = CurrentCamera.transform.position + CameraTransformMargins;
                overTime = overTimeStart;
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

            if (CurrentBorder != null) {
                CurrentBorder.SetActive (false);
            }
            if (OldBorder != null) {
                OldBorder.SetActive (false);
            }
            SelectedColor = "";
            SelectedType = "";
        }

        if (MenuState == "Selecting") {
            transform.position = CurrentCamera.transform.position + CameraTransformMargins;
            RefreshMenu ();

            if ((Input.GetKey (KeyCode.Escape)) || (Input.GetKey (KeyCode.Tab))) {
                MenuState = "Hide";
            }

            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast (ray, out hit)) {
                if (hit.collider.gameObject.tag == "Selection") {
                    if (CurrentBorder != null) {
                        OldBorder = CurrentBorder;
                        OldBorder.SetActive (false);

                        SelectedColor = hit.collider.gameObject.name;
                        SelectedType = hit.collider.gameObject.transform.parent.name;
                    }
                    CurrentBorder = hit.collider.gameObject.transform.Find ("Hover").gameObject;
                    CurrentBorder.SetActive (true);
                }
            }
            Debug.DrawRay (ray.origin, ray.direction * 1000, Color.blue);
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

            if (i < 3) {
                Tattoos[i].SetActive (Tattoo[i]);
            }
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

    public void GenerateSpeakeasy (int Building, int SpeakeasyID) {
        switch (Building) {
            case 0:
                BuildingName = "Bank";
                break;
            case 1:
                BuildingName = "Barber-Shop";
                break;
            case 2:
                BuildingName = "Bowling";
                break;
            case 3:
                BuildingName = "Hotel";
                break;
            case 4:
                BuildingName = "Cafe";
                break;
            case 5:
                BuildingName = "Police-Station";
                break;
        }

        if ((SpeakeasyBuilding[0] != BuildingName) && (SpeakeasyBuilding[1] != BuildingName) && (SpeakeasyBuilding[2] != BuildingName)) {
            GenerateManagerLock (SpeakeasyID, Random.Range (0, 7));
            SpeakeasyBuilding[SpeakeasyID] = BuildingName;
            BuildingName = "";
        } else {
            GenerateSpeakeasy (Random.Range (0, 6), SpeakeasyID);
            BuildingName = "";
        }
    }

    void GenerateManagerLock (int SpeakeasyID, int RandomItem, string RandomItemType = "") {
        switch (RandomItem) {
            case 0:
                RandomItemType = "Painting";
                break;
            case 1:
                RandomItemType = "Flower";
                break;
            case 2:
                RandomItemType = "Trophy";
                break;
            case 3:
                RandomItemType = "Shampoo";
                break;
            case 4:
                RandomItemType = "File";
                break;
            case 5:
                RandomItemType = "Coffee";
                break;
            case 6:
                RandomItemType = "Wanted";
                break;
        }

        if ((ManagerItemType[0] != RandomItemType) && (ManagerItemType[1] != RandomItemType) && (ManagerItemType[2] != RandomItemType)) {
            ManagerItemType[SpeakeasyID] = RandomItemType;
            ManagerItemColor[SpeakeasyID] = GenerateRandomColor ();
        } else {
            GenerateManagerLock (SpeakeasyID, Random.Range (0, 7));
        }
    }

    string GenerateRandomColor () {
        string GeneratedColor = "White";
        int GeneratedColorID;

        GeneratedColorID = Random.Range (0, 4);

        switch (GeneratedColorID) {
            case 0:
                GeneratedColor = "White";
                break;
            case 1:
                GeneratedColor = "Red";
                break;
            case 2:
                GeneratedColor = "Blue";
                break;
            case 3:
                GeneratedColor = "Yellow";
                break;
        }

        return GeneratedColor;
    }
}
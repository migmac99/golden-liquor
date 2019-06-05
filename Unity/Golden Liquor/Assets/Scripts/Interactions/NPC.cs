using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour {

    public GameObject ChatboxCanvas;
    [Space (10)]
    public GameObject LoadingController;
    public GameObject Doors;
    [Space (10)]
    public LayerMask NPC_Layer;
    public string NPC_Type;
    [Space (10)]
    public bool InRange = false;
    [Space (10)]
    public int ManagerID = -1;
    [Space (10)]
    ChatBox Chat;
    [Space (20)]
    public GameObject[] NPCS;

    private Animator _Animator;

    void Start () {
        Chat = ChatboxCanvas.GetComponent<ChatBox> ();
        _Animator = GetComponent<Animator> ();

        if ((NPC_Type == "Manager") || (NPC_Type == "Tattooer")) {
            NPCS = GameObject.FindGameObjectsWithTag ("NPC_Normal");
            if ((NPC_Type == "Manager") && (ManagerID != -1)) {
                GenerateValidNPC (true);
                GenerateValidNPC (false);
            }
        }

        for (int i = 0; i < 3; i++) {
            if (SceneManager.GetActiveScene ().name == Menu.Instance.SpeakeasyBuilding[i]) {
                ManagerID = i;
            } else if (SceneManager.GetActiveScene ().name == "SpeakEasy_" + i) {
                if (NPC_Type == "Tattooer") {
                    ManagerID = i;
                    Doors.GetComponent<SwitchScene> ().DesiredScene = Menu.Instance.SpeakeasyBuilding[i];
                }
            }
        }
    }

    void Update () {
        Ray CameraToMouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit CameraToMouseHitInfo;

        if (Input.GetMouseButtonDown (1)) { //if mouse right clicked then try to collect item
            if (InRange) {
                if (Physics.Raycast (CameraToMouseRay, out CameraToMouseHitInfo, 1000, NPC_Layer)) {
                    Speak ();
                }
            }
        }

        if (NPC_Type == "Manager") {
            if ((Menu.Instance.MenuState == "Selecting") && (Menu.Instance.CurrentBorder != null)) {
                if (Input.GetKeyDown (KeyCode.Space)) {
                    TradeItem (Menu.Instance.SelectedType, Menu.Instance.SelectedColor);
                    Menu.Instance.MenuState = "Hide";
                }
            }
        }
    }

    void Speak () {
        switch (NPC_Type) {
            case "Manager":
                if (ManagerID != -1) {
                    Chat.TextContent = "YOU GOT SOMETHING FOR ME?";

                    StartCoroutine (Coroutines.Instance.Countdown (Menu.Instance.ChatBox_Time + 1f, () => {
                        Chat.TextContent = "HOVER MOUSE TO SELECT A COLLECTED ITEM";
                        Chat.Show ();
                        StartCoroutine (Coroutines.Instance.Countdown (Menu.Instance.ChatBox_Time, () => { Chat.Show (false); }));

                        StartCoroutine (Coroutines.Instance.Countdown (Menu.Instance.ChatBox_Time + 1f, () => {
                            Chat.TextContent = "PRESS SPACE TO CONFIRM SELECTION";
                            Chat.Show ();
                            StartCoroutine (Coroutines.Instance.Countdown (Menu.Instance.ChatBox_Time, () => { Chat.Show (false); Menu.Instance.MenuState = "Selecting"; }));
                        }));
                    }));
                } else {
                    Chat.TextContent = "HI!";
                }
                break;
            case "Tattooer":
                if (!Menu.Instance.Tattoo[ManagerID]) {
                    Chat.TextContent = "YOU JUST GOT A NEW MARK!";
                    Menu.Instance.Tattoo[ManagerID] = true;
                } else {
                    Chat.TextContent = "I ALREADY GAVE YOU THIS MARK!";
                }
                break;
            case "Normal":
                Chat.TextContent = "HELLO";
                break;
            case "Type":
                Chat.TextContent = "WHAT A UNIQUE " + Menu.Instance.ManagerItemType[ManagerID].ToUpper () + "!";
                break;
            case "Color":
                Chat.TextContent = Menu.Instance.ManagerItemColor[ManagerID].ToUpper () + " IS SUCH A NICE COLOR";
                break;
        }

        Chat.Show ();
        StartCoroutine (Coroutines.Instance.Countdown (Menu.Instance.ChatBox_Time, () => { Chat.Show (false); }));
    }

    public void TouchingPlayer (bool touching) {
        InRange = touching;
    }

    public void TradeItem (string type, string color) {
        if ((type == Menu.Instance.ManagerItemType[ManagerID]) && (color == Menu.Instance.ManagerItemColor[ManagerID])) {
            LoadingController.GetComponent<LoadScene> ().LoadLevel ("SpeakEasy_" + (ManagerID));
        } else {
            Menu.Instance.MenuState = "Hide";
            _Animator.SetBool ("PlayerDead", true);
            StartCoroutine (Coroutines.Instance.Countdown (3f, () => { LoadingController.GetComponent<LoadScene> ().LoadLevel ("GameOverLose"); }));
        }
    }

    void GenerateValidNPC (bool color, int RandomNPC = -1) {
        for (int i = 0; i <= 50; i++) {
            if (i < 50) {
                RandomNPC = Random.Range (0, 6);

                if (ValidNPC (RandomNPC)) {
                    if (color) {
                        NPCS[RandomNPC].GetComponent<NPC> ().NPC_Type = "Color";
                    } else {
                        NPCS[RandomNPC].GetComponent<NPC> ().NPC_Type = "Type";
                    }
                    break;
                } else {
                    print ("Skippint iteration " + i + "  " + RandomNPC);
                    continue;
                }
            } else {
                print ("MAX Gen tries exceeded, falling back to default values");
                if (color) {
                    NPCS[0].GetComponent<NPC> ().NPC_Type = "Color";
                } else {
                    NPCS[1].GetComponent<NPC> ().NPC_Type = "Type";
                }
            }
        }
    }

    public bool ValidNPC (int GeneratedNPC) {
        if (NPCS[GeneratedNPC].GetComponent<NPC> ().NPC_Type == "Normal") {
            return true;
        } else {
            return false;
        }
    }
}
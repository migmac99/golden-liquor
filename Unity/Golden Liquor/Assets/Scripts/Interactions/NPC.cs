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

        if ((NPC_Type == "Manager") || (NPC_Type == "Tattooer")) {
            NPCS = GameObject.FindGameObjectsWithTag ("NPC_Normal");
            if ((NPC_Type == "Manager") && (ManagerID != -1)) {
                GenerateValidNPC (true);
                GenerateValidNPC (false);
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
                    int RandomEntryManager = Random.Range (0, 3);

                    switch (RandomEntryManager) {
                        case 0:
                            Chat.TextContent = "WELCOME";
                            break;
                        case 1:
                            Chat.TextContent = "DO YOU NEED ANYTHING?";
                            break;
                        case 2:
                            Chat.TextContent = "WHAT CAN I HELP YOU WITH?";
                            break;
                    }
                }
                break;
            case "Tattooer":
                if (!Menu.Instance.Tattoo[ManagerID]) {
                    Chat.TextContent = "YOU JUST GOT A NEW MARK!";
                    Menu.Instance.Tattoo[ManagerID] = true;
                } else {
                    int RandomEntryTattoer = Random.Range (0, 3);

                    switch (RandomEntryTattoer) {
                        case 0:
                            Chat.TextContent = "I ALREADY GAVE YOU THIS MARK!";
                            break;
                        case 1:
                            Chat.TextContent = "DONT PUSH IT!";
                            break;
                        case 2:
                            Chat.TextContent = "STOP INSISTING!";
                            break;
                    }
                }
                break;
            case "Normal":
                int RandomEntryNormal = Random.Range (0, 6);

                switch (RandomEntryNormal) {
                    case 0:
                        Chat.TextContent = "HELLO";
                        break;
                    case 1:
                        Chat.TextContent = "WHAT A NICE DAY";
                        break;
                    case 2:
                        Chat.TextContent = "YES?";
                        break;
                    case 3:
                        Chat.TextContent = "WHAT DO YOU WANT?";
                        break;
                    case 4:
                        Chat.TextContent = "LOVELY";
                        break;
                    case 5:
                        Chat.TextContent = "INTERESTING...";
                        break;
                }
                break;
            case "Type":
                switch (Menu.Instance.ManagerItemType[ManagerID]) {
                    case "Painting":
                        int RandomEntryPainting = Random.Range (0, 3);
                        switch (RandomEntryPainting) {
                            case 0:
                                Chat.TextContent = "I TEND TO APPRECIATE MODERN ART";
                                break;
                            case 1:
                                Chat.TextContent = "THE HOTEL'S ART COLLECTION IS AMAZING";
                                break;
                            case 2:
                                Chat.TextContent = "I REALLY GET LOST WHEN I LOOK AT SOME PAINTINGS";
                                break;
                        }
                        break;

                    case "Flower":
                        int RandomEntryFlower = Random.Range (0, 3);
                        switch (RandomEntryFlower) {
                            case 0:
                                Chat.TextContent = "THE SMELL OF FLOWERS REALLY MAKES ME HAPPY";
                                break;
                            case 1:
                                Chat.TextContent = "FLOWERS ARE WAY MORE IMPORTANT THAN PEOPLE THING";
                                break;
                            case 2:
                                Chat.TextContent = "I NEED TO BUY FLOWERS FOR MY WIFE";
                                break;
                        }
                        break;

                    case "Trophy":
                        int RandomEntryTrophy = Random.Range (0, 3);
                        switch (RandomEntryTrophy) {
                            case 0:
                                Chat.TextContent = "THE BOWLING'S TROPHY COLLECTION IS AMAZING";
                                break;
                            case 1:
                                Chat.TextContent = "WINNING A TROPHY MUST FEEL NICE";
                                break;
                            case 2:
                                Chat.TextContent = "I HEARD THAT THE MANAGER LOVES TROPHIES";
                                break;
                        }
                        break;

                    case "Shampoo":
                        int RandomEntryShampoo = Random.Range (0, 3);
                        switch (RandomEntryShampoo) {
                            case 0:
                                Chat.TextContent = "I COULD USE A GOOD SHAMPOO... HAHA, I'M JOKING!";
                                break;
                            case 1:
                                Chat.TextContent = "I DON'T GET THE DIFFERENCE BETWEEN SHAMPOO AND SOAP";
                                break;
                            case 2:
                                Chat.TextContent = "WHY IS SHAMPOO CALLED SHAMPOO?";
                                break;
                        }
                        break;

                    case "File":
                        int RandomEntryFile = Random.Range (0, 3);
                        switch (RandomEntryFile) {
                            case 0:
                                Chat.TextContent = "I NEED TO BUY FOLDERS FOR MY FILES, I'M SO MESSY";
                                break;
                            case 1:
                                Chat.TextContent = "DO YOU THINK PEOPLE WILL STILL USE FILES IN THE FUTURE?";
                                break;
                            case 2:
                                Chat.TextContent = "I HEARD THE MANAGER LOST AN IMPORTANT FILE";
                                break;
                        }
                        break;

                    case "Coffee":
                        int RandomEntryCoffee = Random.Range (0, 3);
                        switch (RandomEntryCoffee) {
                            case 0:
                                Chat.TextContent = "I LOVE THE SMELL OF COFFEE IN THE MORNING";
                                break;
                            case 1:
                                Chat.TextContent = "I CAN'T DO ANYTHING WITHOUT MY MORNING COFFEE";
                                break;
                            case 2:
                                Chat.TextContent = "COFFEE IS MORE ADDICTIVE THAN ALCOHOL, IT SHOULD BE ILLEGAL";
                                break;
                        }
                        break;

                    case "Wanted":
                        int RandomEntryWanted = Random.Range (0, 3);
                        switch (RandomEntryWanted) {
                            case 0:
                                Chat.TextContent = "THE POLICE ARE LOOKING FOR SOMEONE";
                                break;
                            case 1:
                                Chat.TextContent = "SOMEONE ESCAPED POLICE CUSTODY";
                                break;
                            case 2:
                                Chat.TextContent = "THERE IS A WANTED SIGN FOR SOMEONE";
                                break;
                        }
                        break;
                        // Chat.TextContent = "WHAT A UNIQUE " + Menu.Instance.ManagerItemType[ManagerID].ToUpper () + "!";
                }
                break;
            case "Color":
                int RandomEntryColor = Random.Range (0, 3);
                switch (RandomEntryColor) {
                    case 0:
                        Chat.TextContent = Menu.Instance.ManagerItemColor[ManagerID].ToUpper () + " IS SUCH A NICE COLOR";
                        break;
                    case 1:
                        Chat.TextContent = "THE MANAGER'S FAVORITE COLOR IS " + Menu.Instance.ManagerItemColor[ManagerID].ToUpper () + "!";
                        break;
                    case 2:
                        Chat.TextContent = Menu.Instance.ManagerItemColor[ManagerID].ToUpper () + " IS WHAT YOU ARE LOOKING FOR";
                        break;
                }
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
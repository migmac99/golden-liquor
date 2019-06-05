using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public GameObject ChatboxCanvas;
    [Space (10)]
    public string Stage;
    [Space (10)]
    [Range (0, 10)] public int ChatBoxTimer;

    void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            switch (Stage) {
                case "Start":
                    ChatboxCanvas.GetComponent<ChatBox> ().TextContent = "HI! WELCOME TO GOLDEN LIQUOR";
                    ChatboxCanvas.GetComponent<ChatBox> ().Show ();
                    StartCoroutine (Coroutines.Instance.Countdown (ChatBoxTimer, () => {
                        ChatboxCanvas.GetComponent<ChatBox> ().Show (false);

                        StartCoroutine (Coroutines.Instance.Countdown (1f, () => {
                            ChatboxCanvas.GetComponent<ChatBox> ().TextContent = "PLEASE COMPLETE THIS SMALL TUTORIAL";
                            ChatboxCanvas.GetComponent<ChatBox> ().Show ();
                            StartCoroutine (Coroutines.Instance.Countdown (ChatBoxTimer, () => { ChatboxCanvas.GetComponent<ChatBox> ().Show (false); }));
                        }));
                    }));
                    break;

                case "Collect":
                    ChatboxCanvas.GetComponent<ChatBox> ().TextContent = "COLLECT THIS FLOWER";
                    ChatboxCanvas.GetComponent<ChatBox> ().Show ();
                    StartCoroutine (Coroutines.Instance.Countdown (ChatBoxTimer, () => { ChatboxCanvas.GetComponent<ChatBox> ().Show (false); }));
                    break;

                case "Inventory":
                    ChatboxCanvas.GetComponent<ChatBox> ().TextContent = "OPEN YOUR INVENTORY";
                    ChatboxCanvas.GetComponent<ChatBox> ().Show ();
                    StartCoroutine (Coroutines.Instance.Countdown (ChatBoxTimer, () => { ChatboxCanvas.GetComponent<ChatBox> ().Show (false); }));
                    break;

                case "Interact":
                    ChatboxCanvas.GetComponent<ChatBox> ().TextContent = "IF YOU ARE CLOSE ENOUGH, YOU CAN ALSO INTERACT WITH NPCS";
                    ChatboxCanvas.GetComponent<ChatBox> ().Show ();
                    StartCoroutine (Coroutines.Instance.Countdown (ChatBoxTimer, () => { ChatboxCanvas.GetComponent<ChatBox> ().Show (false); }));
                    break;

                case "Door":
                    ChatboxCanvas.GetComponent<ChatBox> ().TextContent = "OPEN THE DOOR";
                    ChatboxCanvas.GetComponent<ChatBox> ().Show ();
                    StartCoroutine (Coroutines.Instance.Countdown (ChatBoxTimer, () => { ChatboxCanvas.GetComponent<ChatBox> ().Show (false); }));
                    break;
            }
        }
    }
}
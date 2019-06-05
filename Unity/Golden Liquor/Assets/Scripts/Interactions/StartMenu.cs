using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public GameObject LoadingController;
    [Space (10)]
    public GameObject Play;
    public GameObject Help;
    public GameObject Exit;
    public GameObject _Menu;
    [Space (10)]
    public GameObject CurrentBorder;
    public GameObject OldBorder;
    public bool StarterBorder;

    void Start () {
        StarterBorder = true;
        //CurrentBorder = gameObject.transform.Find ("Play").gameObject.transform.parent.gameObject.transform.Find ("Hover").gameObject;
        CurrentBorder = Play.gameObject.transform.parent.gameObject.transform.Find ("Hover").gameObject;
        CurrentBorder.SetActive (true);
    }

    void Update () {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown (KeyCode.Space)) {
            if ((CurrentBorder.transform.parent.gameObject.transform.Find ("Play"))) {
                LoadingController.GetComponent<LoadScene> ().LoadLevel ("Golden-Liquor");
                ResetVariables ();
            }
        }

        if (Physics.Raycast (ray, out hit)) {
            if ((Input.GetKeyDown (KeyCode.Space)) || (Input.GetMouseButtonDown (0))) {
                if ((CurrentBorder.transform.parent.gameObject.transform.Find ("Play"))) {
                    LoadingController.GetComponent<LoadScene> ().LoadLevel ("Golden-Liquor");
                    ResetVariables ();
                } else if (CurrentBorder.transform.parent.gameObject.transform.Find ("Help")) {
                    LoadingController.GetComponent<LoadScene> ().LoadLevel ("Help");
                } else if (CurrentBorder.transform.parent.gameObject.transform.Find ("Exit")) {
                    Application.Quit ();
                } else if (CurrentBorder.transform.parent.gameObject.transform.Find ("Menu")) {
                    LoadingController.GetComponent<LoadScene> ().LoadLevel ("StartMenu");
                }
            }

            if (hit.collider.gameObject.tag == "Selection") {
                if (CurrentBorder != null) {
                    OldBorder = CurrentBorder;
                    OldBorder.SetActive (false);
                }
                CurrentBorder = hit.collider.gameObject.transform.parent.gameObject.transform.Find ("Hover").gameObject;
                StarterBorder = false;
                CurrentBorder.SetActive (true);
            }
        }
        Debug.DrawRay (ray.origin, ray.direction * 1000, Color.red);
    }

    void ResetVariables () {
        ///////////////////////////////////////////////////
        ////////////////////StartMenu//////////////////////
        ///////////////////////////////////////////////////
        //Storage
        Menu.Instance.FlowerCollected = new bool[4];
        Menu.Instance.FileCollected = new bool[4];
        Menu.Instance.ShampooCollected = new bool[4];
        Menu.Instance.TrophyCollected = new bool[4];
        Menu.Instance.PaintingCollected = new bool[4];
        Menu.Instance.CoffeeCollected = new bool[4];
        Menu.Instance.WantedCollected = new bool[4];
        //Tattoos
        Menu.Instance.Tattoo = new bool[3];
        //NPC
        Menu.Instance.ManagerItemType = new string[3];
        Menu.Instance.ManagerItemColor = new string[3];
        Menu.Instance.SpeakeasyBuilding = new string[3];
        //Menu Start();
        Menu.Instance.MenuState = "Hidden";
        for (int i = 0; i < 3; i++) {
            Menu.Instance.GenerateSpeakeasy (Random.Range (0, 6), i);
        }
        ///////////////////////////////////////////////////
        //////////////////SpawnerManager///////////////////
        ///////////////////////////////////////////////////
        //Variables
        SpawnerManager.Instance.hasSpawned = new bool[7];
        //Dictionaries
        SpawnerManager.Instance.CollectiblePositions = new Dictionary<string, Vector3[]> () { { "Painting", new Vector3[4] }, { "Flower", new Vector3[4] }, { "Trophy", new Vector3[4] }, { "Shampoo", new Vector3[4] }, { "File", new Vector3[4] }, { "Coffee", new Vector3[4] }, { "Wanted", new Vector3[4] }, };
        SpawnerManager.Instance.CollectibleRotations = new Dictionary<string, Vector3[]> () { { "Painting", new Vector3[4] }, { "Flower", new Vector3[4] }, { "Trophy", new Vector3[4] }, { "Shampoo", new Vector3[4] }, { "File", new Vector3[4] }, { "Coffee", new Vector3[4] }, { "Wanted", new Vector3[4] }, };
    }
}
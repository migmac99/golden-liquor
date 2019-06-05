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

    //void OnCollisionEnter (Collision other) {
    void Update () {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown (0)) {

            if (Physics.Raycast (ray, out hit)) {

                if (hit.collider.gameObject == Play) {
                    LoadingController.GetComponent<LoadScene> ().LoadLevel ("Golden-Liquor");
                    ResetVariables ();
                } else if (hit.collider.gameObject == Help) {
                    LoadingController.GetComponent<LoadScene> ().LoadLevel ("Help");
                } else if (hit.collider.gameObject == Exit) {
                    Application.Quit ();
                } else if (hit.collider.gameObject == _Menu) {
                    LoadingController.GetComponent<LoadScene> ().LoadLevel ("StartMenu");
                }
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
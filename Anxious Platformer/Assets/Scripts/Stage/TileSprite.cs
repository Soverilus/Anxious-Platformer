using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSprite : MonoBehaviour {
    GameController myGameController;
    [SerializeField]
    int whichTile;
    public GameObject[] mySprites;

    private void Start() {
        HandleSprites();
    }


    public void HandleSprites() {
        myGameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        whichTile = myGameController.whichTile;
        for (int i = 0; i < mySprites.Length; i++) {
            mySprites[i].SetActive(false);
            if (i == whichTile) {
                mySprites[i].SetActive(true);
            }
        }
    }
}

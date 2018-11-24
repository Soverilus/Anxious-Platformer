using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSymbolChooser : MonoBehaviour {

    public GameObject[] mySymbols;

    private void Start() {
        ChooseTile();
    }

    public void ChooseTile() {
        int myChance = Random.Range(1, 101);
        if (myChance >= 1 && myChance <= 89) {
            mySymbols[0].SetActive(true);
            mySymbols[1].SetActive(false);
            mySymbols[2].SetActive(false);
        }
        else if (myChance >= 90 && myChance <= 94) {
            mySymbols[1].SetActive(true);
            mySymbols[0].SetActive(false);
            mySymbols[2].SetActive(false);
        }
        else if (myChance >= 95) {
            mySymbols[2].SetActive(true);
            mySymbols[0].SetActive(false);
            mySymbols[1].SetActive(false);
        }
    }
}

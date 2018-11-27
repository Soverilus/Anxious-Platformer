using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class GameController : MonoBehaviour {

    [Header("Possible Texts")]
    public string[] myFall;
    public string[] myLoss;
    public string[] myTrapLoss;
    public string[] myTimeLoss;
    [Space(10)]

    [HideInInspector]
    public int whichTile;
    public Text myDisplay;
    int myDisplayChoice;

    public void Victory() {

    }

    private void Start() {
       myDisplayChoice = Random.Range(0, 5);
    }

    public void Death(int type) {
        switch (type) {
            default:
                Debug.LogError("No Death Type Recorded! Check MovementStats collisions and GameController.Death() for possible problems.");
                break;

            case 0:
                FallSwitch();
                break;

            case 1:
                EnemySwitch();
                break;

            case 2:
                TrapSwitch();
                break;

            case 3:
                TimeSwitch();
                break;
        }
    }

    void FallSwitch() {
        myDisplay.text = myFall[myDisplayChoice];
    }

    void EnemySwitch() {
        myDisplay.text = myLoss[myDisplayChoice];
    }

    void TrapSwitch() {
        myDisplay.text = myTrapLoss[myDisplayChoice];
    }

    void TimeSwitch() {
        myDisplay.text = myTimeLoss[myDisplayChoice];
    }
}

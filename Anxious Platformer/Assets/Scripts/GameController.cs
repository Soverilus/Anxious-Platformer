using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class GameController : MonoBehaviour {

    [Header("Possible Texts")]
    public string[] victory;
    public string[] myFall;
    public string[] myLoss;
    public string[] myTrapLoss;
    public string[] myTimeLoss;
    bool hasFadedIn = false;
    public float myFadeInSpeed;
    [Space(10)]

    [HideInInspector]
    public int whichTile;
    public Text myDisplay;
    int myDisplayChoice;

    public void Victory() {

    }

    private void Start() {
        myDisplayChoice = Random.Range(0, 5);
        myDisplay.color = new Color(myDisplay.color.r, myDisplay.color.g, myDisplay.color.b, 0f);
    }

    public void Death(string deathType) {
        switch (deathType) {
            case "Fall":
                FallSwitch();
                break;

            case "Enemy":
                EnemySwitch();
                break;

            case "Trap":
                TrapSwitch();
                break;

            case "Time":
                TimeSwitch();
                break;

            default:
                Debug.LogError("No Death Type Recorded! Check MovementStats collisions and GameController.Death() for possible problems.");
                break;
        }
    }

    void FallSwitch() {
        myDisplay.text = myFall[myDisplayChoice];
        if (!hasFadedIn) {
            FadeTextToFullAlpha(myFadeInSpeed, myDisplay);
        }
    }

    void EnemySwitch() {
        myDisplay.text = myLoss[myDisplayChoice];
        if (!hasFadedIn) {
            FadeTextToFullAlpha(myFadeInSpeed, myDisplay);
        }
    }

    void TrapSwitch() {
        myDisplay.text = myTrapLoss[myDisplayChoice];
        if (!hasFadedIn) {
            FadeTextToFullAlpha(myFadeInSpeed, myDisplay);
        }
    }

    void TimeSwitch() {
        myDisplay.text = myTimeLoss[myDisplayChoice];
        if (!hasFadedIn) {
            FadeTextToFullAlpha(myFadeInSpeed, myDisplay);
        }
    }

    void FadeTextToFullAlpha(float t, Text i) {
        if (i.color.a < 1.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
        }
        else hasFadedIn = true;
    }

    /*void FadeTextToZeroAlpha(float t, Text i) {
        if (i.color.a > 0.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
        }
        else hasFadedOut = true;
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour {
    public ShowNumberToText mySNTT;
    StatHandler mySH;
    MovementStats myMS;
    [Header("Possible Texts")]
    public string[] victory;
    public string[] myFall;
    public string[] myLoss;
    public string[] myTrapLoss;
    public string[] myTimeLoss;
    bool hasFadedIn = false;
    public float myFadeInSpeed;
    bool endDay = false;
    [Space(10)]

    [HideInInspector]
    public int whichTile;
    public Text myDisplay;
    int myDisplayChoice;

    public void Victory() {
        myDisplay.text = victory[Mathf.Clamp(mySH.dayNumber, 0,victory.Length)];
        if (!hasFadedIn) {
            FadeTextToFullAlpha(myFadeInSpeed, myDisplay);
        }
        endDay = true;
    }

    private void Start() {
        mySH = GameObject.FindGameObjectWithTag("StatHandler").GetComponent<StatHandler>();
        myMS = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementStats>();
        myDisplayChoice = Random.Range(0, 5);
        myDisplay.color = new Color(myDisplay.color.r, myDisplay.color.g, myDisplay.color.b, 0f);
    }

    private void Update() {
        if (endDay) {
            mySNTT.useTime = false;
            Invoke("RestartScene", 4f);
        }
    }

    public void Death(string deathType) {
        switch (deathType) {
            case "Fall":
                FallSwitch();
                myMS.isDead = true;
                endDay = true;
                break;

            case "Enemy":
                EnemySwitch();
                myMS.isDead = true;
                endDay = true;
                break;

            case "Trap":
                TrapSwitch();
                myMS.isDead = true;
                endDay = true;
                break;

            case "Time":
                TimeSwitch();
                myMS.isDead = true;
                endDay = true;
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

    void RestartScene() {
        SceneManager.LoadScene(1);
    }
}

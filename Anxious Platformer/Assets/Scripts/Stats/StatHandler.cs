﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour {
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    public int dayNumber;

    public bool canGoRight;
    public bool canGoLeft;
    public float gravityMultiplier;
    public float myMoveSpeedMult;
    public float jumpForceMult;
    public int whichTileRand;
    public float timeLeft;

    //updates at the start of the first frame called by MovementStats;
    public void StatHandlerStart(float maxTime) {
        if (dayNumber == 7) {
            dayNumber = 0;
        }
        if (dayNumber >= 2 && Random.Range(0, 4) > 2) {
            //canGoRight = false;
        }
        else {
            canGoRight = true;
        }
        if (dayNumber >= 2 && Random.Range(0, 2) == 1) {
            //canGoLeft = false;
        }
        else {
            canGoLeft = true;
        }
        if (dayNumber >= 2) {
            gravityMultiplier = Random.Range(0.9f, 1.1f);
            myMoveSpeedMult = Random.Range(0.9f, 1.1f);
            jumpForceMult = Random.Range(0.9f, 1.1f);
        }
        else {
            gravityMultiplier = 1f;
            myMoveSpeedMult = 1f;
            jumpForceMult = 1f;
        }
        
        if (dayNumber <= 3 && dayNumber > 1) {
            whichTileRand = Random.Range(2, 4);
        }
        else if (dayNumber <= 4) {
            whichTileRand = Random.Range(1, 4);
        }
        else if (dayNumber > 4) {
            whichTileRand = Random.Range(0, 4);
        }
        timeLeft = Random.Range(0.5f, 1.75f) * maxTime;
    }

    //updates at the END of the first frame called by MovementStats;
    public void StatHandlerLateStart() {

    }

    //updates at the start of every frame called by MovementStats
    public void StatHandlerUpdate() {

    }
}

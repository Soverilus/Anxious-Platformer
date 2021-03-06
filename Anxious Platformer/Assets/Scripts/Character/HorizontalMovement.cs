﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HorizontalMovement {
    MovementStats myMS;
    private Rigidbody2D myRB;
    private PhysicsMaterial2D myPM;
    Collider2D playerCol;
    float friction;
    Vector2 input;
    public float myMoveSpeed;
    public GameObject player;
    bool canGoRight;
    bool canGoLeft;
    bool hasDisplayedLeft = false;
    bool hasDisplayedRight = false;
    float maxClamp;
    float minClamp;
    Text myText;
    GameController myGC;

    public void NewStart() {
        GrabStats();
        myGC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void GrabStats() {
        //player = GameObject.FindGameObjectWithTag("Player");
        myMS = player.GetComponent<MovementStats>();
        myRB = player.GetComponent<Rigidbody2D>();
        myPM = myMS.myPhysMaterial;
        friction = myPM.friction;
        myMoveSpeed = myMS.myMoveSpeed;
        playerCol = player.GetComponent<Collider2D>();
        canGoLeft = myMS.canGoLeft;
        canGoRight = myMS.canGoRight;
    }

    public void NewUpdate() {
        SendStats();
        if (myMS.myEndGoal <= player.transform.position.x) {
            input.x = 1;
            myMS.IWon();
        }
        else {
            if (canGoLeft) {
                minClamp = -1f;
            }
            else {
                minClamp = 0f;
                CantMoveLeft();
            }
            if (canGoRight) {
                maxClamp = 1f;
            }
            else {
                maxClamp = 0f;
                CantMoveRight();
            }
            input = new Vector2(Mathf.Clamp(Input.GetAxisRaw("Horizontal"), minClamp, maxClamp), Input.GetAxisRaw("Vertical"));
        }
        if (myMS.grounded) {
            MovementFunct();
        }
    }

    void SendStats() {
        myMS.input = input;
    }

    void CantMoveLeft() {
        if (!hasDisplayedLeft) {
            hasDisplayedLeft = true;
            //queue text fade for can't go left
            Debug.Log("I refuse to go backwards");
        }
    }

    void CantMoveRight() {
        if (!hasDisplayedRight) {
            hasDisplayedRight = true;
            //queue text fade for can't go right
            Debug.Log("If I go that way I'll get hurt");
        }
    }

    void MovementFunct() {
        float myXMovement = Mathf.Clamp(myRB.velocity.x + 0.05f * myMoveSpeed * input.x, -myMoveSpeed, myMoveSpeed);
        //if my input is towards the right
        if (input.x > 0f && myRB.velocity.x >= 0f) {
            if (canGoRight) {
                myRB.velocity = new Vector2(myXMovement, myRB.velocity.y);
            }
        }

        //if my input is towards the left
        if (input.x < 0f && myRB.velocity.x <= 0f) {
            if (canGoLeft) {
                myRB.velocity = new Vector2(myXMovement, myRB.velocity.y);
            }
        }
        if ((myRB.velocity.x > 0f && input.x <= 0f) ||
            (myRB.velocity.x < 0f && input.x >= 0f)){
            myRB.velocity = new Vector2(myRB.velocity.x * 0.95f, myRB.velocity.y);
            if (input.x != 0f) {
                if (input.x > 0f) {
                    myRB.AddForce(new Vector2(0.01f * myMoveSpeed, 0f));
                }
                if (input.x < 0f) {
                    myRB.AddForce(new Vector2(-0.01f * myMoveSpeed, 0f));
                }
            }
        }
    }
}

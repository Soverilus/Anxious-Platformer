using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement {
    MovementStats myMS;
    private Rigidbody2D myRB;
    private PhysicsMaterial2D myPM;
    Collider2D playerCol;
    float friction;
    Vector2 input;
    public float myMoveSpeed;
    public GameObject player;

    public void NewStart() {
        GrabStats();
    }

    void GrabStats() {
        //player = GameObject.FindGameObjectWithTag("Player");
        myMS = player.GetComponent<MovementStats>();
        myRB = player.GetComponent<Rigidbody2D>();
        myPM = myMS.myPhysMaterial;
        friction = myPM.friction;
        myMoveSpeed = myMS.myMoveSpeed;
        playerCol = player.GetComponent<Collider2D>();
    }

    public void NewUpdate() {
        SendStats();
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (myMS.grounded) {
            MovementFunct();
        }
    }

    void SendStats() {
        myMS.input = input;
    }

    void MovementFunct() {
        float myXMovement = Mathf.Clamp(myRB.velocity.x + 0.05f * myMoveSpeed * input.x, -myMoveSpeed, myMoveSpeed);
        //if my input is towards the right
        if (input.x > 0f && myRB.velocity.x >= 0f) {
            myRB.velocity = new Vector2(myXMovement, myRB.velocity.y);
        }
        //if my input is towards the left
        if (input.x < 0f && myRB.velocity.x <= 0f) {
            myRB.velocity = new Vector2(myXMovement, myRB.velocity.y);
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

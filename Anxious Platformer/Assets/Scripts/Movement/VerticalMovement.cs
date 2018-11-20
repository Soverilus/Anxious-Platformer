using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement {
    MovementStats myMS;
    /*these floats are the force you use to jump, the max time you want your jump to be allowed to happen,
     * and a counter to track how long you have been jumping*/
    public GameObject player;
    public GameObject groundChecker;
    CapsuleCollider2D gCheck;
    Collider2D playerCol;
    LayerMask whatIsGround;
    float jumpForce;
    float jumpTime;
    float jumpTimeCounter;
    float myMoveSpeed;
    bool isHoldingJump;
    bool grounded;
    bool stoppedJumping;
    bool canJump;


    /*this bool is to tell us whether you are on the ground or not
     * the layermask lets you select a layer to be ground; you will need to create a layer named ground(or whatever you like) and assign your
     * ground objects to this layer.
     * The stoppedJumping bool lets us track when the player stops jumping.*/
    float myHorzMovement;
    bool hasMovedThisFrame;
    //public bool stoppedWallJumping;

    /*the public transform is how you will detect whether we are touching the ground.
     * Add an empty game object as a child of your player and position it at your feet, where you touch the ground.
     * the float groundCheckRadius allows you to set a radius for the groundCheck, to adjust the way you interact with the ground*/

    public Transform groundCheck;
    public float groundCheckRadius;

    //You will need a rigidbody to apply forces for jumping, in this case I am using Rigidbody 2D because we are trying to emulate Mario :)
    private Rigidbody2D myRB;

    private PhysicsMaterial2D myPM;
    float origFriction;

    public void NewStart() {
        GrabStats();
        gCheck = groundChecker.GetComponent<CapsuleCollider2D>();
        //sets the jumpCounter to whatever we set our jumptime to in the editor
        jumpTimeCounter = jumpTime;
        myRB = player.GetComponent<Rigidbody2D>();
        myPM = myMS.myPhysMaterial;
    }

    void GrabStats() {
        //player = GameObject.FindGameObjectWithTag("Player");
        myMS = player.GetComponent<MovementStats>();
        whatIsGround = myMS.whatIsGround;
        jumpForce = myMS.jumpForce;
        jumpTime = myMS.jumpTime;
        myMoveSpeed = myMS.myMoveSpeed;
        myPM = myMS.myPhysMaterial;
        origFriction = myPM.friction;
        playerCol = player.GetComponent<Collider2D>();
        canJump = myMS.canJump;
    }

    public void NewUpdate() {
        SendStats();
        GroundChecker();
        if (canJump) {
            JumpFunct();
        }
        else CantJump();
    }

    void CantJump() {
        if (Input.GetAxisRaw("Jump") > 0) {
            if (isHoldingJump == false) {
                isHoldingJump = true;
                //queue text fade for can't go left
                Debug.Log("I'm too scared to jump");
            }
        }
    }

    void SendStats() {
        myMS.jumpTimeCounter = jumpTimeCounter;
        myMS.grounded = grounded;
        myMS.stoppedJumping = stoppedJumping;
        myMS.isHoldingJump = isHoldingJump;
        myMS.friction = myPM.friction;
    }

    void GroundChecker() {
        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
        if (gCheck.IsTouchingLayers(whatIsGround)) {
            grounded = true;
        }
        else grounded = false;
        //if we are grounded...
        if (grounded) {
            if (stoppedJumping) {
                //the jumpcounter is whatever we set jumptime to in the editor.
                if (!isHoldingJump) {
                    jumpTimeCounter = 0;
                }
            }
        }
    }

    void JumpFunct() {
        if (Input.GetAxisRaw("Jump") <= 0) {
            myPM.friction = origFriction;
            playerCol.enabled = false;
            playerCol.enabled = true;
        }
        //if you press down the jump button...
        if (Input.GetAxisRaw("Jump") > 0) {
            myPM.friction = 0f;
            playerCol.enabled = false;
            playerCol.enabled = true;
            if (!isHoldingJump) {
                isHoldingJump = true;
                //and you are on the ground...
                if (stoppedJumping) {
                    if (grounded) {
                        //allow for slight horizontal adjustments
                        myRB.velocity = new Vector2(Mathf.Clamp(myRB.velocity.x + myMS.input.x, -myMoveSpeed, myMoveSpeed), jumpForce);
                        stoppedJumping = false;
                    }
                }
            }
        }
        //if you keep holding down the jump button...
        if (isHoldingJump) {
            if (!grounded) {
                myRB.velocity = new Vector2(Mathf.Clamp(myRB.velocity.x + myMS.input.x * 0.05f, -myMoveSpeed, myMoveSpeed), myRB.velocity.y);
            }
            if (!stoppedJumping) {
                //and your counter hasn't reached zero...
                if (jumpTimeCounter < jumpTime) {
                    jumpTimeCounter += Time.deltaTime;
                    //allow for slight horizontal adjustments
                    myRB.velocity = new Vector2(Mathf.Clamp(myRB.velocity.x + myMS.input.x * 0.05f, -myMoveSpeed, myMoveSpeed), jumpForce);
                }
            }
        }
        //if you stop holding down the jump button...
        if (Input.GetAxisRaw("Jump") == 0) {
            if (!grounded) {
                myRB.velocity = new Vector2(Mathf.Clamp(myRB.velocity.x + myMS.input.x * 0.05f, -myMoveSpeed, myMoveSpeed), Mathf.Clamp(myRB.velocity.y, -jumpForce*2f, jumpForce*2f));
            }
            isHoldingJump = false;
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the Groundchecker function.
            jumpTimeCounter = jumpTime;
            stoppedJumping = true;
        }
    }
}
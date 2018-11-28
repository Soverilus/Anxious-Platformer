using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AnimatorController : MonoBehaviour {
    Animator myAnim;
    GameObject myPlayer;
    MovementStats myMS;
    Rigidbody2D playerRB;
    Rigidbody2D myRB;
    SpriteRenderer mySR;
    bool hasAddedForce = false;

    float myMaxMoveSpeed;
    float myMoveSpeed;

    private void Start() {
        mySR = GetComponent<SpriteRenderer>();
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        playerRB = myPlayer.GetComponent<Rigidbody2D>();
        myMS = myPlayer.GetComponent<MovementStats>();
        myAnim = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody2D>();
        myMaxMoveSpeed = myMS.myMoveSpeed;
    }

    private void Update() {
        SpriteChecker();
        GroundedCheck();
        RunningCheck();
        JumpAndFallCheck();
        DeathCheck();
    }

    void SpriteChecker() {
        if (playerRB.velocity.x > 0f) {
            mySR.flipX = false;
        }
        else if (playerRB.velocity.x < 0f) {
            mySR.flipX = true;
        }
    }

    void GroundedCheck() {
        if (myMS.grounded) {
            myAnim.SetBool("Grounded", true);
            myAnim.SetBool("Falling", false);
        }
        else {
            myAnim.SetBool("Grounded", false);
        }
    }

    void RunningCheck() {
        if (playerRB.velocity.x != 0f) {
            myMoveSpeed = Mathf.Abs(playerRB.velocity.x);
            myAnim.SetBool("Running", true);
        }
        else {
            myAnim.SetBool("Running", false);
        }
        if (myAnim.GetBool("Running")) {
            myAnim.speed = myMoveSpeed / myMaxMoveSpeed;
        }
    }

    void JumpAndFallCheck() {
        if (playerRB.velocity.y != 0f) {
            if (playerRB.velocity.y > 0f) {
                myAnim.SetBool("Jump", true);
            }
            else {
                myAnim.SetBool("Falling", true);
                myAnim.SetBool("Jump", false);
            }
        }
    }

    void DeathCheck() {
        if (myMS.isDead == true) {
            myAnim.speed = 1f;
            myAnim.SetTrigger("Death");
            myRB.isKinematic = false;
            playerRB.velocity = Vector3.zero;
            myPlayer.transform.position = myPlayer.transform.position;
            playerRB.isKinematic = true;
            if (!hasAddedForce) {
                myRB.AddForce(Vector2.up * 25f *-Physics.gravity);
                hasAddedForce = true;
            }
            Invoke("RestartScene", 4f);
        }
    }

    void RestartScene() {
        SceneManager.LoadScene(1);
    }
}

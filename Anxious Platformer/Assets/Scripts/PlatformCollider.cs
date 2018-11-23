using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour {
    GameObject player;
    CapsuleCollider2D playerCol;
    Rigidbody2D playerRB;
    BoxCollider2D myBCol;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCol = player.GetComponent<CapsuleCollider2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
        myBCol = GetComponent<BoxCollider2D>();
	}
	
	void Update () {
        if (playerRB.velocity.y <= 0) {
            if (player.transform.position.y - 0.25f * playerCol.size.y > transform.position.y + 0.25f * myBCol.size.y) {
                myBCol.enabled = true;
            }
        }
        else {
            myBCol.enabled = false;
        }
	}
}

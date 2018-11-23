using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour {
    GameObject player;
    CapsuleCollider2D playerCol;
    BoxCollider2D myBCol;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCol = player.GetComponent<CapsuleCollider2D>();
        myBCol = GetComponent<BoxCollider2D>();
	}
	
	void FixedUpdate () {
        //transform.position.y - 0.5 * playerCol.size.y
		if (player.transform.position.y - 0.25f * playerCol.size.y > transform.position.y + 0.25f * myBCol.size.y) {
            myBCol.enabled = true;
        }
        else {
            myBCol.enabled = false;
        }
	}
}

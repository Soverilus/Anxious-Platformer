using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayWithPlayer : MonoBehaviour {
    StartEndMoveAndReplace mySO;
    GameObject player;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        mySO = GetComponent<StartEndMoveAndReplace>();
	}

	void Update () {
        mySO.oldPos = transform.position.x;
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        mySO.newPos = transform.position.x;
        mySO.CalculateMoveDiff();
	}
}

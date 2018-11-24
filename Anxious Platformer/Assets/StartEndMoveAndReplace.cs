using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndMoveAndReplace : MonoBehaviour {
    public GameObject[] myTiles;
    Vector3 myStart;
    Vector3 myEnd;
    public GameObject myStartObj;
    public GameObject myEndObj;
    public Vector3 myNormalizedVector;
    [HideInInspector]
    public float myMoveDiff;
    [HideInInspector]
    public float oldPos;
    [HideInInspector]
    public float newPos;

    private void Start() {
        
    }

    public void CalculateMoveDiff() {
        myMoveDiff = newPos - oldPos;
    }

    private void FixedUpdate() {
        myStart = myStartObj.transform.localPosition;
        myEnd = myEndObj.transform.localPosition;
        myNormalizedVector = Vector3.Normalize(myStart - myEnd);
        for (int i = 0; i < myTiles.Length; i++) {
            Vector3 myPos = myTiles[i].transform.localPosition;
                             //can't use myMoveDiff unless I make a switch statement for which way the water is going. 
                             //if it's moving towards start or end using prediction via the following vector. 
                             //if it's direction is in the same as the start marker, I need to flip the whole script somehow.
            Vector3 moveVector = new Vector3(myPos.x - Time.fixedDeltaTime * (myNormalizedVector.x / myNormalizedVector.x) /*- myMoveDiff*/, myPos.y, myPos.z);
            //if my tile's x is between the start and finish
            if ((myPos.x > myEnd.x && myPos.x <= myStart.x)||
                (myPos.x < myEnd.x && myPos.x >= myStart.x)) {
                myTiles[i].transform.localPosition = moveVector;
                
            }
            //else if it's beyond or equal to the end marker
            else {
                float dist = Vector3.Distance(myPos, myEnd);
                myTiles[i].transform.localPosition = new Vector3(myStart.x - dist, myPos.y, myPos.z);
            }
        }
    }
}

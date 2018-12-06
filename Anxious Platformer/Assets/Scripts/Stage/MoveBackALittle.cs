using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackALittle : MonoBehaviour {
    public GameObject myBG2;
    public GameObject myBG1;
    public float bG2ChangeRate;
    public float bG1ChangeRate;

    Vector3 oldPos;
    Vector3 newPos;

    private void FixedUpdate() {
        newPos = transform.position;
        if (newPos != oldPos) {
                myBG2.transform.position = new Vector3(myBG2.transform.position.x - (((newPos.x - oldPos.x) * bG2ChangeRate) * Time.deltaTime), myBG2.transform.position.y, myBG2.transform.position.z);
                myBG1.transform.position = new Vector3(myBG1.transform.position.x - (((newPos.x - oldPos.x) * bG1ChangeRate) * Time.deltaTime), myBG1.transform.position.y - ((newPos.y - oldPos.y) * bG1ChangeRate) * Time.deltaTime, myBG1.transform.position.z);
        }
        oldPos = newPos;
    }
}

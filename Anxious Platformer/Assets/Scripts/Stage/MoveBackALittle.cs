using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackALittle : MonoBehaviour {
    public GameObject myBG2;
    public GameObject myBG1;


    Vector3 oldPos;
    Vector3 newPos;

    private void Update() {
        newPos = transform.position;
        if (newPos != oldPos) {
                myBG2.transform.position = new Vector3(myBG2.transform.position.x - (newPos.x - oldPos.x) * Time.deltaTime * 10f, myBG2.transform.position.y, myBG2.transform.position.z);
                myBG1.transform.position = new Vector3(myBG1.transform.position.x - (newPos.x - oldPos.x) * Time.deltaTime * 50f, myBG1.transform.position.y - (newPos.y - oldPos.y) * Time.deltaTime * 50f, myBG1.transform.position.z);
        }
        oldPos = newPos;
    }
}

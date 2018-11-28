using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMyStatHandler : MonoBehaviour {

    public GameObject myStatHandler;

    private void Start() {
        if (GameObject.FindGameObjectWithTag("StatHandler")) {
            StatHandler myObj = GameObject.FindGameObjectWithTag("StatHandler").GetComponent<StatHandler>();
            myObj.dayNumber += 1;
            Destroy(gameObject);
        }
        else {
            Instantiate(myStatHandler, Vector3.zero, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {

    GameObject target;
    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
    }
}

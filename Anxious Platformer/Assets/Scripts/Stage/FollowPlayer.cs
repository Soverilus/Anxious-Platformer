using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public float minHeight;

    Camera thisCam;
    public GameObject myTargetObj;
    Vector3 myTarget;
    public Transform myXPosEnd;
    public Transform myXPosStart;

    private void Start() {
        thisCam = GetComponent<Camera>();
    }

    private void LateUpdate() {
        myTarget = myTargetObj.transform.position;
        transform.position = Vector3.Lerp(transform.position, UseMyZAxis(myTarget), 0.25f);
    }

    private Vector3 UseMyZAxis(Vector3 target) {
        Vector3 myNewVector = new Vector3(Mathf.Clamp(target.x, myXPosStart.position.x, myXPosEnd.position.x), Mathf.Clamp(target.y, minHeight, 100f), transform.position.z);
        return myNewVector;
    }
}
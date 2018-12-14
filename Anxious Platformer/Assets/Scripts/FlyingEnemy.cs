using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {

    GameObject target;
    Vector3 targetPos;
    public float speed = 1;
    bool activated = false;
    bool startMove = false;
    float timer = 0f;
    public float timerMax;
    Animator myAnim;
    float myAnimSpeed;
    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
        myAnim = GetComponent<Animator>();
        myAnimSpeed = myAnim.speed;
    }
    private void Update() {
        if (gameObject.GetComponent<SpriteRenderer>().isVisible) {
            activated = true;
        }
        if (startMove) {
            if (timer < timerMax) {
                timer += Time.deltaTime;
                transform.Translate(Vector3.Normalize(targetPos - transform.position) * speed * Time.deltaTime);
                myAnim.speed = 0f;
            }
            else {
                timer = 0f;
                startMove = false;
                myAnim.speed = myAnimSpeed;
            }
        }
    }
    public void OnWingFlap() {
        if (activated) {
            startMove = true;
            targetPos = target.transform.position;
        }
    }
}

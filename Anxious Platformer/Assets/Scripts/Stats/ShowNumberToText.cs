using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowNumberToText : MonoBehaviour {
    public MovementStats myMS;
    float myMax;
    float timeLeft;
    Text myText;
    public bool useTime = true;

    private void Start() {
        myText = GetComponent<Text>();
        myMax = myMS.maxTime;
        timeLeft = myMS.maxTime;
    }

    private void Update() {
        if (useTime) {
            timeLeft = myMS.timeLeft;
            myText.text = timeLeft.ToString("F0");
            myText.color = new Color(((timeLeft / myMax) * -1f + 1f), 0f, 0f);
            if (timeLeft <= 0) {
                Destroy(gameObject);
            }
        }
        else {
            myMS.useTime = false;
            Destroy(gameObject);
        }
    }
}

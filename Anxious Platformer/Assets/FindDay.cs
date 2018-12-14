using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindDay : MonoBehaviour {
    Text thisText;
    StatHandler mySH;
    int day;
    public string[] whichDayIsIt;

    private void Start() {
        thisText = GetComponent<Text>();
        mySH = GameObject.FindGameObjectWithTag("StatHandler").GetComponent<StatHandler>();
        day = mySH.dayNumber;

        thisText.text = whichDayIsIt[day];
    }
}

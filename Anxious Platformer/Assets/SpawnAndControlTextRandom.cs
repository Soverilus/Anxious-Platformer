using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnAndControlTextRandom : MonoBehaviour {
    List<Text> myTexts = new List<Text>();
    List<bool> myFades = new List<bool>();
    public string[] myDayTexts;
    public string[] myThoughts;
    public Text myPrefab;
    public float intensity = 1f;
    float deltaTimeIntense;
    float timer = 0f;
    StatHandler mySH;
    MovementStats myMS;
    Text myText;
    public Canvas myCanvas;
    int howManyTimers;

    float timeLeft;
    float maxTime;
    float timeMultiplier;

    private void Start() {
        myMS = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementStats>();
        mySH = GameObject.FindGameObjectWithTag("StatHandler").GetComponent<StatHandler>();
        maxTime = myMS.maxTime;
        timeLeft = 0;
    }

    private void Update() {
        Mathf.Clamp(timeLeft += Time.deltaTime, 0f, maxTime);
        timeMultiplier = timeLeft / maxTime;
        deltaTimeIntense = Time.deltaTime * intensity * timeMultiplier;
        timer += deltaTimeIntense;

        if (timer >= 3) {
            howManyTimers = Mathf.RoundToInt(timer / 3f);
            for (int k = howManyTimers; k > 0; k--) {
                myText = Instantiate(myPrefab, gameObject.transform);
                myText.rectTransform.localPosition = new Vector3(Random.Range(-myCanvas.pixelRect.size.x / 2f, myCanvas.pixelRect.size.x / 2f), Random.Range(-myCanvas.pixelRect.size.y / 2f, myCanvas.pixelRect.size.y / 2f), 0f);
                myFades.Add(false);
                myTexts.Add(myText);
                myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 0f);
                myText.text = myThoughts[Random.Range(0, myThoughts.Length)];
                int r = Random.Range(0, 10);
                if (r == 0) {
                    myText.text = myDayTexts[mySH.dayNumber];
                }
                intensity += 0.2f;
                timer = 0f;
            }
        }

        //Seperate script for fading in and out
        for (int i = 0; i < myTexts.Count; i++) {
            FadeInOutFunct(myTexts[i], i);
        }
    }

    void FadeInOutFunct(Text text, int num) {
        if (!myFades[num]) {
            FadeTextToFullAlpha(2f, text, num);
        }
        else {
            FadeTextToZeroAlpha(2f, text, num);
        }
    }

    void FadeTextToFullAlpha(float t, Text i, int num) {
        if (i.color.a < 1.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
        }
        else myFades[num] = true; ;
    }

    void FadeTextToZeroAlpha(float t, Text i, int num) {
        if (i.color.a > 0.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
        }
        else {
            myFades.Remove(myFades[num]);
            GameObject tempObj = myTexts[num].gameObject;
            myTexts.Remove(myTexts[num]);
            Destroy(tempObj);

        }
    }
}

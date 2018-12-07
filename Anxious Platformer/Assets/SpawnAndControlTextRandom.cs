using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnAndControlTextRandom : MonoBehaviour {
    public bool inGame;
    List<Text> myTexts = new List<Text>();
    List<bool> myFades = new List<bool>();
    public string[] myDayTexts;
    public string[] myThoughts;
    public Text myPrefab;
    public float intensity = 1f;
    float originalIntensity;
    float deltaTimeIntense;
    float timer = 0f;
    StatHandler mySH;
    MovementStats myMS;
    Text myText;
    public Canvas myCanvas;
    int howManyTimers;

    public Transform lowerSpawnArea;

    float timeLeft;
    float maxTime;
    float timeMultiplier;

    private void Start() {
        originalIntensity = intensity;
        if (inGame) {
            myMS = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementStats>();
            mySH = GameObject.FindGameObjectWithTag("StatHandler").GetComponent<StatHandler>();
            maxTime = myMS.maxTime;
        }
        else {
            maxTime = 10f;
        }
        timeLeft = 0;
    }

    private void Update() {
        if (inGame) {
            if (maxTime != myMS.maxTime) {
                maxTime = myMS.maxTime;
            }
        }
        Mathf.Clamp(timeLeft += Time.deltaTime, 0f, maxTime);
        timeMultiplier = timeLeft / maxTime;
        deltaTimeIntense = Time.deltaTime * intensity * timeMultiplier *2f;
        timer += deltaTimeIntense;
        if (timeLeft >= maxTime) {
            Debug.Log("this timer");
        }

        if (timer >= 3) {
            howManyTimers = Mathf.RoundToInt(timer / 3f);
            for (int k = howManyTimers; k > 0; k--) {
                myText = Instantiate(myPrefab, gameObject.transform);
                if (inGame) {
                    myText.rectTransform.localPosition = new Vector3(Random.Range(-myCanvas.pixelRect.size.x / 2.5f, myCanvas.pixelRect.size.x / 2.5f), Random.Range(-myCanvas.pixelRect.size.y / 2.5f, myCanvas.pixelRect.size.y / 2.5f), 0f);
                }
                else {
                    myText.rectTransform.localPosition = new Vector3(Random.Range(-myCanvas.pixelRect.size.x / 2.5f, myCanvas.pixelRect.size.x / 2.5f), Random.Range(lowerSpawnArea.localPosition.y, myCanvas.pixelRect.size.y / 2.5f), 0f);
                }
                myFades.Add(false);
                myTexts.Add(myText);
                myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 0f);
                myText.text = myThoughts[Random.Range(0, myThoughts.Length)];
                if (inGame) {
                    int r = Random.Range(0, 10);
                    if (r == 0) {
                        myText.text = myDayTexts[Mathf.Clamp(mySH.dayNumber, 0, myDayTexts.Length - 1)];
                    }
                }
                if (intensity < 3f * originalIntensity) {
                    intensity += 0.2f;
                }
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

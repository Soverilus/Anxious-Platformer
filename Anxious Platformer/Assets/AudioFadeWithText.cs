using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioFadeWithText : MonoBehaviour {
    Text myText;
    AudioSource myAS;
    float originalVolume;
    float currentValue;

    private void Start() {
        myText = GetComponent<Text>();
        myAS = GetComponent<AudioSource>();
        originalVolume = myAS.volume;
    }
    private void Update() {
        currentValue = myText.color.a;
        myAS.volume = originalVolume * (currentValue - 0.05f);
    }

}

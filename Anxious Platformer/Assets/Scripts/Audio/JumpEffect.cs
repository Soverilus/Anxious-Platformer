using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class JumpEffect : MonoBehaviour {
    AudioSource myAS;
    bool startClip = false;
    Vector3 jumpStart;
    float myPitchIncrease;
    float oldPitch;
    float newPitch;
    float myOGVol;
    bool pitchShift = false;

    private void Start() {
        myAS = GetComponent<AudioSource>();
        myAS.UnPause();
        myAS.Pause();
        myOGVol = myAS.volume;
    }

    private void Update() {
        if (startClip) {
            newPitch = myAS.pitch;
            myAS.UnPause();
            if (newPitch >= oldPitch && pitchShift) {
                myAS.pitch = myPitchIncrease + (transform.position.y - jumpStart.y) / 25f;
            }
            else {
                pitchShift = false;
                myAS.volume -= 2 * Time.deltaTime;
            }
            oldPitch = newPitch;
        }
    }

    public void ActivateJump(float level) {
        pitchShift = true;
        myAS.volume = myOGVol;
        startClip = true;
        myPitchIncrease = level + 1;
        jumpStart = transform.position;
    }
    public void StopJump() {
        myAS.Pause();
        myAS.time = 0f;
        startClip = false;
    }
}

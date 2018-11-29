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
                myAS.pitch = myPitchIncrease + (transform.position.y - jumpStart.y) / 10f;
            }
            else {
                pitchShift = false;
                myAS.volume -= 3 * Time.deltaTime;
                if (myAS.volume == 0f) {
                    startClip = false;
                    oldPitch = 0f;
                }
            }
            oldPitch = newPitch;
        }
        else {
            myAS.pitch = 1f;
            oldPitch = 0f;
        }
    }

    public void ActivateJump(float level) {
        pitchShift = true;
        myAS.volume = myOGVol;
        startClip = true;
        myPitchIncrease = level/5 + 1;
        jumpStart = transform.position;
    }
    public void StopJump() {
        startClip = false;
        pitchShift = false;
        myAS.Pause();
        myAS.time = 1f;
        myAS.volume = 0;
        myAS.pitch = 1f;
        
    }
}

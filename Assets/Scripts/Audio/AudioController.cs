using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public GameObject AudioEffects;
    public AudioSource audioEffect;

    public AudioClip StartMenu;
    public AudioClip BackgroundStory;
    public AudioClip Level1;
    public AudioClip Level2;

    public AudioClip levelComplete;
    public AudioClip attackBune;
    public AudioClip attackCrowbar;
    public AudioClip attackFist;
    public AudioClip damagedBune;
    public AudioClip damagedPlayer;
    public AudioClip deathBune;
    public AudioClip deathPlayer;
    public AudioClip flashlightToggle;
    public AudioClip flashlightRecharge;
    public AudioClip pickupObject;
    public AudioClip explosionAlarm;


    void Start () {
        audioEffect = AudioEffects.GetComponent<AudioSource>();
    }

	void Update () {
        
    }

}

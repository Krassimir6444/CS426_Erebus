/*Explosion trigger, if collider is Player, trigger explosion of the engine room*/
// Not finished yet

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour {
    
    public List<GameObject> ExplosionObjects;
    public AudioController audioControllerScript;


    float startTime = 0.0f;
    float duration = 0.0f;
    bool playAudio = false;

    void Start ()
    {
        startTime = Time.time;
    }
    
    void Update ()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Start explosion count down and explode.");
            //need to add display to notify player an explosion count down is activated
            //trigger the explosion
            for(int i = 0; i < ExplosionObjects.Count; i++)
            {
                Debug.Log(ExplosionObjects[i]);
                ExplosionObjects[i].SetActive(true);
                //playAudio = true;
            }


            //need to make this explosion alarm loop for a few times 
            /*duration = Time.time - startTime;
            if (duration < 10)
            {
                audioControllerScript.audioEffect.clip = audioControllerScript.explosionAlarm;
                audioControllerScript.audioEffect.Play();
            }*/
            //end of explosion alarm thingie
        }
    }
}

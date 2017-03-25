/*Explosion trigger, if collider is Player, trigger explosion of the engine room*/
// Not finished yet

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour {

    public GameObject Player; //used because an istrigger of player is bigger than the actual player model
    
    void Start ()
    {
		
    }
    
    void Update ()
    {
		
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other == Player)
        {
            Debug.Log("Start explosion count down and explode.");
            //trigger the explosion
        }
    }
}

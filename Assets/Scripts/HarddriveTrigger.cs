/* Harddrive resource available trigger, if collider is Player, 
 * trigger display to player that they can pick up a harddrive 
 * with a very strong earth magnetic*/

// Not finished yet

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarddriveTrigger : MonoBehaviour {

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
            Debug.Log("Prompt harddrive pick up prompt");
            //display the pickup harddrive pick up prompt
        }

    }
}

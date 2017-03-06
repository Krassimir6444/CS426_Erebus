using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    PlayerInventory playerInventory;
    public UnityEngine.GameObject crowbar;

    public bool enemyInRange = false;
    Collider nearbyEnemy;

    void Start () {
        playerInventory = GetComponent<PlayerInventory>();
    }
	
	void Update () {
        
        // crowbar attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && (playerInventory.equippedCrowbar == true))
        {
            crowbar.transform.position += Vector3.up * 0.1F;
            crowbar.transform.Rotate(new Vector3(0,0,40));
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && (playerInventory.equippedCrowbar == true))
        {
            crowbar.transform.Rotate(new Vector3(0,0,-40));
            crowbar.transform.position -= Vector3.up * 0.1F;
            //audio clip

            // kinda poor way to do this, change if alternative found
            if (enemyInRange == true)
            {
                if (nearbyEnemy.gameObject.CompareTag("Enemy_Bune"))
                {
                    //damage enemy 
                }
            }
        }

    }


    void OnTriggerEnter(Collider other)
    {
        enemyInRange = true;
        nearbyEnemy = other;
    }
    void OnTriggerExit(Collider other)
    {
        enemyInRange = false;
        nearbyEnemy = null;
    }
}

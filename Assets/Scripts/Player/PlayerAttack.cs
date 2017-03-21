using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    PlayerInventory playerInventory;
    EnemyHealth enemyHealth;
    public GameObject player;
    public GameObject crowbar;

    public bool enemyInRange = false;
    public GameObject nearbyEnemy;

    void Start () {
        playerInventory = GetComponent<PlayerInventory>();
    }
	
	void Update () {
        
        // crowbar attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && (playerInventory.equippedCrowbar == true))
        {
            crowbar.transform.position += Vector3.up * 0.1F;
            crowbar.transform.Rotate(new Vector3(0,0,40));

            if (enemyInRange == true)
            {
                enemyHealth = nearbyEnemy.GetComponent<EnemyHealth>();
                enemyHealth.ReceiveDamage(20);

                var force = transform.position - nearbyEnemy.transform.position;
                force.Normalize();
                nearbyEnemy.GetComponent<Rigidbody>().AddForce(force * 5000);
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && (playerInventory.equippedCrowbar == true))
        {
            crowbar.transform.Rotate(new Vector3(0,0,-40));
            crowbar.transform.position -= Vector3.up * 0.1F;

            enemyHealth = null;
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = true;
            nearbyEnemy = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = false;
            nearbyEnemy = null;
        }
    }
}

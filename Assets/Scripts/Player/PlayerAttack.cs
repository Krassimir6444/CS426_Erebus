using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public GameObject AudioController;
    AudioController audioControllerScript;

    PlayerInventory playerInventory;
    EnemyHealth enemyHealth;
    public GameObject player;
    public GameObject crowbar;

    public bool enemyInRange = false;
    public GameObject nearbyEnemy;

    void Start () {
        audioControllerScript = AudioController.GetComponent<AudioController>();
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
            crowbar.transform.Rotate(new Vector3(0, 0, -40));
            crowbar.transform.position -= Vector3.up * 0.1F;

            if (enemyInRange == true)
            {
                audioControllerScript.audioEffect.clip = audioControllerScript.attackCrowbar;
                audioControllerScript.audioEffect.Play();

                enemyHealth = nearbyEnemy.GetComponentInParent<EnemyHealth>();
                enemyHealth.ReceiveDamage(20);
                enemyHealth = null;

                var force = transform.position - nearbyEnemy.transform.position;
                force.Normalize();
                nearbyEnemy.GetComponentInParent<Rigidbody>().AddForce(force * 5000);
            }  
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy_Hitbox"))
        {
            enemyInRange = true;
            nearbyEnemy = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy_Hitbox"))
        {
            enemyInRange = false;
            nearbyEnemy = null;
        }
    }
}

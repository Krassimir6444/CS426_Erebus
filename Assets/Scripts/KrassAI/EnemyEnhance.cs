using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnhance : MonoBehaviour
{

    public GameObject EnemyParentObject;

    EnemyAttack enemyAttack;
    EnemyDigitalScent enemyDigitalScent;
    EnemyHealth enemyHealth;
    PlayerInventory playerInventory;

    public List<Collider> nearbyObjectColliderList = new List<Collider>();
    
    private bool inLight = false;
    private bool toggle = false;
    private bool overRide = false;

    void Start()
    {        
        enemyAttack = EnemyParentObject.GetComponentInChildren<EnemyAttack>();
        enemyDigitalScent = EnemyParentObject.GetComponent<EnemyDigitalScent>();
        enemyHealth = EnemyParentObject.GetComponent<EnemyHealth>();
    }
    
    void Update()
    {
        if (nearbyObjectColliderList.Count > 0)
        {
            int index = -1;
            toggle = false;
            for (int i = 0; i < nearbyObjectColliderList.Count; i++)
            {
                if (nearbyObjectColliderList[i].CompareTag("Light_Exposure"))
                {
                    index = i;
                    toggle = true;
                    if (inLight == false)
                    {
                        inLight = true;
                        EnemyParentObject.GetComponent<SphereCollider>().radius = 10;       
                        enemyDigitalScent.stepSpeed = 03.0f;
                        enemyDigitalScent.turnSpeed = 30.0f;
                        enemyAttack.EnemyAttackLowerBound = 10;
                        enemyAttack.EnemyAttackUpperBound = 50;
                        enemyHealth.invulnerable = true;
                    }
                }
            }

            // having the enemy detect when they are no longer exposed to light has proved to be difficult, i.e. buggy
            if( (toggle == false && inLight == true) || (playerInventory.activateFlashlight == false) || overRide)
            {
                if (index >= 0) { nearbyObjectColliderList.RemoveAt(index); }
                inLight = false;
                overRide = false;

                EnemyParentObject.GetComponent<SphereCollider>().radius = 5;
                enemyDigitalScent.stepSpeed = 01.5f;
                enemyDigitalScent.turnSpeed = 15.0f;
                enemyAttack.EnemyAttackLowerBound = 05;
                enemyAttack.EnemyAttackUpperBound = 30;
                enemyHealth.invulnerable = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!nearbyObjectColliderList.Contains(other))
            { nearbyObjectColliderList.Add(other); }

        if (other.gameObject.CompareTag("Player"))
            { playerInventory = other.gameObject.GetComponent<PlayerInventory>(); }
    }
    void OnTriggerExit(Collider other)
    {
        if (nearbyObjectColliderList.Contains(other))
            { nearbyObjectColliderList.Remove(other); }

        // if (other.gameObject.CompareTag("Player")) { overRide = true; }
    }
}
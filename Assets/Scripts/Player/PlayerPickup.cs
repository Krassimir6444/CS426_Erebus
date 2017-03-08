using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour {

    public UnityEngine.UI.Text pickupPrompt;
    PlayerInventory playerInventory;

    bool objectInRange = false;
    Collider nearbyObject;


    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if(objectInRange == true && nearbyObject != null)
        {
            if (nearbyObject.gameObject.CompareTag("Equipment_Flashlight"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //audio clip
                    playerInventory.hasFlashlight = true;
                    playerInventory.equippedFlashlight = true;
                    playerInventory.flashlight.SetActive(true);
                    Destroy(nearbyObject.gameObject);
                    pickupPrompt.gameObject.SetActive(false);
                }
            }

            if (nearbyObject.gameObject.CompareTag("Consumable_Battery"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //audio clip
                    playerInventory.numBatteries++;
                    playerInventory.batteryCount.text = playerInventory.numBatteries + "";
                    Destroy(nearbyObject.gameObject);
                    pickupPrompt.gameObject.SetActive(false);
                }
            }
           
            if (nearbyObject.gameObject.CompareTag("Equipment_Crowbar"))
            { 
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //audio clip
                    playerInventory.hasCrowbar = true;
                    playerInventory.unequipPreviousWeapon();
                    playerInventory.equippedCrowbar = true;
                    playerInventory.crowbar.SetActive(true);
                    playerInventory.equipedWeapon.sprite = playerInventory.crowbarSprite;
                    Destroy(nearbyObject.gameObject);
                    pickupPrompt.gameObject.SetActive(false);
                }
            }

            /*
            if (nearbyObject.gameObject.CompareTag("Equipment_LaserPistol")) 
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //audio clip
                    playerInventory.hasLaserPistol = true;
                    Destroy(nearbyObject.gameObject);
                    pickupPrompt.gameObject.SetActive(false);
                }
            }

            if (nearbyObject.gameObject.CompareTag("Consumable_Bandage"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //audio clip
                    playerInventory.numBandages++;
                    Destroy(nearbyObject.gameObject);
                    pickupPrompt.gameObject.SetActive(false);
                }
            }
            */
        }
    }

    void OnTriggerEnter(Collider other)
    {
        pickupPrompt.gameObject.SetActive(true);
        objectInRange = true;
        nearbyObject = other;
    }
    void OnTriggerExit(Collider other)
    {
        pickupPrompt.gameObject.SetActive(false);
        objectInRange = false;
        nearbyObject = null;
    }

}

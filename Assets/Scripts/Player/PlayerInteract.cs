
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour {

    public UnityEngine.UI.Text interactPrompt;
    public UnityEngine.UI.Text keycardPrompt;
    public UnityEngine.UI.Text crowbarPrompt;
    PlayerInventory playerInventory;
    //DoorController doorController;

    public bool objectInRange = false;
    public Collider nearbyObject;


    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        //doorController = GetComponent<DoorController>();
    }

    void Update()
    {
        if (objectInRange == true && nearbyObject != null)
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
                    interactPrompt.gameObject.SetActive(false);
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
                    interactPrompt.gameObject.SetActive(false);
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
                    interactPrompt.gameObject.SetActive(false);
                }
            }

            if (nearbyObject.gameObject.CompareTag("Consumable_Keycard"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //audio clip
                    playerInventory.hasKeycard = true;
                    Destroy(nearbyObject.gameObject);
                    interactPrompt.gameObject.SetActive(false);
                }
            }

            if (nearbyObject.gameObject.CompareTag("Consumable_Medkit"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //audio clip
                    playerInventory.numMedkits++;
                    Destroy(nearbyObject.gameObject);
                    interactPrompt.gameObject.SetActive(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.E) && playerInventory.hasKeycard &&
                nearbyObject.gameObject.CompareTag("Door_Locked"))
            {
                nearbyObject.gameObject.transform.position += Vector3.up * 5.0F;
            }


            if (Input.GetKeyDown(KeyCode.Mouse0) && playerInventory.hasCrowbar &&
                nearbyObject.gameObject.CompareTag("Door_Jammed"))
            {
                playerInventory.unequipPreviousWeapon();
                playerInventory.hasCrowbar = false;
                nearbyObject.gameObject.transform.position += Vector3.up * 5.0F;
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
            */
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door_Locked"))
        {
            keycardPrompt.gameObject.SetActive(true);
        }
        else if (other.gameObject.CompareTag("Door_Jammed"))
        {
            crowbarPrompt.gameObject.SetActive(true);
        }
        else if (!(other.gameObject.CompareTag("Door") || other.gameObject.CompareTag("SF_Door")))
        {
            interactPrompt.gameObject.SetActive(true);
        }
        
        objectInRange = true;
        nearbyObject = other;
    }
    void OnTriggerExit(Collider other)
    {
        interactPrompt.gameObject.SetActive(false);
        keycardPrompt.gameObject.SetActive(false);
        crowbarPrompt.gameObject.SetActive(false);
        objectInRange = false;
        nearbyObject = null;     
    }

}

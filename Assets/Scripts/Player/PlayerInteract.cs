// List of nearby objects that entered player's collider http://answers.unity3d.com/questions/638319/getting-a-list-of-colliders-inside-a-trigger.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour {

    public GameObject AudioController;
    AudioController audioControllerScript;

    public UnityEngine.UI.Text interactPrompt;
    public UnityEngine.UI.Text keycardPrompt;
    public UnityEngine.UI.Text crowbarPrompt;
    PlayerInventory playerInventory;
    
    public bool objectInRange = false;
    //public GameObject nearbyObject;

    public List<Collider> nearbyObjectColliderList = new List<Collider>();

    void Start()
    {
        audioControllerScript = AudioController.GetComponent<AudioController>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        //if (objectInRange == true && nearbyObject != null)
        if(objectInRange && nearbyObjectColliderList.Count > 0)
        {
            for (int i = 0; i < nearbyObjectColliderList.Count; i++)
            {
                // small hack to get around the null in list bug
                if (nearbyObjectColliderList[i] == null)
                {
                    nearbyObjectColliderList = new List<Collider>();
                    return;
                }

                //if (nearbyObject != null && nearbyObject.CompareTag("Equipment_Flashlight"))
                if(nearbyObjectColliderList[i].CompareTag("Equipment_Flashlight"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        audioControllerScript.audioEffect.clip = audioControllerScript.pickupObject;
                        audioControllerScript.audioEffect.Play();

                        playerInventory.hasFlashlight = true;
                        playerInventory.equippedFlashlight = true;
                        playerInventory.flashlight.SetActive(true);
                        //Destroy(nearbyObject);
                        Destroy(nearbyObjectColliderList[i].gameObject);
                        //nearbyObject = null;
                        nearbyObjectColliderList.Remove(nearbyObjectColliderList[i]);
                        objectInRange = false;
                        interactPrompt.gameObject.SetActive(false);
                    }
                }

                //if (nearbyObject != null && nearbyObject.CompareTag("Consumable_Battery"))
                else if (nearbyObjectColliderList[i].CompareTag("Consumable_Battery"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        audioControllerScript.audioEffect.clip = audioControllerScript.pickupObject;
                        audioControllerScript.audioEffect.Play();

                        playerInventory.numBatteries++;
                        playerInventory.batteryCount.text = playerInventory.numBatteries + "";
                        //Destroy(nearbyObject);
                        Destroy(nearbyObjectColliderList[i].gameObject);
                        //nearbyObject = null;
                        nearbyObjectColliderList.Remove(nearbyObjectColliderList[i]);
                        objectInRange = false;
                        interactPrompt.gameObject.SetActive(false);
                    }
                }

                //if (nearbyObject != null && nearbyObject.CompareTag("Equipment_Crowbar"))
                else if (nearbyObjectColliderList[i].CompareTag("Equipment_Crowbar"))
                {
                    if (Input.GetKeyDown(KeyCode.E) && playerInventory.hasCrowbar == false)
                    {
                        audioControllerScript.audioEffect.clip = audioControllerScript.pickupObject;
                        audioControllerScript.audioEffect.Play();

                        playerInventory.hasCrowbar = true;
                        playerInventory.unequipPreviousWeapon();
                        playerInventory.equippedCrowbar = true;
                        playerInventory.crowbar.SetActive(true);
                        playerInventory.equipedWeapon.sprite = playerInventory.crowbarSprite;
                        //Destroy(nearbyObject);
                        Destroy(nearbyObjectColliderList[i].gameObject);
                        //nearbyObject = null;
                        nearbyObjectColliderList.Remove(nearbyObjectColliderList[i]);
                        objectInRange = false;
                        interactPrompt.gameObject.SetActive(false);
                    }
                }

                //if (nearbyObject != null && nearbyObject.CompareTag("Consumable_Keycard"))
                else if (nearbyObjectColliderList[i].CompareTag("Consumable_Keycard"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        audioControllerScript.audioEffect.clip = audioControllerScript.pickupObject;
                        audioControllerScript.audioEffect.Play();

                        playerInventory.hasKeycard = true;
                        //Destroy(nearbyObject);
                        Destroy(nearbyObjectColliderList[i].gameObject);
                        //nearbyObject = null;
                        nearbyObjectColliderList.Remove(nearbyObjectColliderList[i]);
                        objectInRange = false;
                        interactPrompt.gameObject.SetActive(false);
                    }
                }

                //if (nearbyObject != null && nearbyObject.CompareTag("Consumable_Medkit"))
                else if (nearbyObjectColliderList[i].CompareTag("Consumable_Medkit"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        audioControllerScript.audioEffect.clip = audioControllerScript.pickupObject;
                        audioControllerScript.audioEffect.Play();

                        playerInventory.numMedkits++;
                        //Destroy(nearbyObject);
                        Destroy(nearbyObjectColliderList[i].gameObject);
                        //nearbyObject = null;
                        nearbyObjectColliderList.Remove(nearbyObjectColliderList[i]);
                        objectInRange = false;
                        interactPrompt.gameObject.SetActive(false);
                    }
                }

                //if (nearbyObject != null && Input.GetKeyDown(KeyCode.E) && playerInventory.hasKeycard &&
                //    nearbyObject.CompareTag("Door_Locked"))
                if (Input.GetKeyDown(KeyCode.E) && playerInventory.hasKeycard &&
                    nearbyObjectColliderList[i].CompareTag("Door_Locked"))
                {
                    audioControllerScript.audioEffect.clip = audioControllerScript.flashlightRecharge;
                    audioControllerScript.audioEffect.Play();

                    nearbyObjectColliderList[i].transform.position += Vector3.up * 5.0F;
                    playerInventory.hasKeycard = false;
                }


                //if (nearbyObject != null && Input.GetKeyDown(KeyCode.Mouse0) && playerInventory.hasCrowbar &&
                //    nearbyObject.CompareTag("Door_Jammed"))
                if (Input.GetKeyDown(KeyCode.Mouse0) && playerInventory.hasCrowbar &&
                   nearbyObjectColliderList[i].CompareTag("Door_Jammed"))
                {
                    audioControllerScript.audioEffect.clip = audioControllerScript.attackCrowbar;
                    audioControllerScript.audioEffect.Play();

                    playerInventory.unequipPreviousWeapon();
                    playerInventory.hasCrowbar = false;
                    nearbyObjectColliderList[i].transform.position += Vector3.up * 5.0F;
                }

                /*
                if (nearbyObject.CompareTag("Equipment_LaserPistol")) 
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //audio clip
                        playerInventory.hasLaserPistol = true;
                        Destroy(nearbyObject);
                        pickupPrompt.gameObject.SetActive(false);
                    }
                }
                */
            }
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
        else if (!(other.gameObject.CompareTag("Untagged") ||
                   other.gameObject.CompareTag("Door") || 
                   other.gameObject.CompareTag("SF_Door") ||
                   other.gameObject.CompareTag("Enemy") ||
                   other.gameObject.CompareTag("Enemy_AttackRange") ||
                   other.gameObject.CompareTag("Enemy_Hitbox") ||
                   other.gameObject.CompareTag("Player") ||
                   other.gameObject.CompareTag("Player_Node") ||
                   other.gameObject.CompareTag("Light_Exposure") ) )
        {
            interactPrompt.gameObject.SetActive(true);
        }

        if (!(other.gameObject.CompareTag("Player_Node") ||
              other.gameObject.CompareTag("Player") ||
              other.gameObject.CompareTag("Light_Exposure") ) ) 
        {
            objectInRange = true;
            //nearbyObject = other.gameObject;
            /*if (!nearbyObjectList.Contains(other))
            {
                nearbyObjectList.Add(other);
                Debug.Log(other);
                Debug.Log(nearbyObjectList);
            }*/
            if (!nearbyObjectColliderList.Contains(other))
            {
                nearbyObjectColliderList.Add(other);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        interactPrompt.gameObject.SetActive(false);
        keycardPrompt.gameObject.SetActive(false);
        crowbarPrompt.gameObject.SetActive(false);
        objectInRange = false;
        //nearbyObject = null;
        /*if (!nearbyObjectList.Contains(other))
        {
            nearbyObjectList.Remove(other);
            Debug.Log(other);
            Debug.Log(nearbyObjectList);
        }*/
        if (nearbyObjectColliderList.Contains(other))
        {
            nearbyObjectColliderList.Remove(other);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public UnityEngine.GameObject HUD;
    public UnityEngine.UI.Text batteryCount;
    public UnityEngine.UI.Slider flashlightCharge;
    public UnityEngine.UI.Image equipedWeapon;
    PlayerHealth playerHealth;

    //equipment
    public Sprite fistSprite;
    public bool hasFist = true;
    public bool equippedFist = true;

    public UnityEngine.GameObject flashlight;
    public UnityEngine.Light pointLight;
    public bool hasFlashlight = false;
    public bool equippedFlashlight = false;
    public bool activateFlashlight = false;
    public bool depletedBattery = false;

    public UnityEngine.GameObject crowbar;
    public Sprite crowbarSprite;
    public bool hasCrowbar = false;
    public bool equippedCrowbar = false;

    public bool hasLaserPistol = false;
    public bool equippedLaserPistol = false;

    //consumables
    public int numBandages = 0;
    public int numBatteries = 0;
    
    //objects
    public bool hasKeycard = false;

    float startTime = 0;

	void Start ()
    {
        HUD.SetActive(true);
	}

    void Update()
    {
        // press 'F' to toggle flashlight activation
        if (Input.GetKeyDown(KeyCode.F) && (hasFlashlight == true))
        {
            // start hold down 'F' timer
            startTime = Time.time;

            if (equippedFlashlight && !depletedBattery)
            {
                if (activateFlashlight == false) {
                    activateFlashlight = true;
                    pointLight.gameObject.SetActive(true);
                }
                else {
                    activateFlashlight = false;
                    pointLight.gameObject.SetActive(false);
                }
            }
        }

        // release after holding (from more than half second) to toggle flashlight equip
        if (Input.GetKeyUp(KeyCode.F) && (hasFlashlight == true))
        {
            float timePressed = Time.time - startTime;
            if(timePressed > 0.5)
            {
                if (equippedFlashlight == false) {
                    equippedFlashlight = true;
                    flashlight.SetActive(true);
                }
                else {
                    equippedFlashlight = false;
                    flashlight.SetActive(false);
                    activateFlashlight = false;
                    pointLight.gameObject.SetActive(false);
                }
            }
        }

        // activated flashlight depletes battery
        if (hasFlashlight && equippedFlashlight && activateFlashlight)
        {
            if (flashlightCharge.value > 0)
                { flashlightCharge.value -= 0.05f; }
            else {
                depletedBattery = true;
                activateFlashlight = false;
                pointLight.gameObject.SetActive(false);
            }     
        }

        // recharge flashlight
        if (Input.GetKeyDown(KeyCode.B) && (numBatteries > 0))
        {
            flashlightCharge.value = 100;
            depletedBattery = false;
            numBatteries--;
            batteryCount.text = numBatteries + "";
        }

        // toggle crowbar equip
        if (Input.GetKeyDown(KeyCode.R) && (hasCrowbar == true))
        {
            if (equippedCrowbar == false) {
                crowbar.SetActive(true);
                equippedCrowbar = true;
                equipedWeapon.sprite = crowbarSprite;
            }
            else {
                equipedWeapon.sprite = fistSprite;
                unequipPreviousWeapon();
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.Q) && (numBandages > 0))
        {
            playerHealth.restoreHealth(33);
            numBandages--;
        }
        */
    }

    public void unequipPreviousWeapon()
    {
        equippedFist = false;

        equippedCrowbar = false;
        crowbar.SetActive(false);

        equippedLaserPistol = false;
        // add .SetActive(false) for future laserPistol fbx
    }
}

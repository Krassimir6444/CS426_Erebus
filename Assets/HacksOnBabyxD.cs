/* === Hacks On Baby xD ===
 * Used during demo only.
 * Allow us to change player settings so we can speed through the level as 
 * well as give/get resources without actually going to specific locations 
 * where the resources reside in the map.
 * */

/* Note to Self:
 *     RigidbodyFirstPersonController:
 *         ForwardSpeed, BackwardSpeed, StrafeSpeed
 *         Run Multiplier
 *     PlayerHealth:
 *         CurrentHealth
 *     PlayerStamina:
 *         CurrentStamina
 *     PlayerInventory:
 *         Has
 *             Fist
 *             Flashlight
 *             Crowbar
 *             LaserPistol
 *             Keycard
 *         Equipped
 *             Fist
 *             Flashlight
 *             Crowbar
 *             LaserPistol
 *          NumMedkits
 *          NumBatteries
 *      Under PlayerPrefab:
 *          Player->PlayerView->Equipment:
 *              HandLight
 *              Crowbar
 *          HUD (low priority)
 * */
using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class HacksOnBabyxD : MonoBehaviour
{
    public class PlayerValues
    {
        [NonSerialized]
        public float forwardSpeed, backwardSpeed, strafeSpeed;
        public float runMultiplier;

        public int currentHealth;

        public float currentStamina;

        public bool hasFist, hasFlashlight, hasCrowbar, hasLaserPistol, hasKeycard;
        public bool equippedFist, equippedFlashlight, equippedCrowbar, equippedLaserPistol;
        public int numMedkits, numBatteries;

        public bool showHandLightModel, showCrowbarModel;
    }
    
    RigidbodyFirstPersonController RbFPCScript;
    PlayerHealth HealthScript;
    PlayerStamina StaminaScript;
    PlayerInventory InventoryScript;

    GameObject HandLightModel, CrowbarModel;

    PlayerValues defaultValues;

    void Start()
    {
        defaultValues = new PlayerValues();
        SaveDefaultValues(defaultValues);
    }

    void Update()
    {

    }

    private void SaveDefaultValues(PlayerValues pv)
    {
        pv.forwardSpeed = RbFPCScript.movementSettings.ForwardSpeed;
        pv.backwardSpeed = RbFPCScript.movementSettings.BackwardSpeed;
        pv.strafeSpeed = RbFPCScript.movementSettings.StrafeSpeed;
        pv.runMultiplier = RbFPCScript.movementSettings.RunMultiplier;
        pv.currentHealth = HealthScript.currentHealth;
        pv.currentStamina = StaminaScript.currentStamina;
        pv.hasFist = InventoryScript.hasFist;
        pv.hasFlashlight = InventoryScript.hasFlashlight;
        pv.hasCrowbar = InventoryScript.hasCrowbar;
        pv.hasLaserPistol = InventoryScript.hasLaserPistol;
        pv.hasKeycard = InventoryScript.hasKeycard;
        pv.equippedFist = InventoryScript.equippedFist;
        pv.equippedFlashlight = InventoryScript.equippedFlashlight;
        pv.equippedCrowbar = InventoryScript.equippedCrowbar;
        pv.equippedLaserPistol = InventoryScript.equippedLaserPistol;
        pv.numMedkits = InventoryScript.numMedkits;
        pv.numBatteries = InventoryScript.numBatteries;
        //add default values for showing flashlight and crowbar models
    }
}

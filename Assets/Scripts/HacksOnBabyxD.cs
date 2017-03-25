/* === Hacks On Baby xD ===
 * Used during demo only.
 * 
 * Right Ctrl + H to show the hacks menu.
 * 
 * Allow us to change player settings so we can speed through the level as 
 * well as give/get resources without actually going to specific locations 
 * where the resources reside in the map.
 * */

/* Note to Self:
 *     RigidbodyFirstPersonController:
 *         ForwardSpeed, BackwardSpeed, StrafeSpeed
 *         RunMultiplier
 *     PlayerHealth:
 *         CurrentHealth
 *     PlayerStamina:
 *         CurrentStamina
 *     PlayerInventory:
 *         Has
 *             Fist, Flashlight, Crowbar, LaserPistol, Keycard
 *         Equipped
 *             Fist, Flashlight, Crowbar, LaserPistol
 *          NumMedkits
 *          NumBatteries
 *      Under PlayerPrefab:
 *          Player->PlayerView->Equipment:
 *              HandLight
 *              Crowbar
 *          HUD (medium priority, show/display models if equipped/not equipped)
 * */
using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[Serializable]
public class HacksOnBabyxD : MonoBehaviour
{
    public class PlayerValues
    {
        public float forwardSpeed, backwardSpeed, strafeSpeed;
        public float runMultiplier;

        public int currentHealth;

        public float currentStamina;

        public bool hasFist, hasFlashlight, hasCrowbar, hasLaserPistol, hasKeycard;
        public bool equippedFist, equippedFlashlight, equippedCrowbar, equippedLaserPistol;
        public int numMedkits, numBatteries;

        //public bool showHandLightModel, showCrowbarModel;
    }

    //↓↓↓ Warning about UnityStandardAssets is because there are 2 "Standard Assets" folder. Fix that and this will be fixed
    public RigidbodyFirstPersonController RbFPCScript;
    public PlayerHealth HealthScript;
    public PlayerStamina StaminaScript;
    public PlayerInventory InventoryScript;

    //public GameObject HandLightModel, CrowbarModel;

    private PlayerValues defaultValues = new PlayerValues();
    private PlayerValues customValues = new PlayerValues();

    private Rect HacksOnRect = new Rect(50, 50, (Screen.width - 100), (Screen.height - 100));
    private bool ShowHacksDialog = false;

    void Start()
    {
        SaveDefaultValues(defaultValues);   //save to use if needed
        SaveDefaultValues(customValues);    //save to show default values at start
    }

    private void OnGUI()
    {
        if (ShowHacksDialog)
        {
            HacksOnRect = GUILayout.Window(0, HacksOnRect, HacksOnContent, "Hacks On Baby - You Noob.");
        }
    }

    void HacksOnContent(int WindowID)
    {
        GUILayout.Label("Todo");
    }

    void Update()
    {
        //right ctrl + h
        if( Input.GetKey(KeyCode.RightControl))
        {
            if( Input.GetKeyDown(KeyCode.H))
            {
                ShowHacksDialog = !ShowHacksDialog;
            }
        }
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

    //function: revert values back to default values

    //function: change values according to modified values (customValues)
}

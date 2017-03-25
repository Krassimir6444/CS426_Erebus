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

public class HacksOnBabyxD : MonoBehaviour {
    public class PlayerValues
    {

    }
	void Start () {
		
	}
	
	void Update () {
		
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	public GameObject Enemy;
	private int startingHealth = 100;                            // The amount of health the player starts the game with.
	private int currentHealth;                                   // The current health the player has.

	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.


	void Start ()
	{
		// Set the initial health of the player.
		currentHealth = startingHealth;

		isDead = false;
	}


	void Update ()
	{
		if (damaged) {
			ReceiveDamage ();
		} else {
			damaged = false;
		}
		
	}


	public void ReceiveDamage ()
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;

		// Reduce the current health by the damage amount.
		currentHealth -= 20;

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead)
		{
			// ... it should die.
			Death ();
		}
	}


	void Death ()
	{
		// Set the death flag so this function won't be called again.
		isDead = true;
		Destroy (Enemy);
	}       
}
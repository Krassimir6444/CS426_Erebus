using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public GameObject AudioController;
    AudioController audioControllerScript;

    public GameObject Enemy;
	private int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.

    public bool invulnerable = false;
    bool isDead = false;                                                // Whether the player is dead.
	bool damaged = false;                                               // True when the player gets damaged.

	void Start ()
	{
        audioControllerScript = AudioController.GetComponent<AudioController>();

        // Set the initial health of the player.
        currentHealth = startingHealth;
	}


	void Update ()
	{
        if(isDead && !invulnerable) { Death(); }
	}


	public void ReceiveDamage (int value)
	{
        audioControllerScript.audioEffect.clip = audioControllerScript.damagedBune;
        audioControllerScript.audioEffect.PlayDelayed(1);

        // Set the damaged flag so the screen will flash.
        damaged = true;

		// Reduce the current health by the damage amount.
		currentHealth -= value;

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead && !invulnerable)
		{
			// ... it should die.
			Death ();
		}
	}


	void Death ()
	{
        audioControllerScript.audioEffect.clip = audioControllerScript.deathBune;
        audioControllerScript.audioEffect.Play();

        // Set the death flag so this function won't be called again.
        isDead = true;
		Destroy (Enemy);
	}       
}

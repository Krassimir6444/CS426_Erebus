using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

// source tutorial: https://www.youtube.com/watch?v=a916_lhps04

public class PlayerHealth : MonoBehaviour {

    public GameObject GameController;
    GameController gameControllerScript;
    public GameObject AudioController;
    AudioController audioControllerScript;

    public GameObject Message;

    public int initialHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    bool damaged = false;
    public Image damageImage;
    public AudioClip damageClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    bool isDead = false;
    
    RigidbodyFirstPersonController rigidbodyFirestPersonController;
    PlayerStamina playerStamina;

    void Awake()
    {
        gameControllerScript = GameController.GetComponent<GameController>();
        audioControllerScript = AudioController.GetComponent<AudioController>();
        rigidbodyFirestPersonController = GetComponent<RigidbodyFirstPersonController>();
        playerStamina = GetComponent<PlayerStamina>();
    }

    void Start()
    {
        currentHealth = initialHealth;
        updateHealth();
    }

    void Update()
    {
        updateHealth();

        //if (damaged) { damageImage.color = flashColor; }
        //damaged = false;

        //if (currentHealth == 100) healthSlider.gameObject.SetActive(false);
        //else healthSlider.gameObject.SetActive(true);   
    }


    public void updateHealth()
    {
        healthSlider.value = currentHealth;
    }

    public void restoreHealth(int value)
    {
        currentHealth += value;
        updateHealth();
        if (currentHealth > 100) { currentHealth = 100; }
    }

    public void damageHealth(int value)
    {
        if (isDead != true)
        {
            audioControllerScript.audioEffect.clip = audioControllerScript.damagedPlayer;
            audioControllerScript.audioEffect.Play();

            currentHealth -= value;
            updateHealth();

            if (currentHealth <= 0 && !isDead) { Death(); }
        }
    }


    void Death()
    {
        audioControllerScript.audioEffect.clip = audioControllerScript.deathPlayer;
        audioControllerScript.audioEffect.Play();

        isDead = true;
        rigidbodyFirestPersonController.enabled = false;

        Message.GetComponent<Text>().color = Color.red;
        Message.GetComponent<Text>().text = "You Have Died";
        Message.SetActive(true);

        Invoke("ReloadGame", 5.0f);
    }

    void ReloadGame() { SceneManager.LoadScene(gameControllerScript.currentLevel); }


    public void damageHealth(string state)
    {
        switch(state)
        {
            case "Cancel":
                CancelInvoke();
                break;
       
            // just an example (from Chitin Predators)
            case "Starvation":
                InvokeRepeating("damageHealth_Starvation", 1, 1);
                break;
        }
    }

                    /* Player States */
    /* TODO: replace with states relevant to Erebus */

    // Player: Starving
    void damageHealth_Starvation()
    {
        currentHealth -= 1;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead) { Death(); }
    }

}


// could possibly be useful for collisions, with say light?
/*
private void OnCollisionStay(Collision collision)
{
    if (collision.collider.name == "Water")
    {
        damageImage.color = flashColor;
        damageHealth(1);
    }
}
*/



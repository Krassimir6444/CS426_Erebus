using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerStamina : MonoBehaviour {


    public int initialStamina = 100;
    public float currentStamina;
    public Slider staminaSlider;


    void Start()
    {
        currentStamina = initialStamina;
        InvokeRepeating("restoreStamina_Standard", 1, 1);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CancelInvoke("restoreStamina_Standard");
            depleteStamina_Sprint();
            InvokeRepeating("restoreStamina_Standard", 1, 1); 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelInvoke("restoreStamina_Standard");
            depleteStamina_Jump();
            InvokeRepeating("restoreStamina_Standard", 1, 1);
        }

        if( !Input.GetKey(KeyCode.LeftShift) && 
            !Input.GetKeyDown(KeyCode.Space) )
        {
            CancelInvoke("restoreStamina_Standard");
            restoreStamina_Idle();
            InvokeRepeating("restoreStamina_Standard", 1, 1);
        }

        if (
            !Input.GetKey(KeyCode.W) &&
            !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.D) && 
            !Input.GetKey(KeyCode.LeftShift) &&
            !Input.GetKeyDown(KeyCode.Space))
        {
            CancelInvoke("restoreStamina_Standard");
            restoreStamina_Idle();
            restoreStamina_Idle();
            InvokeRepeating("restoreStamina_Standard", 1, 1);
        }

        //if (currentStamina == 100) staminaSlider.gameObject.SetActive(false);
        //else staminaSlider.gameObject.SetActive(true);
    }


    public float getCurrentStamina()
    {
        return currentStamina;
    }


    public void restoreStamina(int value)
    {
        currentStamina += value;
        if (currentStamina >= 100) { currentStamina = 100; }
        staminaSlider.value = currentStamina;
    }

    public void depleteStamina(int value)
    {
        currentStamina -= value;
        if (currentStamina <= 0) { currentStamina = 0; }
        staminaSlider.value = currentStamina;
    }


                    /* Player States */
    /* the assigned values are just default placeholders */

    // Player: Standard
    public void restoreStamina_Standard()
    {
        currentStamina += 5.0f;
        if (currentStamina >= 100) { currentStamina = 100; }
        staminaSlider.value = currentStamina;
    }

    // Player: Idle
    public void restoreStamina_Idle()
    {
        currentStamina += 0.10f;
        if (currentStamina >= 100) { currentStamina = 100; }
        staminaSlider.value = currentStamina;
    }

    // Player: Sprinting
    public void depleteStamina_Sprint()
    {
        currentStamina -= 0.20f;
        if (currentStamina <= 0) { currentStamina = 0; }
        staminaSlider.value = currentStamina;
    }

    // Player: Attacking
    public void depleteStamina_Attack()
    {
        currentStamina -= 20;
        if (currentStamina <= 0) { currentStamina = 0; }
        staminaSlider.value = currentStamina;
    }

    // Player: Jumping
    public void depleteStamina_Jump()
    {
        currentStamina -= 33;
        if (currentStamina <= 0) { currentStamina = 0; }
        staminaSlider.value = currentStamina;
    }

}
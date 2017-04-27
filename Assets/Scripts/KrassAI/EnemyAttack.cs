using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public GameObject AudioController;
    AudioController audioControllerScript;

    PlayerHealth playerHealth;
    EnemyDigitalScent enemyDigitalScent;

    public GameObject EnemyParentObject;
    private Animation EnemyAnimation;
    public int EnemyAttackUpperBound = 33;
    public int EnemyAttackLowerBound = 5;
    System.Random rand = new System.Random();

    public GameObject playerHandle;
    float startAttack = 0.0f;
    float interval = 0.0f;

    void Start () {
        audioControllerScript = AudioController.GetComponent<AudioController>();
        enemyDigitalScent = EnemyParentObject.GetComponentInParent<EnemyDigitalScent>();
        EnemyAnimation = EnemyParentObject.GetComponentInChildren<Animation>();
    }
	
	void Update ()
    {
		if (enemyDigitalScent.EnemyState == EnemyDigitalScent.EnemyStates.Attack)
        {
            //EnemyAnimation.Play("creature1Attack2");
            //EnemyAnimation.CrossFade("creature1Attack2", 0.3f, PlayMode.StopAll);
            EnemyAnimation.CrossFadeQueued("creature1Attack2", 0.3f, QueueMode.CompleteOthers, PlayMode.StopAll);

            interval = Time.time - startAttack;
            if (interval >= 0.5f)
            {
                audioControllerScript.audioEffect.clip = audioControllerScript.attackBune;
                audioControllerScript.audioEffect.Play();

                playerHealth.damageHealth(rand.Next(EnemyAttackLowerBound, EnemyAttackUpperBound));
                interval = 0;
                startAttack = Time.time;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHandle = other.gameObject;
            playerHealth = playerHandle.GetComponent<PlayerHealth>();

            startAttack = Time.time;

            enemyDigitalScent.pickedUpScent = Time.time;
            enemyDigitalScent.EnemyState = EnemyDigitalScent.EnemyStates.Attack;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHandle = null;
            playerHealth = null;

            enemyDigitalScent.EnemyState = EnemyDigitalScent.EnemyStates.Chase;
        }
    }
}

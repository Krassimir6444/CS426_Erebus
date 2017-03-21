using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDigitalScent : MonoBehaviour {

    PlayerHealth playerHealth;
    public GameObject playerNode;
    public GameObject playerHandle;
    public bool playerNodeInRange;
    public bool playerInRange;

    public GameObject EnemyParentObject;
    public GameObject AIViewRange;
    private Transform EnemyTransform;
    private Animation EnemyAnimation;
    public enum EnemyStates { Idle, Chase, Attack };
    public EnemyStates EnemyState;

    public float StepSpeed = 1.7f;
    public float TurnSpeed = 15f;

    float startAttack = 0.0f;
    float interval = 0.0f;

    public float pickedUpScent = 0.0f;
    public float freshness = 0.0f;

    void Start () {
        EnemyTransform = EnemyParentObject.GetComponent<Transform>();
        EnemyAnimation = EnemyParentObject.GetComponentInChildren<Animation>();
    }
	
	void Update () {

        freshness = Time.time - pickedUpScent;
        if (freshness > 2.5f) { EnemyState = EnemyStates.Idle; }

        //Idle
        if (EnemyState == EnemyStates.Idle)
        {
            EnemyAnimation.Play("creature1Idle");
        }

        //Follow Scent (i.e Chase)
        else if (EnemyState == EnemyStates.Chase)
        {
            EnemyAnimation.Play("creature1walkforward");
            //EnemyAnimation.CrossFade("creature1walkforward", 0.3f, PlayMode.StopAll);
            //EnemyAnimation.CrossFadeQueued("creature1walkforward", 0.3f, QueueMode.CompleteOthers, PlayMode.StopAll);

            //Turn monster towards next player node
            Vector3 targetDir = playerNode.transform.position - EnemyTransform.position;
            float turnStep = TurnSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(EnemyTransform.forward, targetDir, turnStep, 0.0f);
            EnemyTransform.rotation = Quaternion.LookRotation(newDir);

            //Move towards next player node
            float step = StepSpeed * Time.deltaTime;
            EnemyTransform.position = Vector3.MoveTowards(EnemyTransform.position, playerNode.transform.position, step);

            //move view range along with AI
            Vector3 offset = new Vector3(0f, 1f, 0.25f);
            Vector3 creaturePosition = EnemyTransform.position;
            AIViewRange.GetComponent<Transform>().position = creaturePosition + offset;
        }

        //Attack
        else if (EnemyState == EnemyStates.Attack)
        {
            //EnemyAnimation.Play("creature1Attack2");
            //EnemyAnimation.CrossFade("creature1Attack2", 0.3f, PlayMode.StopAll);
            EnemyAnimation.CrossFadeQueued("creature1Attack2", 0.3f, QueueMode.CompleteOthers, PlayMode.StopAll);

            interval = Time.time - startAttack;
            if (interval >= 0.5f)
            {
                playerHealth.damageHealth(5);
                interval = 0;
                startAttack = Time.time;
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player_Node"))
        {
            playerNodeInRange = true;
            playerNode = other.gameObject;
            pickedUpScent = Time.time;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            playerHandle = other.gameObject;
            playerHealth = playerHandle.GetComponent<PlayerHealth>();
            EnemyState = EnemyStates.Attack;
            startAttack = Time.time;
            pickedUpScent = Time.time;
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            playerHandle = null;
            playerHealth = null;
            EnemyState = EnemyStates.Chase;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FSM_AIController : MonoBehaviour
{
    public enum PatrolTypes { Circular, Linear, IrregularLinear };
    public enum AI_States { Spawn, Idle, Patrol, Attack, Chase };


    public GameObject Player;

    public AI_States AI_State;
    public GameObject PatrolNodes;
    public PatrolTypes PatrolType = PatrolTypes.Linear;
    public float MoveSpeed = 3.0f;
    public float TurnSpeed = 15.0f;


    private Transform AI_Transform;
    private Animation AI_Animation;
    private Transform[] PatrolNodeArrayTransforms;  //NTS: [0] is the PatrolNodes parent so skip that; 1-n is what im looking for
    private int patrolNodeArrayCounter = 1;
    private int patrolNodeArrayDirection = 1;
    private Vector3 TargetedPatrolNodePosition;


    public GameObject AudioController;
    AudioController audioControllerScript;
    System.Random rand = new System.Random();
    float startAttack = 0.0f;
    float interval = 0.0f;

    void Start()
    {
        AI_State = AI_States.Patrol;
        AI_Transform = gameObject.transform;
        AI_Animation = gameObject.GetComponentInChildren<Animation>();

        Vector3 startPosition = new Vector3(AI_Transform.position.x, AI_Transform.position.y, AI_Transform.position.z);

        GameObject PatrolStartNode = new GameObject("Patrol Start Location");


        PatrolStartNode.transform.position = startPosition;
        PatrolStartNode.transform.parent = PatrolNodes.transform;
        PatrolStartNode.transform.SetAsFirstSibling();

        PatrolNodeArrayTransforms = PatrolNodes.GetComponentsInChildren<Transform>();
        TargetedPatrolNodePosition = PatrolStartNode.transform.position;

        audioControllerScript = AudioController.GetComponent<AudioController>();
    }

    void Update()
    {
        //testing purposes only
        /*
        if (Input.GetKey(KeyCode.U)) { AI_State = AI_States.Idle; }
        else if (Input.GetKey(KeyCode.I)) { AI_State = AI_States.Patrol; }
        else if (Input.GetKey(KeyCode.O)) { AI_State = AI_States.Attack; }
        else if (Input.GetKey(KeyCode.P)) { AI_State = AI_States.Chase; }
        */

        switch (AI_State)
        {
            case AI_States.Spawn:
                AI_Spawn();
                break;

            case AI_States.Idle:
                AI_Idle();
                break;

            case AI_States.Patrol:
                AI_Patrol();
                break;

            case AI_States.Chase:
                AI_Chase();
                break;

            case AI_States.Attack:
                AI_Attack();
                break;
        }
    }

    private void AI_Spawn()
    {
        AI_State = AI_States.Idle;
        //AI_Animation.Play("Spawn");
    }

    private void AI_Idle()
    {
        AI_Animation.Play("Idle");
    }

    private void AI_Patrol()
    {
        AI_Animation["WalkForward"].speed = 0.75f;
        AI_Animation.Play("WalkForward");

        //Get next targeted patrol node's position if arrived at the current one
        if (Vector3.Distance(AI_Transform.position, TargetedPatrolNodePosition) < 0.1f)
        {
            TargetedPatrolNodePosition = GetNextPatrolNodePosition();
        }
        TargetedPatrolNodePosition.y = AI_Transform.position.y;

        AIRotateToward(TargetedPatrolNodePosition);
        AIMoveToward(TargetedPatrolNodePosition);
    }

    private void AI_Attack()
    {
        //AI_Animation.Play("Attack2");
        //call player take damage function
        AI_Animation.CrossFadeQueued("Attack2", 0.3f, QueueMode.CompleteOthers, PlayMode.StopAll);

        interval = Time.time - startAttack;
        if (interval >= 0.5f)
        {
            audioControllerScript.audioEffect.clip = audioControllerScript.attackBune;
            audioControllerScript.audioEffect.Play();

            Player.GetComponent<PlayerHealth>().damageHealth(rand.Next(2, 11));
            interval = 0;
            startAttack = Time.time;
        }
    }

    private void AI_Chase()
    {
        AI_Animation.Play("WalkForward");

        //Get position of player
        Vector3 playerPosition = Player.transform.position;

        playerPosition.y = AI_Transform.position.y;

        AIRotateToward(playerPosition);
        AIMoveToward(playerPosition);
    }

    private void AIRotateToward(Vector3 Target)
    {
        //Rotate AI towards targeted patrol node
        Vector3 targetDirection = Target - AI_Transform.position;
        float turnStep = TurnSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(AI_Transform.forward, targetDirection, turnStep, 0.0f);
        AI_Transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void AIMoveToward(Vector3 Target)
    {
        //Move AI towards targeted patrol node
        float moveStep = MoveSpeed * Time.deltaTime;
        AI_Transform.position = Vector3.MoveTowards(AI_Transform.position, Target, moveStep);
    }

    private Vector3 GetNextPatrolNodePosition()
    {
        switch (PatrolType)
        {
            //Allows GetNextPatrolNodePosition to go through the array of Patrol Nodes from 0 -> lastnode -> 0 and repeat
            case PatrolTypes.Linear:
                if (patrolNodeArrayCounter >= (PatrolNodeArrayTransforms.Length - 1))
                {
                    patrolNodeArrayDirection = -1;
                }
                else if (patrolNodeArrayCounter <= 1)
                {
                    patrolNodeArrayDirection = 1;
                }
                patrolNodeArrayCounter += patrolNodeArrayDirection;
                break;

            case PatrolTypes.Circular:
                //randomly change direction & skip 0

                patrolNodeArrayCounter += patrolNodeArrayDirection;

                //connect start and end of nodes to form circular path
                if (patrolNodeArrayCounter >= PatrolNodeArrayTransforms.Length)
                {
                    patrolNodeArrayCounter = 1;
                }
                else if (patrolNodeArrayCounter <= 0)
                {
                    patrolNodeArrayCounter = (PatrolNodeArrayTransforms.Length - 1);
                }
                break;

            case PatrolTypes.IrregularLinear:
                //randomly change direction & skip 0
                break;
        }
        return PatrolNodeArrayTransforms[patrolNodeArrayCounter].position;
    }
}
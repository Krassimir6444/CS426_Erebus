using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyStates { Idle, Patrol, Attack };

public class EdwardAIController : MonoBehaviour {

    public EdwardAIController Script;
    public GameObject CreatureParentObject;
    public GameObject AIViewRange;
    public List<GameObject> PatrolNodes;

    public float StepSpeed = 1.7f;
    public float TurnSpeed = 15f;

    private Transform CreatureTransform;
    private Animation CreatureAnimation;
    private EnemyStates EnemyState = EnemyStates.Patrol;

    private GameObject StartNode;
    private Vector3 NextPatrolNodePosition;
    private int patrolNodeCounter = 0;
    private int patrolNodeDirection = 1;



    void Start () {
        CreatureTransform = CreatureParentObject.GetComponent<Transform>();
        CreatureAnimation = CreatureParentObject.GetComponentInChildren<Animation>();

        StartNode = new GameObject("Patrol Start Location");
        StartNode.GetComponent<Transform>().position = new Vector3(CreatureTransform.position.x, CreatureTransform.position.y, CreatureTransform.position.z);
        PatrolNodes.Insert(0, StartNode);

        NextPatrolNodePosition = StartNode.GetComponent<Transform>().position;
    }

    void Update () {
        //Used for testing
        if ( Input.GetKey(KeyCode.K) )
        {
            EnemyState = EnemyStates.Patrol;
        }
        else if ( Input.GetKey(KeyCode.L) )
        {
            EnemyState = EnemyStates.Attack;
        }
        else
        {
            //EnemyState = EnemyStates.Idle;
        }



        //Idle
        if (EnemyState == EnemyStates.Idle)
        {
            CreatureAnimation.Play("creature1Idle");
        }
        //Patrol
        else if (EnemyState == EnemyStates.Patrol)
        {
            CreatureAnimation.Play("creature1walkforward");

            //Get next patrol node if arrived at current targeted patrol node
            if (Vector3.Distance(CreatureTransform.position, NextPatrolNodePosition) < 0.5f)
            {
                NextPatrolNodePosition = GetNextPatrolNodePosition();
            }


            //Turn monster towards next patrol node
            Vector3 targetDir = NextPatrolNodePosition - CreatureTransform.position;
            float turnStep = TurnSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(CreatureTransform.forward, targetDir, turnStep, 0.0f);
            CreatureTransform.rotation = Quaternion.LookRotation(newDir);

            //turn view range along with AI
            /*
            Vector3 newDir2 = Vector3.RotateTowards(AIViewRange.GetComponent<Transform>().forward, targetDir, turnStep, 0.0f);
            Quaternion tq = Quaternion.Euler(CreatureTransform.rotation.x, CreatureTransform.rotation.y, CreatureTransform.rotation.z) * Quaternion.Euler(0, 135, 0);
            AIViewRange.GetComponent<Transform>().rotation = tq;*/


            //Move towards next patrol node
            float step = StepSpeed * Time.deltaTime;
            CreatureTransform.position = Vector3.MoveTowards(CreatureTransform.position, NextPatrolNodePosition, step);

            //move view range along with AI
            Vector3 offset = new Vector3(0f, 1f, 0.25f);
            Vector3 creaturePosition = CreatureTransform.position;
            AIViewRange.GetComponent<Transform>().position = creaturePosition + offset;
        }
        //Attack
        else if(EnemyState == EnemyStates.Attack)
        {
            CreatureAnimation.Play("creature1Attack2");
            //damage player
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other " + other);
        Debug.Log("this " + this);
        if (other.tag == "Player")
        {
            Script.EnemyState = EnemyStates.Attack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Script.EnemyState = EnemyStates.Patrol;
        }
    }

    private Vector3 GetNextPatrolNodePosition()
    {
        //Allows GetNextPatrolNodePosition to go through the array of Patrol Nodes from 0 -> lastnode -> 0 and repeat
        if(patrolNodeCounter == PatrolNodes.Count-1)
        {
            patrolNodeDirection = -1;
        }
        if(patrolNodeCounter == 0)
        {
            patrolNodeDirection = 1;
        }

        patrolNodeCounter += patrolNodeDirection;

        return PatrolNodes[patrolNodeCounter].GetComponent<Transform>().position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyStates { Idle, Patrol, Attack };

public class EdwardAIController : MonoBehaviour {

    public GameObject Creature;
    public List<GameObject> PatrolNodes;

    public float StepSpeed = 1.7f;
    public float TurnSpeed = 15f;

    private Transform CreatureTransform;
    private Animation CreatureAnimation;
    private EnemyStates EnemyState;

    private GameObject StartNode;
    private Vector3 NextPatrolNodePosition;
    private int patrolNodeCounter = 0;
    private int patrolNodeDirection = 1;



    void Start () {
        CreatureTransform = Creature.GetComponentInParent<Transform>();
        CreatureAnimation = Creature.GetComponent<Animation>();
        EnemyState = EnemyStates.Idle;

        StartNode = new GameObject("Patrol Start Location");
        StartNode.GetComponent<Transform>().position = new Vector3(Creature.transform.position.x, Creature.transform.position.y, Creature.transform.position.z);
        PatrolNodes.Insert(0, StartNode);

        NextPatrolNodePosition = StartNode.GetComponent<Transform>().position;
    }
	
	void Update () {
        if(Input.GetKey(KeyCode.K))//use for testing
        {
            EnemyState = EnemyStates.Patrol;
        }
        else
        {
            EnemyState = EnemyStates.Idle;
        }



        //Idle
        if(EnemyState == EnemyStates.Idle)
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

            //Move towards next patrol node
            float step = StepSpeed * Time.deltaTime;
            CreatureTransform.position = Vector3.MoveTowards(CreatureTransform.position, NextPatrolNodePosition, step);
            
        }
    }

    private Vector3 GetNextPatrolNodePosition()
    {
        //Allows GetNextPatrolNodePosition to go through the array of Patrol Nodes from 0 -> lastnode -> 0 and repeat
        if(patrolNodeCounter == PatrolNodes.Count)
        {
            patrolNodeDirection = -1;
        }
        if(patrolNodeCounter == -1)
        {
            patrolNodeDirection = 1;
        }

        patrolNodeCounter += patrolNodeDirection;

        return PatrolNodes[patrolNodeCounter].GetComponent<Transform>().position;
    }
}

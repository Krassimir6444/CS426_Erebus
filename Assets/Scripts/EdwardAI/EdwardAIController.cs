using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyStates { Idle, Patrol, Attack };

public class EdwardAIController : MonoBehaviour {

    public GameObject Creature;
    public List<GameObject> PatrolNodes;

    public float speed = 1.7f;

    private Transform CreatureTransform;
    private Animation CreatureAnimation;
    private EnemyStates EnemyState;

    private GameObject StartNode;
    private int patrolNodeCounter;
    private int patrolNodeDirection;
    private Vector3 NextPatrolNodePosition;



    void Start () {
        CreatureTransform = Creature.GetComponentInParent<Transform>();
        CreatureAnimation = Creature.GetComponent<Animation>();
        EnemyState = EnemyStates.Idle;

        StartNode = new GameObject("Patrol Start Location");
        StartNode.GetComponent<Transform>().position = new Vector3(Creature.transform.position.x, Creature.transform.position.y, Creature.transform.position.z);
        PatrolNodes.Insert(0, StartNode);

        NextPatrolNodePosition = StartNode.GetComponent<Transform>().position;
        patrolNodeCounter = 0;
        patrolNodeDirection = 1;
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



        if(EnemyState == EnemyStates.Idle)
        {
            CreatureAnimation.Play("creature1Idle");
        }
        else if (EnemyState == EnemyStates.Patrol)
        {
            if (Vector3.Distance(CreatureTransform.position, NextPatrolNodePosition) < 0.5f)
            {
                NextPatrolNodePosition = GetNextPatrolNodePosition();
            }
            
            Creature.GetComponent<Animation>().Play("creature1walkforward");
            //Creature.GetComponentInParent<Transform>().position =
            float step = speed * Time.deltaTime;
            Creature.GetComponentInParent<Transform>().position = Vector3.MoveTowards(Creature.GetComponentInParent<Transform>().position, NextPatrolNodePosition, step);
            
        }
    }

    private Vector3 GetNextPatrolNodePosition()
    {
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

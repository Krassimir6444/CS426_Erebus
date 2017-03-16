using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyStates { Idle, Patrol, Attack };

public class EdwardEnemyController : MonoBehaviour {

    public GameObject Creature;
    public List<GameObject> PatrolNodes;

    private Animation CreatureAnimation;
    private EnemyStates EnemyState;
    private GameObject StartNode;

	void Start () {
        EnemyState = EnemyStates.Idle;

        StartNode = new GameObject("Patrol Start Location");
        StartNode.GetComponent<Transform>().position = new Vector3(Creature.transform.position.x, Creature.transform.position.y, Creature.transform.position.z);
        PatrolNodes.Insert(0, StartNode);
	}
	
	void Update () {
        if(EnemyState == EnemyStates.Idle)
        {
            Creature.GetComponent<Animation>().Play("creature1Idle");
        }
        //Debug.Log(PatrolNodes[0]);

        //EnemyAnimation = Enemy.GetComponent<Rigidbody>().GetComponentInParent<Animation>();
        //EnemyAnimation.Play("creature1Idle");

        //EnemyAnimation.Play("creature1walkforward");
        //Enemy.GetComponent<Animation>().Play("creature1walkforward");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public Transform player;
	public float attackDistance;
	public float enemyLookDistance;
	public float targetDistance;
	public float rotationSpeed;
	public float moveSpeed;
	Rigidbody rb;
	Renderer myRender;

	// Use this for initialization
	void Start ()
	{
		myRender = GetComponent<Renderer> ();
		rb = GetComponent<Rigidbody> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update ()
	{
		targetDistance = Vector3.Distance (player.position, transform.position);
		if(targetDistance < enemyLookDistance){
			lookAtPlayer ();
			print (" I saw you!");//for debug
		}
		if (targetDistance < attackDistance) {
			attack ();
			print ("Attack");//debug
		} else {
			rest ();
		}
	}

	void lookAtPlayer(){
		transform.rotation = Quaternion.Slerp(transform.rotation, 
			Quaternion.LookRotation(player.position - transform.position),
			rotationSpeed * Time.deltaTime);
	}

	void attack(){
		/* Move to player*/
		//transform.position += transform.forward * moveSpeed * Time.deltaTime;
		rb.AddForce (transform.forward * moveSpeed);
	}
	void rest(){
		// Do nothing
	}
}


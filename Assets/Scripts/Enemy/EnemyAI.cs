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
			myRender.material.color = Color.green;//debug
			lookAtPlayer ();
			print (" I saw you!");//for debug
		}
		if (targetDistance < attackDistance) {
			myRender.material.color = Color.red;//debug
			attack ();
			print ("Attack");//debug
		} else {
			rest ();
		}
	}

	void lookAtPlayer(){
		Quaternion rotation = Quaternion.LookRotation (player.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, 
			rotation,
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


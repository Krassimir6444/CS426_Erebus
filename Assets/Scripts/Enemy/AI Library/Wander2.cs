﻿using UnityEngine;
using System.Collections;
//https://www.reddit.com/r/gamedev/comments/3q0yn8/here_is_some_free_unity_movement_ai_ive_made/
public class Wander2 : MonoBehaviour {

	public float wanderRadius = 1.2f;

	public float wanderDistance = 2f;

	//maximum amount of random displacement a second
	public float wanderJitter = 40f;

	private Vector3 wanderTarget;

	private Steering steeringBasics;

	void Start()
	{
		//stuff for the wander behavior
		float theta = Random.value * 2 * Mathf.PI;

		//create a vector to a target position on the wander circle
		wanderTarget = new Vector3(wanderRadius * Mathf.Cos(theta), 0f, 0f);//wanderRadius * Mathf.Cos(theta));

		steeringBasics = GetComponent<Steering>();
	}

	public Vector3 getSteering()
	{
		//get the jitter for this time frame
		float jitter = wanderJitter * Time.deltaTime;

		//add a small random vector to the target's position
		wanderTarget += new Vector3(Random.Range(-1f, 1f) * jitter, 0f, 0f);//Random.Range(-1f, 1f) * jitter);

		//make the wanderTarget fit on the wander circle again
		wanderTarget.Normalize();
		wanderTarget *= wanderRadius;

		//move the target in front of the character
		Vector3 targetPosition = transform.position + transform.right * wanderDistance + wanderTarget;

		//Debug.DrawLine(transform.position, targetPosition);

		return steeringBasics.seek(targetPosition);
	}


}

using UnityEngine;
using System.Collections;
//https://www.reddit.com/r/gamedev/comments/3q0yn8/here_is_some_free_unity_movement_ai_ive_made/
public class Wander2AI : MonoBehaviour
{

	private Steering steeringBasics;
	private Wander2 wander;

	// Use this for initialization
	void Start()
	{
		steeringBasics = GetComponent<Steering>();
		wander = GetComponent<Wander2>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 accel = wander.getSteering();

		steeringBasics.steer(accel);
		steeringBasics.lookWhereYoureGoing();
	}
}

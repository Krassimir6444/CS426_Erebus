using UnityEngine;
using System.Collections;

public class PursueAI : MonoBehaviour {

	public Rigidbody target;

	private Steering steeringBasics;
	private Pursue pursue;

	// Use this for initialization
	void Start () {
		steeringBasics = GetComponent<Steering>();
		pursue = GetComponent<Pursue>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 accel = pursue.getSteering(target);

		steeringBasics.steer(accel);
		steeringBasics.lookWhereYoureGoing();
	}
}

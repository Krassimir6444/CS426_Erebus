using UnityEngine;
using System.Collections;

public class Wander1AI : MonoBehaviour {

	private Steering steeringBasics;
	private Wander1 wander;

	// Use this for initialization
	void Start () {
		steeringBasics = GetComponent<Steering>();
		wander = GetComponent<Wander1>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 accel = wander.getSteering();

		steeringBasics.steer(accel);
		steeringBasics.lookWhereYoureGoing();
	}
}

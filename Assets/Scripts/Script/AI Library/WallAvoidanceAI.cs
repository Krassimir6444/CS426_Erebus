using UnityEngine;
using System.Collections;

public class WallAvoidanceAI: MonoBehaviour
{

	public LinePath path;

	private Steering steeringBasics;
	private WallAvoidance wallAvoidance;
	private FollowPath followPath;

	// Use this for initialization
	void Start()
	{
		path.calcDistances();

		steeringBasics = GetComponent<Steering>();
		wallAvoidance = GetComponent<WallAvoidance>();
		followPath = GetComponent<FollowPath>();
	}

	// Update is called once per frame
	void Update()
	{
		if (isAtEndOfPath())
		{
			path.reversePath();
		}

		Vector3 accel = wallAvoidance.getSteering();

		if (accel.magnitude < 0.005f)
		{
			accel = followPath.getSteering(path);
		}

		steeringBasics.steer(accel);
		steeringBasics.lookWhereYoureGoing();

		path.draw();
	}

	public bool isAtEndOfPath()
	{
		return Vector3.Distance(path.endNode, transform.position) < followPath.stopRadius;
	}
}

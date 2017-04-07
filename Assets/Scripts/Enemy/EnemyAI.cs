//Zixuan Zeng

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class EnemyAI : MonoBehaviour {

	public Transform player;
	public GameObject enemy;
	public float attackDistance;
	public float enemyLookDistance;
	public float targetDistance;
	public float rotationSpeed;
	public float moveSpeed;

	public float speed = 2;
	public float directionChangeInterval = 3;
	public float maxHeadingChange = 102;

	CharacterController controller;
	float heading;
	Vector3 targetRotation;

	private PlayerHealth ph;
	public int damagePlayerHealth;
	public GameObject playerModel;
	private Animation enemyAnimation;
	private bool playerInRange;

	// Use this for initialization
	void Start ()
	{
		//rb = GetComponent<Rigidbody> ();
		controller = GetComponent<CharacterController>();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerModel = GameObject.FindGameObjectWithTag ("Player");
		ph = playerModel.GetComponent<PlayerHealth> ();
		enemyAnimation = enemy.GetComponent<Animation> ();
	}

	void Awake ()
	{
		controller = GetComponent<CharacterController>();

		// Set random initial rotation
		heading = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, heading, 0);

		StartCoroutine(NewHeading());
	}

	// Update is called once per frame
	void Update ()
	{
		/*
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
		}*/
		if (playerInRange == true) {
			attack ();
		} else {
			rest ();
		}
	}

	void lookAtPlayer(){
		var rotation = Quaternion.LookRotation (player.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, 
			rotation,
			rotationSpeed * Time.deltaTime);
	}

	void attack(){
		/* Move to player*/
		//transform.position += transform.forward * moveSpeed * Time.deltaTime;
		//rb.AddForce (transform.forward * moveSpeed);
		enemyAnimation.Play ("creature1Attack2");
		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
		var forward = transform.TransformDirection(Vector3.forward);
		controller.SimpleMove(forward * speed);
		//ph.damageHealth (damagePlayerHealth);
	}
	void rest(){
		// Wandering
		enemyAnimation.Play ("creature1walkforward");
		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
		var forward = transform.TransformDirection(Vector3.forward);
		controller.SimpleMove(forward * speed);
	}

	/// <summary>
	/// Repeatedly calculates a new direction to move towards.
	/// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
	/// </summary>
	IEnumerator NewHeading ()
	{
		while (true) {
			NewHeadingRoutine();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}

	/// <summary>
	/// Calculates a new direction to move towards.
	/// </summary>
	void NewHeadingRoutine ()
	{
		var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
		var ceil  = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
		heading = Random.Range(floor, ceil);
		targetRotation = new Vector3(0, heading, 0);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			playerInRange = true;
			//nearbyPlayer = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			playerInRange = false;
			//nearbyPlayer = null;
		}
	}
		
}
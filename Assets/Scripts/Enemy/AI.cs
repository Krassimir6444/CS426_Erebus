using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	public int moveSpeed = 10;  //per second 
	//bool inBoundary;
	Vector3 computerDirection = Vector3.left; 
	Vector3 moveDirection = Vector3.zero; 
	Vector3 newPosition = Vector3.zero; 

	// Use this for initialization
	void Start () {
		//inBoundary = true;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = computerDirection * (moveSpeed * Time.deltaTime);
		newPosition = transform.position + newPosition;  
		//while (inBoundary == true) {
			newPosition.x = Mathf.Clamp (newPosition.x, -50, 80);  
			newPosition.z = Mathf.Clamp (newPosition.z, -50, 80);
			transform.position = newPosition; 
			if (newPosition.x > 80) {
				newPosition.x = 80;
				computerDirection.x *= -1;
			} else if (newPosition.z > 80) {
				newPosition.z = 80;
				computerDirection.z *= -1;
			} else if (newPosition.x < -50) {
				newPosition.x = -50;
				computerDirection.x *= -1;
			} else if (newPosition.z < -50) {
				newPosition.z = -50;
				computerDirection.z *= -1;
			}
		//}
	}
}

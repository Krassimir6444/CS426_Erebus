using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDigitalScent : MonoBehaviour {

    public GameObject Player;
    public GameObject NodePrefab;
    float startTime = 0.0f;
    float interval = 0.0f;

	void Start () {
        startTime = Time.time;
    }
	
	void Update () {
        interval = Time.time - startTime;
        if (interval >= 2)
        {   
            Vector3 nodePosition = new Vector3(Player.transform.position.x, 0.1f, Player.transform.position.z);
            Instantiate(NodePrefab, nodePosition, Quaternion.identity);
            startTime = Time.time;
            interval = 0;
        }
    }
}

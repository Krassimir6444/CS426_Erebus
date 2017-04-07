using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNode : MonoBehaviour {

    public GameObject Player;
    PlayerDigitalScent playerDigitalScent;

    public GameObject thisNode;
    public int NodeID = 0;

    float startTime = 0.0f;
    float interval = 0.0f;

    void Awake() {
        NodeID = Player.GetComponentInChildren<PlayerDigitalScent>().nodeCount++;
        thisNode.GetComponent<BoxCollider>().enabled = true;
    }

    void Start () { 
        startTime = Time.time;
    }
	
	void Update () {
        interval = Time.time - startTime;
        if (interval >= 5)
            { Destroy(thisNode); }
    }
}

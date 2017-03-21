using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNode : MonoBehaviour {

    public GameObject thisNode;
    float startTime = 0.0f;
    float interval = 0.0f;

    void Start () {
        startTime = Time.time;
    }
	
	void Update () {
        interval = Time.time - startTime;
        if (interval >= 10)
            { Destroy(thisNode); }
    }
}

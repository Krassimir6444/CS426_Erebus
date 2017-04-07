using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    public GameObject toBillboard;
    public GameObject playerView;

    void Start () {
		
	}

	void Update () {
        toBillboard.transform.LookAt(playerView.GetComponent<Camera>().transform);
	}
}

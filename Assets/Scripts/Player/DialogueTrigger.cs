using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public GameObject dialogue;

    float startTime = 0.0f;
    float interval = 0.0f;
    bool displayed = false;

    void Start () { }
	
	void Update ()
    {
        if (displayed) { interval = Time.time - startTime; }
        if (interval >= 5) { Destroy(dialogue); }
    }

	void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag ("Player"))
        {
			dialogue.SetActive (true);
            startTime = Time.time;
            displayed = true;
        }
	}

    /*
	void OnTriggerExit(Collider other)
	{
        dialogue.SetActive (false);
        //Destroy(dialogue);
	}*/
    
}

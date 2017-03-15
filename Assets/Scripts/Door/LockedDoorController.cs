using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorController : MonoBehaviour {

    public GameObject Player;
    public GameObject Door;

    private PlayerInventory playerInventory;

	void Start () {
        playerInventory = Player.GetComponent<PlayerInventory>();
    }
	
	void Update () {
		//empty
	}

    private void OnTriggerEnter(Collider other)
    {
        if (playerInventory.hasKeycard)
        {
            Door.GetComponent<Animation>().Play("open");
        }
        //else do nothing
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerInventory.hasKeycard)
        {
            Door.GetComponent<Animation>().Play("close");
        }
        //else do nothing
    }
}

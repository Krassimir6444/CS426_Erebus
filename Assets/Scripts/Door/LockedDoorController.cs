using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorController : MonoBehaviour {

    public GameObject Player;
    public GameObject Door;
    public UnityEngine.UI.Text KeycardPrompt;

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
        else
        {
            KeycardPrompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerInventory.hasKeycard)
        {
            Door.GetComponent<Animation>().Play("close");
        }
        else
        {
            KeycardPrompt.gameObject.SetActive(false);
        }
    }
}

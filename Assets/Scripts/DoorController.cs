//put in "base", where ever the trigger is at
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public GameObject Door;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Door.GetComponent<Animation>().Play("open");
        
    }
    private void OnTriggerExit(Collider other)
    {
        Door.GetComponent<Animation>().Play("close");
    }
}
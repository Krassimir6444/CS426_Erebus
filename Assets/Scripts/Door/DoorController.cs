using UnityEngine;
using UnityEngine.UI;

//put in "base", where ever the trigger is at
public class DoorController : MonoBehaviour
{
    public GameObject Door;
    //public GameObject Player;
    //public UnityEngine.UI.Text KeycardPrompt;         //handled in PlayerInteract

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void openDoor()
    {
        //Door.SetActive(false);
        //Door.gameObject.transform.Translate(0, 5, 0);
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
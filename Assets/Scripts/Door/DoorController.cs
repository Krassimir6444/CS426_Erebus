using UnityEngine;
using UnityEngine.UI;

//put in "base", where ever the trigger is at
public class DoorController : MonoBehaviour
{
    public GameObject Door;
    //public GameObject Player;
    //public UnityEngine.UI.Text KeycardPrompt;         //handled in PlayerInteract

    public GameObject AudioController;
    AudioController audioControllerScript;

    private void Start()
    {
        audioControllerScript = AudioController.GetComponent<AudioController>();
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
        audioControllerScript.audioEffect.clip = audioControllerScript.doorToggle;
        audioControllerScript.audioEffect.Play();

    }
    private void OnTriggerExit(Collider other)
    {
        Door.GetComponent<Animation>().Play("close");
        audioControllerScript.audioEffect.clip = audioControllerScript.doorToggle;
        audioControllerScript.audioEffect.Play();
    }
}
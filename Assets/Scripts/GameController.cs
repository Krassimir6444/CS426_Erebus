using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour {

    public GameObject Player;
    public GameObject AudioController;
    public GameObject Backstory;
    public RectTransform Background;
    public RectTransform Premise;
    public GameObject HUD;

    private RigidbodyFirstPersonController RbFPCScript;
    private GameObject backgroundMusic;
    private AudioSource theme;
    private bool cursorOn = true;
    public bool gameStarted = false;

    void Start()
    {
        RbFPCScript = Player.GetComponent<RigidbodyFirstPersonController>();

        backgroundMusic = AudioController.transform.GetChild(0).gameObject;
        theme = backgroundMusic.GetComponent<AudioSource>();

        theme.clip = AudioController.GetComponent<AudioController>().BackgroundStory;
        theme.Play();

        HUD.SetActive(false);
        Backstory.SetActive(true);
        Vector2 UserScreenSize = new Vector2(Screen.width, Screen.height);
        Background.sizeDelta = UserScreenSize;
        Premise.sizeDelta = UserScreenSize;
        CursorIOSwitch(cursorOn);
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && gameStarted == false) BeginGame();
    }

    void BeginGame()
    {
        gameStarted = true;
        cursorOn = false;
        Backstory.SetActive(false);
        HUD.SetActive(true);

        theme.clip = AudioController.GetComponent<AudioController>().Level1;
        theme.Play();
        CursorIOSwitch(cursorOn);

    }

    void CursorIOSwitch(bool cursorOn) {
        if (cursorOn)
        {
            RbFPCScript.mouseLook.lockCursor = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            RbFPCScript.mouseLook.lockCursor = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}


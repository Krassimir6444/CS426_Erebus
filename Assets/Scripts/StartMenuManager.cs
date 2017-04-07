using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager: MonoBehaviour {
    public RectTransform Background;
    public RectTransform BlackBg;

    private void Start()
    {
        try
        {
            Background.sizeDelta = new Vector2( ((Screen.height-100) * 2) , (Screen.height-100) );
            BlackBg.sizeDelta = new Vector2(Screen.width, Screen.height);
        }
        catch
        {

        }
    }
	
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

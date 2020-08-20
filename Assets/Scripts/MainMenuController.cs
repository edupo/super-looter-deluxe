using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public string gameScene;
    public string tutorScene;
    public bool alwaysTutorial = false;

    private void Start()
    {
        if (alwaysTutorial)
        {
            GetComponent<GameOverMenuManager>().sceneName = tutorScene;
            return;
        }

        if (PlayerPrefs.GetInt("tutorial", 0) == 0)
            GetComponent<GameOverMenuManager>().sceneName = tutorScene;        
        else
            GetComponent<GameOverMenuManager>().sceneName = gameScene;

        PlayerPrefs.SetInt("tutorial", 1);
    }
}

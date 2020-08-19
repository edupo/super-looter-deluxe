using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour
{
    public float sleepFor = 5f;
    public bool sleeping = true;
    public string sceneName;

    public UnityEvent onEndSleep;
    public UnityEvent onContinue;

    public bool triggered = false;

    private void Start()
    {
        Invoke("EndSleep", sleepFor);
    }

    public void EndSleep()
    {
        sleeping = false;
        onEndSleep.Invoke();
    }

    public void Continue()
    {
        if (sleeping || triggered) return;
        triggered = true;
        Debug.Log("Loading " + sceneName);
        onContinue.Invoke();
        SceneManager.LoadSceneAsync(sceneName);
    }
}

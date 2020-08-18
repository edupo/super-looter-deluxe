using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float initialTime = 60f;

    [Header("Events")]
    public GlobalEvent gameOver;
    public GlobalEvent gameStarted;
    public GlobalEvent exit;

    private float timeToGo;
    private bool playing;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void StartGame(Object obj)
    {
        timeToGo = initialTime;
        playing = true;
        gameStarted.Raise();
    }

    public void Stop(Object obj)
    {
        playing = false;
    }

    public float TimeToGo { get { return timeToGo; } }

    public void Update()
    {
        if (!playing)
            return;

        timeToGo -= Time.deltaTime;
        if (timeToGo < 0)
        {
            timeToGo = 0f;
            gameOver.Raise();
        }
    }
}

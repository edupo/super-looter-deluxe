using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float initialTime = 60f;
    public float startDelay = 2f;

    [Header("Events")]
    public GlobalEvent gameOver;
    public GlobalEvent gameStarted;
    public GlobalEvent exit;

    private float timeToGo;
    private bool playing = false;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void StartGame()
    {
        Invoke("DoStartGame", startDelay);
    }

    public void DoStartGame()
    {
        timeToGo = initialTime;
        playing = true;
        gameStarted.Raise();
    }

    public void Stop()
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
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOver.Raise();
        SceneManager.LoadSceneAsync("GameOver");
    }
}

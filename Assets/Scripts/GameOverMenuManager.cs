using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("Menu");
    }
}

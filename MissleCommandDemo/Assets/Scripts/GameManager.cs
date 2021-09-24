using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas mainGameCanvas;

    private void Start()
    {
        DontDestroyOnLoad(mainGameCanvas);
    }

    void Update()
    {
        QuitGame();
        RestartGame();    
    }

    public void GameOver()
    {

    }

    private void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("quitting game");
            Application.Quit();
        }
    }

    private void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("restart called");
            SceneManager.LoadScene(0);
        }
    }
}

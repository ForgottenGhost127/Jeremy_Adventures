using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    #region Public Methods
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    #endregion

}

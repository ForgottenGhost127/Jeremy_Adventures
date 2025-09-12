using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MMenuController : MonoBehaviour
{
    #region Properties
    public AudioSource menuMusic;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        if (menuMusic != null)
        {
            menuMusic.Play();
        }
    }
    #endregion

    #region Public Methods
    public void OnStartGame()
    {
        if (menuMusic != null)
        {
            menuMusic.Stop();
        }

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void OnExitGame()
    {
        if (menuMusic != null)
        {
            menuMusic.Stop();
        }

        Application.Quit();
    }
    #endregion

}

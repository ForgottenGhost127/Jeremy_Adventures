using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCanController : MonoBehaviour
{
    #region Fields
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button exitGameButton;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        backToMenuButton.onClick.AddListener(BackToMenu);
        exitGameButton.onClick.AddListener(ExitGame);
    }
    #endregion

    #region Public Methods
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

}

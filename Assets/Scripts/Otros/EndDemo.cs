using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EndDemo : MonoBehaviour
{
    #region Fields
    [SerializeField] private string targetSceneName = "EndDemo";
    [SerializeField] private string playerTag = "Player";
    #endregion

    #region Unity Callbacks
    void Start()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            LoadEndDemoScene();
        }
    }
    #endregion

    #region Public Methods
    public void LoadEndDemoScene()
    {
        LoadScene(targetSceneName);
    }
    #endregion

    #region Private Methods
    private void LoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"La escena '{sceneName}' no se encuentra en Build Settings o no existe.");
        }
    }
    #endregion

}

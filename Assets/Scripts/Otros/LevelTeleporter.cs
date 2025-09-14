using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelTeleporter : MonoBehaviour
{
	#region Fields
	[SerializeField] private string targetSceneName = "UraVillage";
	[SerializeField] private string spawnPointTag = "Spawn1";
	#endregion

	#region Unity Callbacks
	void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            TeleportToNextLevel();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void TeleportToNextLevel()
    {
        PlayerPrefs.SetString("SpawnPointTag", spawnPointTag);
        SceneManager.LoadScene(targetSceneName);
    }
    #endregion

}

using UnityEngine;
using System;

public class PlayerSpawner : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private string defaultSpawnTag = "Spawn1";
    #endregion

    #region Unity Callbacks
    void Start()
    {
        SpawnPlayer();
    }
    void Update()
    {

    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void SpawnPlayer()
    {
        string spawnTag = PlayerPrefs.GetString("SpawnPointTag", defaultSpawnTag);
        GameObject spawnPoint = GameObject.FindGameObjectWithTag(spawnTag);

        if (spawnPoint != null)
        {
            Debug.Log($"Spawn Point encontrado en posición: {spawnPoint.transform.position}");
            GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

            if (allPlayers.Length > 0)
            {
                GameObject player = allPlayers[0];
                Debug.Log($"Jugador encontrado en posición: {player.transform.position}");
                player.transform.position = spawnPoint.transform.position;
                Debug.Log($"Jugador movido a nueva posición: {player.transform.position}");

                for (int i = 1; i < allPlayers.Length; i++)
                {
                    Debug.Log("Destruyendo jugador duplicado");
                    Destroy(allPlayers[i]);
                }
            }
            else
            {
                Debug.LogWarning("No se encontró ningún objeto con tag 'Player' en la escena.");
            }
        }
        else
        {
            Debug.LogWarning($"No se encontró ningún objeto con tag '{spawnTag}' en la escena.");
        }
        PlayerPrefs.DeleteKey("SpawnPointTag");
    }
    #endregion
}
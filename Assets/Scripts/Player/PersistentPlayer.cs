using UnityEngine;
using System;

public class PersistentPlayer : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    private static PersistentPlayer instance;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Player marcado como persistente");
        }
        else
        {
            Debug.Log("Destruyendo Player duplicado");
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion

}

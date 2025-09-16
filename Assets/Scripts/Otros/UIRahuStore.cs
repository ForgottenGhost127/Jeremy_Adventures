using Inventory;
using System;
using TMPro;
using UnityEngine;

public class UIRahuStore : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private PlayerCoins _playerCoins;
    [SerializeField] private TextMeshProUGUI _collectedCoins;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        
    }

    void Update()
    {
        _collectedCoins.text = _playerCoins.ToString();
    }
    #endregion

    #region Public Methods
    
    #endregion

    #region Private Methods
    #endregion

}

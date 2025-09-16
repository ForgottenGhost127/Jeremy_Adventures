using UnityEngine;
using System;

public class PlayerCoins : MonoBehaviour
{
    #region Properties
    public static PlayerCoins instance { get; private set; }
    public int Coins => coins;
    #endregion

    #region Fields
    [Header("Coins")]
    [SerializeField] private int coins = 2; 
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Public Methods
    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log($"Monedas añadidas: +{amount}. Total: {coins}");
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            Debug.Log($"Monedas gastadas: -{amount}. Restantes: {coins}");
            return true;
        }
        Debug.Log($"No tienes suficientes monedas. Necesitas: {amount}, Tienes: {coins}");
        return false;
    }

    public override string ToString()
    {
        return coins.ToString();
    }
    #endregion

    #region Private Methods
    #endregion

}

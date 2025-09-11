using UnityEngine;
using System;

public class PlayerCoins : MonoBehaviour
{
    public static PlayerCoins instance;

    [Header("Coins")]
    [SerializeField] private int coins = 0;

    public int Coins => coins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public override string ToString()
    {
        return coins.ToString();
    }

}

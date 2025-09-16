using Inventory;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRahuStore : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [Header("UI References")]
    [SerializeField] private PlayerCoins playerCoins;
    [SerializeField] private TextMeshProUGUI collectedCoins;

    [Header("Items for Sale")]
    [SerializeField] private Weapon upgradedSword;
    [SerializeField] private int upgradedSwordPrice = 100;
    [SerializeField] private Button buyWeaponButton;

    [Header("Items to Sell")]
    [SerializeField] private Button[] sellButtons;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        playerCoins = FindFirstObjectByType<PlayerCoins>();
        SetupButtons();
    }

    void Update()
    {
        if (collectedCoins != null && playerCoins != null)
        {
            collectedCoins.text = playerCoins.ToString();
        }

        UpdateButtonStates();
    }
    #endregion

    #region Public Methods
    public void BuyUpgradedSword()
    {
        if (playerCoins.SpendCoins(upgradedSwordPrice))
        {
            InventorySystem.Instance.AddItem(upgradedSword, 1);
            Debug.Log("¡Espada mejorada comprada!");
        }
    }

    public void SellItem(int itemType)
    {
        switch (itemType)
        {
            case 0:
                SellFruitsByType(FruitType.Sellable);
                break;
            case 1:
                SellFruitsByType(FruitType.Healing);
                break;
        }
    }
    #endregion

    #region Private Methods
    private void SetupButtons()
    {
        if (buyWeaponButton != null)
        {
            buyWeaponButton.onClick.AddListener(BuyUpgradedSword);
        }
    }

    private void UpdateButtonStates()
    {
        if (buyWeaponButton != null)
        {
            buyWeaponButton.interactable = playerCoins.Coins >= upgradedSwordPrice;
        }
    }

    private void SellFruitsByType(FruitType fruitType)
    {
        var allItems = InventorySystem.Instance.GetAllItems();

        foreach (var slot in allItems)
        {
            if (slot.item is Fruit fruit && fruit.fruitType == fruitType)
            {
                int sellValue = fruit.sellValue;
                if (sellValue > 0)
                {
                    InventorySystem.Instance.RemoveItem(slot.item, 1);
                    playerCoins.AddCoins(sellValue);
                    Debug.Log($"Vendido {fruit.itemName} por {sellValue} monedas");
                    break;
                }
            }
        }
    }
    #endregion

}

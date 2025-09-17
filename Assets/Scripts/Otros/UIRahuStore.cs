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
    [SerializeField] private int upgradedSwordPrice = 180;
    [SerializeField] private Button buyWeaponButton;

    [Header("Sell System")]
    [SerializeField] private Button sellButton;
    [SerializeField] private TextMeshProUGUI sellInfoText;

    [Header("Navigation")]
    [SerializeField] private Button cancelButton;
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
        UpdateSellInfo();
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

    public void SellNextItem()
    {
        var sellableItem = FindFirstSellableItem();

        if (sellableItem.fruit != null)
        {
            int sellValue = sellableItem.fruit.sellValue;
            InventorySystem.Instance.RemoveItem(sellableItem.fruit, 1);
            playerCoins.AddCoins(sellValue);
            Debug.Log($"Vendido {sellableItem.fruit.itemName} por {sellValue} monedas");
        }
        else
        {
            Debug.Log("No tienes nada que vender");
        }
    }
    public void CloseStore()
    {
        gameObject.SetActive(false);
        Debug.Log("Tienda cerrada por botón Cancel");
    }
    #endregion

    #region Private Methods
    private void SetupButtons()
    {
        if (buyWeaponButton != null)
        {
            buyWeaponButton.onClick.AddListener(BuyUpgradedSword);
        }

        if (sellButton != null)
        {
            sellButton.onClick.AddListener(SellNextItem);
        }

        if (cancelButton != null)
        {
            cancelButton.onClick.AddListener(CloseStore);
        }
    }

    private void UpdateButtonStates()
    {
        if (buyWeaponButton != null)
        {
            buyWeaponButton.interactable = playerCoins.Coins >= upgradedSwordPrice;
        }

        if (sellButton != null)
        {
            var sellableItem = FindFirstSellableItem();
            sellButton.interactable = sellableItem.fruit != null;
        }
    }

    private void UpdateSellInfo()
    {
        if (sellInfoText != null)
        {
            var sellableItem = FindFirstSellableItem();

            if (sellableItem.fruit != null)
            {
                sellInfoText.text = $"Sell {sellableItem.fruit.itemName} for {sellableItem.fruit.sellValue} coins";
            }
            else
            {
                sellInfoText.text = "No items to sell";
            }
        }
    }

    private (Fruit fruit, int quantity) FindFirstSellableItem()
    {
        var allItems = InventorySystem.Instance.GetAllItems();

        foreach (var slot in allItems)
        {
            if (slot.item is Fruit fruit && fruit.sellValue > 0)
            {
                return (fruit, slot.quantity);
            }
        }

        return (null, 0);
    }
    #endregion

}

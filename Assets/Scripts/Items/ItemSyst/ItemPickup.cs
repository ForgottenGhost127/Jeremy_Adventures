using UnityEngine;
using System;
using Inventory;

public class ItemPickup : MonoBehaviour
{
	#region Fields
	[Header("Pickup Settings")]
	[SerializeField] private ScriptableObject itemData;
	[SerializeField] private int quantity = 1;

	private SpriteRenderer _spriteRenderer;
	private bool _playerNearby = false;
	#endregion

	#region Unity Callbacks
	void Start()
    {
		_spriteRenderer = GetComponent<SpriteRenderer>();
		SetupVisual();
    }

    void Update()
    {
		HandlePickupInput();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = false;
        }
    }
    #endregion

    #region Public Methods
    public void Pickup()
    {
        bool addedToInventory = false;

        if (itemData is Fruit fruit)
        {
            if (fruit.fruitType == FruitType.Healing)
            {
                if (PlayerBuffs.Instance.CurrentHealth < PlayerBuffs.Instance.MaxHealth)
                {
                    fruit.Use();
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    addedToInventory = InventorySystem.Instance.AddItem(itemData, quantity);
                }
            }
            else
            {
                addedToInventory = InventorySystem.Instance.AddItem(itemData, quantity);
            }
        }
        else
        {
            addedToInventory = InventorySystem.Instance.AddItem(itemData, quantity);
        }

        if (addedToInventory)
        {
            Debug.Log($"Recogido: {itemData.name} x{quantity}");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventario lleno!");
        }
    }
    #endregion

    #region Private Methods
    private void SetupVisual()
    {
        if (itemData != null && _spriteRenderer != null)
        {
            if (itemData is Fruit fruit)
            {
                _spriteRenderer.sprite = fruit.icon;
            }
            else if (itemData is Weapon weapon)
            {
                _spriteRenderer.sprite = weapon.icon;
            }
        }
    }

    private void HandlePickupInput()
    {
        if (_playerNearby && Input.GetKeyDown(KeyCode.Q))
        {
            Pickup();
        }
    }

    //Aplicar cuando se añada un UI de Inventario físico al juego para consumir objetos curativos/Buffs desde este y no de forma automatica
    //if (selectedItem is Fruit fruit)
    //{
    //  fruit.Use();
    //  InventorySystem.Instance.RemoveItem(selectedItem, 1);
    //}
    #endregion

}

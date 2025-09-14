using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

namespace Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        #region Properties
        public static InventorySystem Instance { get; private set; }
        #endregion

        #region Fields
        [Header("Inventory Settings")]
        public int maxSlots = 20;

        [Header("Current Inventory")]
        public List<InventorySlot> slots = new List<InventorySlot>();

        [Header("Equipped Weapon")]
        public Weapon currentWeapon;
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeInventory();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Public Methods
        public bool AddItem(ScriptableObject item, int quantity = 1)
        {
            if (item == null) return false;

            int maxStack = GetMaxStackSize(item);

            if (maxStack > 1)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i].CanStack(item))
                    {
                        int spaceAvailable = maxStack - slots[i].quantity;
                        int amountToAdd = Mathf.Min(quantity, spaceAvailable);

                        slots[i].quantity += amountToAdd;
                        quantity -= amountToAdd;

                        if (quantity <= 0)
                        {
                            Debug.Log($"Añadido {item.name} al inventario (stackeado)");
                            return true;
                        }
                    }
                }
            }

            while (quantity > 0)
            {
                int emptySlotIndex = FindEmptySlot();
                if (emptySlotIndex == -1)
                {
                    Debug.Log("Inventario lleno!");
                    return false;
                }

                int amountToAdd = Mathf.Min(quantity, maxStack);
                slots[emptySlotIndex] = new InventorySlot(item, amountToAdd);
                quantity -= amountToAdd;
            }

            Debug.Log($"Añadido {item.name} al inventario");
            return true;
        }

        public bool RemoveItem(ScriptableObject item, int quantity = 1)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].item != null && slots[i].item.name == item.name)
                {
                    if (slots[i].quantity >= quantity)
                    {
                        slots[i].quantity -= quantity;

                        if (slots[i].quantity <= 0)
                        {
                            slots[i].ClearSlot();
                        }

                        Debug.Log($"Removido {quantity} {item.name}");
                        return true;
                    }
                }
            }

            Debug.Log($"No hay suficiente {item.name} en el inventario");
            return false;
        }

        public void UseItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= slots.Count) return;

            InventorySlot slot = slots[slotIndex];
            if (slot.IsEmpty) return;

            if (slot.item is Fruit fruit)
            {
                fruit.Use();
                if (fruit.fruitType == FruitType.Healing || fruit.fruitType == FruitType.AttackBoost)
                {
                    RemoveItem(slot.item, 1);
                }
            }
            else if (slot.item is Weapon weapon)
            {
                weapon.Use();
                EquipWeapon(weapon);
            }
        }

        public int GetCurrentWeaponDamage()
        {
            return currentWeapon?.damage ?? 0;
        }

        public bool HasItem(string itemName, int quantity = 1)
        {
            int totalCount = 0;
            foreach (var slot in slots)
            {
                if (slot.item != null && slot.item.name == itemName)
                {
                    totalCount += slot.quantity;
                }
            }
            return totalCount >= quantity;
        }

        public List<InventorySlot> GetAllItems()
        {
            return slots.Where(slot => !slot.IsEmpty).ToList();
        }

        [ContextMenu("Print Inventory")]
        public void PrintInventory()
        {
            Debug.Log("=== INVENTARIO ===");
            for (int i = 0; i < slots.Count; i++)
            {
                if (!slots[i].IsEmpty)
                {
                    Debug.Log($"Slot {i}: {slots[i].item.name} x{slots[i].quantity}");
                }
            }
            Debug.Log($"Arma equipada: {(currentWeapon?.itemName ?? "Ninguna")}");
        }
        #endregion

        #region Private Methods
        private void InitializeInventory()
        {
            for (int i = 0; i < maxSlots; i++)
            {
                slots.Add(new InventorySlot(null, 0));
            }
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].IsEmpty)
                    return i;
            }
            return -1;
        }

        private void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Debug.Log($"Arma equipada: {weapon.itemName} - Daño: {weapon.damage}");
        }

        private int GetMaxStackSize(ScriptableObject item)
        {
            if (item is Fruit fruit) return fruit.maxStackSize;
            if (item is Weapon weapon) return weapon.maxStackSize;
            return 1;
        }
        #endregion
    }
}

using System;
using UnityEngine;
using static UnityEditor.Progress;

namespace Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        #region Properties
        public ScriptableObject item;
        public int quantity;
        public bool IsEmpty => item == null || quantity <= 0;
        #endregion

        #region Fields
        public InventorySlot(ScriptableObject newItem, int newQuantity)
        {
            item = newItem;
            quantity = newQuantity;
        }
        #endregion

        #region Public Methods
        public bool CanStack(ScriptableObject otherItem)
        {
            if (item == null || otherItem == null) return false;
            if (item.GetType() != otherItem.GetType()) return false;

            int maxStack = GetMaxStackSize(item);
            return item.name == otherItem.name && quantity < maxStack;
        }

        public void ClearSlot()
        {
            item = null;
            quantity = 0;
        }
        #endregion

        #region Private Methods
        private int GetMaxStackSize(ScriptableObject obj)
        {
            if (obj is Fruit fruit) return fruit.maxStackSize;
            if (obj is Weapon weapon) return weapon.maxStackSize;
            return 1;
        }
        #endregion
    }
}

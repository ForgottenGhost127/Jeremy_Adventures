using System;
using System.Xml.Linq;
using UnityEngine;


namespace Inventory
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
    public class Weapon : ScriptableObject
    {
        #region Properties
        [Header("Basic Properties")]
        public string itemName;
        public string description;
        public Sprite icon;
        public int maxStackSize = 1;

        [Header("Weapon Properties")]
        public int damage = 10;
        public float attackSpeed = 1f;
        public int price = 0;
        #endregion

        #region Public Methods
        public void Use()
        {
            EquipWeapon();
        }
        #endregion

        #region Private Methods
        private void EquipWeapon()
        {
            Debug.Log($"Equipando {itemName} - Daño: {damage}");
            // PlayerCombat.Instance.EquipWeapon(this);
        }
        #endregion
    }
}

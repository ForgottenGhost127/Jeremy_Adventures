using System;
using System.Xml.Linq;
using UnityEngine;
using static UnityEditor.Progress;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Fruit", menuName = "Inventory/Fruit")]
    public class Fruit : ScriptableObject
    {
        #region Properties
        [Header("Basic Properties")]
        public string itemName;
        public string description;
        public Sprite icon;
        public int maxStackSize = 10;

        [Header("Fruit Properties")]
        public FruitType fruitType;
        public int healAmount;
        public float attackBoostMultiplier = 1.5f;
        public float attackBoostDuration = 10f;
        public int sellValue;
        #endregion

        #region Public Methods
        public void Use()
        {
            switch (fruitType)
            {
                case FruitType.Healing:
                    HealPlayer();
                    break;
                case FruitType.AttackBoost:
                    BoostPlayerAttack();
                    break;
                case FruitType.Sellable:
                    Debug.Log($"{itemName} se puede vender por {sellValue} monedas");
                    break;
            }
        }
        #endregion

        #region Private Methods
        private void HealPlayer()
        {
            Debug.Log($"Curando {healAmount} puntos de vida con {itemName}");
            // PlayerHealth.Instance.Heal(healAmount);
        }

        private void BoostPlayerAttack()
        {
            Debug.Log($"Aumentando ataque x{attackBoostMultiplier} durante {attackBoostDuration} segundos");
            // PlayerCombat.Instance.ApplyAttackBoost(attackBoostMultiplier, attackBoostDuration);
        }
        #endregion
    }
}

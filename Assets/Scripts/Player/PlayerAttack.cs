using UnityEngine;
using System;
using Inventory;

public class PlayerAttack : MonoBehaviour
{
    #region Fields
    [Header("Attack Settings")]
    public Animator weaponAnimator;
    public float attackCooldown = 1f;

    private float lastAttackTime;
    #endregion

    #region Unity Callbacks
    void Update()
    {
        HandleAttackInput();
    }
    #endregion

    #region Public Methods
    public void PerformAttack()
    {
        if (CanAttack())
        {
            weaponAnimator.SetTrigger("Attack");
            int damage = PlayerBuffs.Instance.GetModifiedAttackDamage();
            Debug.Log($"¡Ataque! Daño: {damage}");

            lastAttackTime = Time.time;
        }
    }
    #endregion

    #region Private Methods
    private void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PerformAttack();
        }
    }

    private bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    private void DealDamage()
    {
        int damage = InventorySystem.Instance.GetCurrentWeaponDamage();
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            }
        }
    }
    #endregion

}

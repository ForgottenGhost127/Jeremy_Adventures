using UnityEngine;
using System;
using Inventory;

public class PlayerAttack : MonoBehaviour
{
    #region Fields
    [Header("Attack Settings")]
    public Animator weaponAnimator;
    public float attackCooldown = 1f;
    public float attackRange = 1.5f;

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
            DealDamage();
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
        int damage = PlayerBuffs.Instance.GetModifiedAttackDamage();
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                    Debug.Log($"¡Golpeaste al enemigo! Daño: {damage}");
                }
            }
        }
    }
    #endregion

}

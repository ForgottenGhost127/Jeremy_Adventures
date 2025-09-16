using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    #region Fields
    public int maxHealth = 3;
    private int currentHealth;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        currentHealth = maxHealth;
    }
    #endregion

    #region Public Methods
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    #endregion

    #region Private Methods
    private void Die()
    {
        Destroy(gameObject);
    }
    #endregion

}

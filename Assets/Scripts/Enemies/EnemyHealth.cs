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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Weapon"))
        {
            print("Damage");
            TakeDamage(10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        if (collision.gameObject.CompareTag("Weapon"))
        {
            print("Damage");
            TakeDamage(10);
        }
    }
    #endregion

    #region Public Methods
    public void TakeDamage(int damage)
    {
        print("Take Damage");
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
        print("Die");
        Destroy(gameObject);
    }
    #endregion

}

using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    #region Properties
    public int CurrentHealth => currentHealth;
    #endregion

    #region Fields
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Visual Feedback")]
    private SpriteRenderer spriteRenderer;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        Debug.Log($"Enemigo recibe {damage} de daño. Salud: {currentHealth - damage}/{maxHealth}");
        currentHealth -= damage;
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    #endregion

    #region Private Methods
    private void Die()
    {
        Debug.Log("Enemigo eliminado");
        Destroy(gameObject);
    }

    private System.Collections.IEnumerator FlashRed()
    {
        Color original = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = original;
    }
    #endregion

}

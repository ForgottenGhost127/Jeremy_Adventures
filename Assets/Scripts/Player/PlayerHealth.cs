using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    #region Properties
    public float CurrentHealth { get; private set; }
    #endregion

    #region Fields
    [Header("Health Settings")]
    [SerializeField] private float _maxHealth = 100f;

    [Header("UI")]
    [SerializeField] private GameObject _gameOverPanel;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        CurrentHealth = _maxHealth;

        if (_gameOverPanel != null)
            _gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // Prueba opcional para restar vida:
        // if (Input.GetKeyDown(KeyCode.Space)) TakeDamage(20f);
    }
    #endregion

    #region Public Methods
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);

        if (CurrentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
    }
    #endregion

    #region Private Methods
    private void Die()
    {
        Debug.Log("El jugador ha muerto");
        if (_gameOverPanel != null)
            _gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }
    #endregion

}

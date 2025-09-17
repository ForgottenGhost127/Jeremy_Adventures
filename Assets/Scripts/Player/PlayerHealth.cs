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

    public static event Action<float, float> OnHealthChanged;
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
        
    }
    #endregion

    #region Public Methods
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, _maxHealth);

        if (CurrentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, _maxHealth);
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

using Inventory;
using System;
using System.Collections;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    #region Properties
    public static PlayerBuffs Instance { get; private set; }
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool HasAttackBoost => attackBoostActive;
    public float CurrentAttackMultiplier => attackBoostActive ? attackMultiplier : 1f;
    #endregion

    #region Fields
    [Header("Health System")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    [Header("Attack Buff System")]
    private bool attackBoostActive = false;
    private float attackMultiplier = 1f;
    private Coroutine attackBoostCoroutine;

    public static event Action<int, int> OnHealthChanged;
    public static event Action<bool> OnAttackBoostChanged;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    #endregion

    #region Public Methods
    public void Heal(int amount)
    {
        int oldHealth = currentHealth;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        if (currentHealth != oldHealth)
        {
            Debug.Log($"Curado! +{amount} HP. Salud: {currentHealth}/{maxHealth}");
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            ShowHealingMessage(amount);
        }
    }

    public void TakeDamage(int amount)
    {
        int oldHealth = currentHealth;
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        if (currentHealth != oldHealth)
        {
            Debug.Log($"Daño recibido! -{amount} HP. Salud: {currentHealth}/{maxHealth}");
            OnHealthChanged?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void ApplyAttackBoost(float multiplier, float duration)
    {
        if (attackBoostCoroutine != null)
        {
            StopCoroutine(attackBoostCoroutine);
        }

        attackMultiplier = multiplier;
        attackBoostActive = true;

        Debug.Log($"¡Buff de ataque activado! x{multiplier} durante {duration} segundos");
        OnAttackBoostChanged?.Invoke(true);

        attackBoostCoroutine = StartCoroutine(AttackBoostTimer(duration));
        UIController uiController = FindObjectOfType<UIController>();
        if (uiController != null)
        {
            uiController.ShowCoconutMessage();
        }
    }

    public int GetModifiedAttackDamage()
    {
        int baseDamage = InventorySystem.Instance?.GetCurrentWeaponDamage() ?? 0;
        return Mathf.RoundToInt(baseDamage * CurrentAttackMultiplier);
    }
    #endregion

    #region Private Methods
    private IEnumerator AttackBoostTimer(float duration)
    {
        yield return new WaitForSeconds(duration);

        attackBoostActive = false;
        attackMultiplier = 1f;

        Debug.Log("Buff de ataque terminado");
        OnAttackBoostChanged?.Invoke(false);

        attackBoostCoroutine = null;
    }
    private void ShowHealingMessage(int amount)
    {
        UIController uiController = FindObjectOfType<UIController>();
        if (uiController != null)
        {
            if (amount == 10)
            {
                uiController.ShowAppleMessage();
            }
            else if (amount == 30)
            {
                uiController.ShowAvocadoMessage();
            }
        }
    }

    private void Die()
    {
        Debug.Log("¡Player ha muerto!");
    }
    #endregion

}

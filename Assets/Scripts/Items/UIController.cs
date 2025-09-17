using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIController : MonoBehaviour
{
	#region Fields
	[SerializeField] private PlayerCoins _playerCoins;
	[SerializeField] private PlayerBuffs _playerBuffs;
	[SerializeField] private Slider _healthSlider;
	[SerializeField] private TextMeshProUGUI _collectedCoins;

    [Header("Fruit Feedback Messages")]
    [SerializeField] private TextMeshProUGUI appleMessage;
    [SerializeField] private TextMeshProUGUI avocadoMessage;
    [SerializeField] private TextMeshProUGUI coconutMessage;
    [SerializeField] private float messageDuration = 2f;

    [Header("Death Panel")]
    [SerializeField] private GameObject deathPanel;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        if (_healthSlider != null && _playerBuffs != null)
        {
            _healthSlider.maxValue = _playerBuffs.MaxHealth;
            _healthSlider.value = _playerBuffs.CurrentHealth;
        }

        PlayerHealth.OnHealthChanged += UpdateHealthSlider;
        PlayerBuffs.OnHealthChanged += OnHealthChanged;
        PlayerBuffs.OnAttackBoostChanged += OnAttackBoostChanged;
        

        HideAllMessages();
        if (deathPanel != null)
            deathPanel.SetActive(false);
    }

    void Update()
    {
		_collectedCoins.text = _playerCoins.ToString();
    }
    private void OnDestroy()
    {
        PlayerBuffs.OnHealthChanged -= OnHealthChanged;
        PlayerBuffs.OnAttackBoostChanged -= OnAttackBoostChanged;
       
        PlayerHealth.OnHealthChanged -= UpdateHealthSlider;
    }
    #endregion

    #region Public Methods
    public void ShowAppleMessage()
    {
        ShowMessage(appleMessage, $"Apple used! +10 HP");
    }

    public void ShowAvocadoMessage()
    {
        ShowMessage(avocadoMessage, $"Avocado used! +30 HP");
    }

    public void ShowCoconutMessage()
    {
        ShowMessage(coconutMessage, "Coconut used! Attack x2 for 20s");
    }
    #endregion

    #region Private Methods
    private void OnHealthChanged(int currentHealth, int maxHealth)
    {
        if (_healthSlider != null)
        {
            _healthSlider.value = currentHealth;
        }
    }

    private void OnAttackBoostChanged(bool isActive)
    {
        if (isActive)
        {
            Debug.Log("UI: Buff visual activado");
        }
        else
        {
            Debug.Log("UI: Buff visual desactivado");
        }
    }
    private void OnPlayerDied()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    private void ShowMessage(TextMeshProUGUI messageText, string text)
    {
        if (messageText != null)
        {
            messageText.text = text;
            messageText.gameObject.SetActive(true);
            StartCoroutine(HideMessageAfterDelay(messageText));
        }
    }

    private void HideAllMessages()
    {
        if (appleMessage != null) appleMessage.gameObject.SetActive(false);
        if (avocadoMessage != null) avocadoMessage.gameObject.SetActive(false);
        if (coconutMessage != null) coconutMessage.gameObject.SetActive(false);
    }
    private void UpdateHealthSlider(float current, float max)
    {
        if (_healthSlider != null)
        {
            _healthSlider.maxValue = max;
            _healthSlider.value = current;
        }
    }
    private IEnumerator HideMessageAfterDelay(TextMeshProUGUI messageText)
    {
        yield return new WaitForSeconds(messageDuration);
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }
    #endregion

}

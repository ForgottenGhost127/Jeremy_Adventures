using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private PlayerCoins _playerCoins;
	[SerializeField] private PlayerHealth _playerHealth;
	[SerializeField] private Slider _healthSlider;
	[SerializeField] private TextMeshProUGUI _collectedCoins;

	#endregion

	#region Unity Callbacks
	void Start()
    {
        
    }

    void Update()
    {
		//_healthSlider.value = _playerHealth.CurrentHealth;
		_collectedCoins.text = _playerCoins.ToString();
    }
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
   
}

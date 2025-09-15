using Inventory;
using System;
using UnityEngine;

public class WeaponVisualizer : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [Header("Weapon Display")]
    public SpriteRenderer weaponSpriteRenderer;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        UpdateWeaponVisual();
    }
    void Update()
    {
        UpdateWeaponVisual();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void UpdateWeaponVisual()
    {
        if (InventorySystem.Instance?.currentWeapon != null)
        {
            weaponSpriteRenderer.sprite = InventorySystem.Instance.currentWeapon.icon;
            weaponSpriteRenderer.gameObject.SetActive(true);
        }
        else
        {
            weaponSpriteRenderer.gameObject.SetActive(false);
        }
    }
    #endregion

}

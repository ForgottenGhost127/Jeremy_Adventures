using UnityEngine;
using System;

public class RanaNPC : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [Header("UI de la tienda")]
    [SerializeField] private GameObject tiendaUI;

    private bool jugadorCerca = false;
    #endregion

    #region Unity Callbacks
    private void Start()
    {

    }

    private void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            AbrirTienda();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            CerrarTienda();
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void AbrirTienda()
    {
        if (tiendaUI != null)
            tiendaUI.SetActive(true);
    }

    private void CerrarTienda()
    {
        if (tiendaUI != null)
            tiendaUI.SetActive(false);
    }
    #endregion

}

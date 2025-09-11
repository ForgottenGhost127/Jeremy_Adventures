using UnityEngine;
using System;

public class Coins : MonoBehaviour
{
    [Header("Coin Settings")]
    public int value = 1; 
    public AudioClip pickupSFX; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerCoins.instance.AddCoins(value);

            if (pickupSFX != null)
            {
                AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
            }

            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGetCoin : MonoBehaviour
{
    private KartController kartController;

    private void Awake()
    {
        kartController = GetComponentInParent<KartController>();    
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Coin"))
        {
            kartController.CoinBoost();

            Coin coin = collision.GetComponent<Coin>();

            coin.Hide();
        }
    }
}

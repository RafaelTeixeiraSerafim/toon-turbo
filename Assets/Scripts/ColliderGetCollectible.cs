using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderGetCollectible : MonoBehaviour
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

        if (collision.TryGetComponent(out CheckpointSingle checkpointSingle) && checkpointSingle.PlayerToCollide == kartController.ActualPlayer)
        {
            if (checkpointSingle.Index == kartController.CheckpointsCollected || checkpointSingle.isFinalCheckpoint)
            {
                kartController.CollectCheckpoint();
                checkpointSingle.Hide();
            }
            else
            {
                Debug.Log("Wrong Way");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    MeshCollider meshCollider;
    MeshRenderer meshRenderer;

    private readonly float respawnTime = 5f;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Hide()
    {
        meshCollider.enabled = false;
        meshRenderer.enabled = false;

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);

            meshCollider.enabled = true;
            meshRenderer.enabled = true;

            break;
        }
    }
}

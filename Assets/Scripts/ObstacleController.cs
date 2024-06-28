using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    Collider _collider;
    MeshRenderer meshRenderer;

    [SerializeField] private readonly float respawnTime = 5f;

    void Start()
    {
        _collider = GetComponent<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Hide()
    {
        _collider.enabled = false;
        meshRenderer.enabled = false;

        Debug.Log("esconder");

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);

            Debug.Log("terminou");
            _collider.enabled = true;
            meshRenderer.enabled = true;

            break;
        }
    }
}
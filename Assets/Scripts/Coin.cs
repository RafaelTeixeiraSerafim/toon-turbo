using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject visuals;
    private float speed = 50.0f;

    private readonly float respawnTime = 5f;

    public void FixedUpdate()
    {
        visuals.transform.rotation = Quaternion.Euler(visuals.transform.rotation.eulerAngles + new Vector3(0, -speed * Time.deltaTime, 0));
    }

    public void Hide()
    {
        boxCollider.enabled = false;
        visuals.SetActive(false);

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);

            boxCollider.enabled = true;
            visuals.SetActive(true);

            break;
        }
    }
}

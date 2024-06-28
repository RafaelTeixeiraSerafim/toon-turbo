using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartObstacleController : MonoBehaviour
{
    private KartController kartController;
    private ObstacleController boxController;
    private Rigidbody rb;
    public float slowdownFactor = 0.8f;
    public float targetSpeed = 10f;

    private void Awake()
    {
        kartController = GetComponentInParent<KartController>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(HitAnimation());
            boxController = collision.gameObject.GetComponent<ObstacleController>();
            boxController.Hide();
        }
    }

    private IEnumerator HitAnimation()
    {
        kartController.allowDrive = 0;

        while (rb.velocity.magnitude > targetSpeed)
        {
            rb.velocity *= slowdownFactor;
            yield return null; // Wait for one frame
        }

        yield return new WaitForSeconds(0);
        kartController.allowDrive = 1;
    }
}

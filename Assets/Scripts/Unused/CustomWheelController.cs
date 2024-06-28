using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomWheelController : MonoBehaviour
{
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private LayerMask groundLayer;
    private float suspensionRestDist;
    public float springStrength = 100;
    public float springDamper = 30;

    void Start()
    {
        suspensionRestDist = transform.localScale.y;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, .0001f, groundLayer))
        {
            Debug.Log(hit.ToString());
            Vector3 springDir = transform.up;

            Vector3 tireWorldVel = carRB.GetPointVelocity(transform.position);

            float offset = suspensionRestDist - hit.distance;

            float vel = Vector3.Dot(springDir, tireWorldVel);

            float force = (offset * springStrength) - (vel * springDamper);

            carRB.AddForceAtPosition(springDir * force, transform.position);
        }
    }
}

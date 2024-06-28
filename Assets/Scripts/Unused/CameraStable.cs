using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStable : MonoBehaviour
{
    [SerializeField] private Transform vehicle;
    public Vector3 offset;
    public float rotationSpeed = 10f;
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        HandleTranslation();
        HandleRotation();
    }

    private void HandleTranslation()
    {
        Vector3 targetPosition = vehicle.TransformPoint(offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void HandleRotation()
    {
        Vector3 pos = new (vehicle.position.x - 0.35f, vehicle.position.y, vehicle.position.z);
        Vector3 direction = pos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
    }
}

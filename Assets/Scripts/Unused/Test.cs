using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject vehicle;
    private GameObject fixPoint;
    private Vector3 original_offset;
    private float original_rotation;
    private Vector3 offset;
    private float angle;

    void Start()
    {
        fixPoint = vehicle.transform.Find("Camera Point").gameObject;

        original_offset = fixPoint.transform.position - transform.position;
        Debug.Log(original_offset);

        offset.y = original_offset.y;
    }

    void Update()
    {
        angle = Mathf.Clamp(vehicle.transform.eulerAngles.y, 0f, 360f);

        if (angle >= 90 && angle <= 270)
        {
            offset.x = Mathf.Lerp(-4, 4, Mathf.InverseLerp(90, 270, angle));
        }
        else if (angle < 90)
        {
            offset.x = Mathf.Lerp(0, -4, Mathf.InverseLerp(0, 90, angle));
        }
        else
        {
            offset.x = Mathf.Lerp(4, 0, Mathf.InverseLerp(270, 360, angle));
        }

        if (angle >= 0 && angle <= 180)
        {
            offset.z = Mathf.Lerp(-4, 4, Mathf.InverseLerp(0, 180, angle));
        }
        else if (angle > 270)
        {
            offset.z = Mathf.Lerp(0, -4, Mathf.InverseLerp(270, 360, angle));
        }
        else
        {
            offset.z = Mathf.Lerp(4, 0, Mathf.InverseLerp(180, 270, angle));
        }



        transform.position = fixPoint.transform.position - offset;
        transform.LookAt(fixPoint.transform.position);
    }
}

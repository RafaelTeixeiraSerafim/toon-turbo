using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSpriteFreezeRotation : MonoBehaviour
{
    Quaternion originalRotation;
    void Start()
    {
        originalRotation = transform.rotation;
    }
    void Update()
    {
        transform.rotation = originalRotation;
    }
}

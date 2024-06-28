using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacao : MonoBehaviour
{
    public Vector3 axisRotate; //eixo de rotação
    public float speed;

    void Update()
    {
            transform.Rotate(axisRotate * speed * Time.deltaTime);
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFlash : MonoBehaviour
{
    public Color Color1;
    public Color Color2;
    [SerializeField] TextMeshProUGUI TextWoble;

    void Update()
    {
        FlashingText();
    }

    void FlashingText()
    {
        TextWoble.color = Color.Lerp(Color1, Color2, Mathf.PingPong(Time.time, 1));
    }
}

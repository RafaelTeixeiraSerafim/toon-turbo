using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSom : MonoBehaviour
{
    private bool estadoSom = true;
    [SerializeField] private AudioSource fundoMusical;

    [SerializeField] private Sprite Ligado;
    [SerializeField] private Sprite Desligado;

    [SerializeField] private Image muteImage;
    private float prevVolume;

    public void OnOff()
    {
        if (estadoSom)
        {
            estadoSom = false;
            prevVolume = fundoMusical.volume;
            fundoMusical.volume = 0;
            muteImage.sprite = Desligado;
        }
        else
        {
            estadoSom = true;
            fundoMusical.volume = prevVolume;
            muteImage.sprite = Ligado;
        }
    }

    public void VolumeMusical(float value)
    {
        fundoMusical.volume = value / 10;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LapFinishUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lapText;
    [SerializeField] private TextMeshProUGUI VictoryTextP1;
    [SerializeField] private TextMeshProUGUI DefeatTextP1;
    [SerializeField] private TextMeshProUGUI VictoryTextP2;
    [SerializeField] private TextMeshProUGUI DefeatTextP2;
    [SerializeField] private TextMeshProUGUI Timer;

    private int lap = 0;
    private float startTime;

    void Start()
    {
        VictoryTextP1.gameObject.SetActive(false);
        DefeatTextP1.gameObject.SetActive(false);

        VictoryTextP2.gameObject.SetActive(false);
        DefeatTextP2.gameObject.SetActive(false);

        lapText.text = $"{lap}/3";
        startTime = Time.time;
    }

    
    public void UpdateLapText(int currentLap)
    {
        lap = currentLap;
        lapText.text = $"{lap}/3";
    }

    
    public void ShowVictoryText(int player)
    {
        if (player == 1)
        {
            VictoryTextP1.gameObject.SetActive(true);
        }
        else if (player == 2)
        {
            VictoryTextP2.gameObject.SetActive(true);
        }
    }

    
    public void ShowDefeatText(int player)
    {
        if (player == 1)
        {
            DefeatTextP1.gameObject.SetActive(true);
        }
        else if (player == 2)
        {
            DefeatTextP2.gameObject.SetActive(true);
        }
    }
}
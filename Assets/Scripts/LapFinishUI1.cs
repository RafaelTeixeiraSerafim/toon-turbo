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

    // Método para atualizar o texto da volta
    public void UpdateLapText(int currentLap)
    {
        lap = currentLap;
        lapText.text = $"{lap}/3";
    }

    private IEnumerator FinishRace()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
     //   trackCheckpoints.OnPlayerFinishLap -= TrackCheckpoints_OnPlayerFinishLap;
    }
}

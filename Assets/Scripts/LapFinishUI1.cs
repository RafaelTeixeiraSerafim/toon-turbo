using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LapFinishUI : MonoBehaviour
{
    [SerializeField] private  TrackCheckpoints trackCheckpoints;
    [SerializeField] private  TextMeshProUGUI lapText;
    [SerializeField] private  TextMeshProUGUI VictoryText;  
    [SerializeField] private  TextMeshProUGUI DefeatText;  
    [SerializeField] private  TextMeshProUGUI Timer;       

    private int lap = 0;
    private float startTime; 

    void Start()
    {
        
        VictoryText.gameObject.SetActive(false);
        DefeatText.gameObject.SetActive(false);

       // trackCheckpoints.OnPlayerFinishLap += TrackCheckpoints_OnPlayerFinishLap;
        lapText.text = $"{lap}/3";
        startTime = Time.time;             
        Debug.Log("0"); 
        
    }

    private void TrackCheckpoints_OnPlayerFinishLap(object sender, System.EventArgs e)
    {
        lap++;
        lapText.text = $"{lap}/3";
        if (lap == 3)
        {
            float raceTime = Time.time - startTime; // Calculate race time in seconds
            Debug.Log(raceTime);
            if (raceTime < 180)
            {
                VictoryText.gameObject.SetActive(true);
            }
            else
            {
                DefeatText.gameObject.SetActive(true);
            }

            StartCoroutine(FinishRace());
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    //Text
    public TextMeshProUGUI hourText;
    public TextMeshProUGUI minuteText;

    // Time Variables
    private int rnrMaxHour = 20; // Maximum hour limit
    private int rnrMaxMin = 60; // Maximum minute limit

    void Start()
    {
        if (PlayerStats.rnrHourDisplay >= 10)
        {
            minuteText.text = PlayerStats.rnrMinuteDisplay.ToString();
        }
        else
        {
            hourText.text = "0" + PlayerStats.rnrHourDisplay.ToString();
        }
        
        if (PlayerStats.rnrMinuteDisplay > 0 )
        {
            minuteText.text = PlayerStats.rnrMinuteDisplay.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStats.rnrTimer += Time.deltaTime;

        // Increment time every 1 second
        if (PlayerStats.rnrTimer >= 10f)
        {
            IncrementTime();
            PlayerStats.rnrTimer = 0f; // Reset the timer
        }
    }

    void IncrementTime()
    {
        PlayerStats.rnrMinuteDisplay += 10; // Increment minutes
        minuteText.text = PlayerStats.rnrMinuteDisplay.ToString();

        if (PlayerStats.rnrMinuteDisplay >= rnrMaxMin)
        {
            PlayerStats.rnrMinuteDisplay = 0; // Reset minutes
            minuteText.text = "00";
            PlayerStats.rnrHourDisplay++; // Increment hours
            if (PlayerStats.rnrHourDisplay >= 10)
            {
                hourText.text = PlayerStats.rnrHourDisplay.ToString();
            }
            else
            {
                hourText.text = "0" + PlayerStats.rnrHourDisplay.ToString();
            }  

            if (PlayerStats.rnrHourDisplay >= rnrMaxHour)
            {
                PlayerStats.rnrHourDisplay = 6; // Reset hours to start value
                hourText.text = PlayerStats.rnrHourDisplay.ToString();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    // Text
    public TextMeshProUGUI hourText; // Text to tell the hour
    public TextMeshProUGUI minuteText; // Text to tell the minute
    public TextMeshProUGUI ampmText; // Text to tell if AM or PM

    // Time Variables
    private int rnrMaxMin = 60; // Maximum minute limit
    private bool isTimeStopped = false; // Flag to stop time when it hits 12:00 AM

    void Start()
    {
        UpdateTimeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeStopped)
            return; // Stop updating when time has stopped

        if (PlayerStats.rnrTimer >= 1f)
        {
            IncrementTime();
            PlayerStats.rnrTimer = 0f; // Reset the timer
        }
        else
        {
            PlayerStats.rnrTimer += Time.deltaTime;
        }
    }

    void IncrementTime()
    {
        if (PlayerStats.rnrHourDisplay == 12 &&
            PlayerStats.rnrMinuteDisplay == 0 &&
            PlayerStats.rnrDay == "AM")
        {
            isTimeStopped = true;
            return;
        }

        PlayerStats.rnrMinuteDisplay += 10; // Increment minutes

        if (PlayerStats.rnrMinuteDisplay >= rnrMaxMin)
        {
            PlayerStats.rnrMinuteDisplay = 0; // Reset minutes
            PlayerStats.rnrHourDisplay++; // Increment hours

            if (PlayerStats.rnrHourDisplay == 12)
            {
                // Toggle AM/PM at 12
                PlayerStats.rnrDay = PlayerStats.rnrDay == "AM" ? "PM" : "AM";
            }
            else if (PlayerStats.rnrHourDisplay > 12)
            {
                // Reset hour to 1 after 12 PM
                PlayerStats.rnrHourDisplay = 1;
            }
        }

        UpdateTimeDisplay();
    }

    void UpdateTimeDisplay()
    {
        // Update hour text with leading zero if necessary
        hourText.text = PlayerStats.rnrHourDisplay < 10
            ? "0" + PlayerStats.rnrHourDisplay.ToString()
            : PlayerStats.rnrHourDisplay.ToString();

        // Update minute text with leading zero if necessary
        minuteText.text = PlayerStats.rnrMinuteDisplay < 10
            ? "0" + PlayerStats.rnrMinuteDisplay.ToString()
            : PlayerStats.rnrMinuteDisplay.ToString();

        // Update AM/PM text
        ampmText.text = PlayerStats.rnrDay;
    }
}

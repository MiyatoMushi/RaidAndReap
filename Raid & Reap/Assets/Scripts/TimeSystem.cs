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
    public int rnrHourDisplay = 6; // Starting hour
    public int rnrMinuteDisplay = 0; // Starting minute

    private int rnrMaxHour = 20; // Maximum hour limit
    private int rnrMaxMin = 60; // Maximum minute limit
    private float rnrTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        rnrTimer += Time.deltaTime;

        // Increment time every 1 second
        if (rnrTimer >= 10f)
        {
            IncrementTime();
            rnrTimer = 0f; // Reset the timer
        }
    }

    void IncrementTime()
    {
        rnrMinuteDisplay += 10; // Increment minutes
        minuteText.text = rnrMinuteDisplay.ToString();

        if (rnrMinuteDisplay >= rnrMaxMin)
        {
            rnrMinuteDisplay = 0; // Reset minutes
            minuteText.text = "00";
            rnrHourDisplay++; // Increment hours
            hourText.text = rnrHourDisplay.ToString();

            if (rnrHourDisplay >= rnrMaxHour)
            {
                rnrHourDisplay = 6; // Reset hours to start value
                hourText.text = rnrHourDisplay.ToString();
            }
        }
    }
}

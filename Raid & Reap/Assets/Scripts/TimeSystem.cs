using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    // References to UI elements for displaying time
    public TextMeshProUGUI hourText; // Displays the current hour
    public TextMeshProUGUI minuteText; // Displays the current minute
    public TextMeshProUGUI ampmText; // Displays "AM" or "PM"

    // Time Variables
    private int rnrMaxMin = 60; // Maximum value for minutes (60 minutes in an hour)
    private bool isTimeStopped = false; // Flag to stop time updates when it hits 12:00 AM

    void Start()
    {
        // Initialize the time display when the script starts
        UpdateTimeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        // Stop updating if the time has been flagged to stop
        if (isTimeStopped)
            return;

        // Increment the timer by the time passed since the last frame
        if (PlayerStats.rnrTimer >= 1f)
        {
            // Once 1 second has passed, update the time
            IncrementTime();
            PlayerStats.rnrTimer = 0f; // Reset the timer
        }
        else
        {
            // Accumulate time since the last update
            PlayerStats.rnrTimer += Time.deltaTime;
        }
    }

    // Method to increment the time
    void IncrementTime()
    {
        // Check if the time has reached 12:00 AM
        if (PlayerStats.rnrHourDisplay == 12 &&
            PlayerStats.rnrMinuteDisplay == 0 &&
            PlayerStats.rnrDay == "AM")
        {
            isTimeStopped = true; // Stop the clock
            return; // Exit the method
        }

        // Increment the minute value by 10
        PlayerStats.rnrMinuteDisplay += 10;

        // Check if minutes have reached or exceeded 60
        if (PlayerStats.rnrMinuteDisplay >= rnrMaxMin)
        {
            PlayerStats.rnrMinuteDisplay = 0; // Reset minutes to 0
            PlayerStats.rnrHourDisplay++; // Increment the hour

            // If the hour reaches 12, toggle AM/PM
            if (PlayerStats.rnrHourDisplay == 12)
            {
                PlayerStats.rnrDay = PlayerStats.rnrDay == "AM" ? "PM" : "AM"; // Toggle AM/PM
            }
            // If the hour exceeds 12, reset to 1 (for 12-hour format)
            else if (PlayerStats.rnrHourDisplay > 12)
            {
                PlayerStats.rnrHourDisplay = 1; // Reset hour to 1
            }
        }

        // Update the displayed time on the screen
        UpdateTimeDisplay();
    }

    // Method to update the displayed time
    void UpdateTimeDisplay()
    {
        // Format and display the hour with a leading zero if it's less than 10
        hourText.text = PlayerStats.rnrHourDisplay < 10
            ? "0" + PlayerStats.rnrHourDisplay.ToString() // Add leading zero
            : PlayerStats.rnrHourDisplay.ToString(); // Display as-is

        // Format and display the minute with a leading zero if it's less than 10
        minuteText.text = PlayerStats.rnrMinuteDisplay < 10
            ? "0" + PlayerStats.rnrMinuteDisplay.ToString() // Add leading zero
            : PlayerStats.rnrMinuteDisplay.ToString(); // Display as-is

        // Display the current period (AM/PM)
        ampmText.text = PlayerStats.rnrDay;
    }
}

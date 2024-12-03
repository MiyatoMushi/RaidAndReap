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
    public TextMeshProUGUI dayText; // Displays the current day of the week
    public TextMeshProUGUI weekText; // Displays the current week number

    // Time Variables
    private int rnrMaxMin = 60; // Maximum value for minutes (60 minutes in an hour)
    private float stopTimeDuration = 3f; // Duration to stop time (in seconds)
    private bool isTimeStopped = false; // Flag to stop time updates
    private float stopTimeTimer = 0f; // Timer to track stop time duration

    // Week and Day Variables
    private int currentDayIndex = 0; // Index for days of the week
    private int currentWeek = 1; // Current week number

    private readonly string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

    void Start()
    {
        // Initialize the time display when the script starts
        UpdateTimeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        // If time is stopped, track the stop time duration
        if (isTimeStopped)
        {
            stopTimeTimer += Time.deltaTime;

            // Once 3 seconds have passed, resume the time
            if (stopTimeTimer >= stopTimeDuration)
            {
                isTimeStopped = false;
                stopTimeTimer = 0f;

                // Reset time to 6:00 AM
                PlayerStats.rnrHourDisplay = 6;
                PlayerStats.rnrMinuteDisplay = 0;
                PlayerStats.rnrDay = "AM"; // Set to AM
                IncrementDayAndWeek(); // Call to update day and week
            }
            return;
        }

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

            // Stop time when it reaches 2:00 AM
            if (PlayerStats.rnrHourDisplay == 2 && PlayerStats.rnrMinuteDisplay == 0 && PlayerStats.rnrDay == "AM")
            {
                isTimeStopped = true; // Stop the time for 3 seconds
            }
        }

        // Update the displayed time on the screen
        UpdateTimeDisplay();
    }

    // Method to increment the day and week
    void IncrementDayAndWeek()
    {
        // Increment the day
        currentDayIndex++; // Move to the next day

        // Check if the week has completed
        if (currentDayIndex >= daysOfWeek.Length)
        {
            currentDayIndex = 0; // Reset to Monday
            currentWeek++; // Increment the week
        }

        Debug.Log($"Day: {daysOfWeek[currentDayIndex]}, Week: {currentWeek}");

        // Update the displayed time for the new day and week
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

        // Display the current day of the week
        dayText.text = daysOfWeek[currentDayIndex];

        // Display the current week number
        weekText.text = "Week " + currentWeek;
    }
}
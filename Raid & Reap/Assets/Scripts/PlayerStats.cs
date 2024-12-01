using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerStats
{

    public static int PlayerHealth = 100;
    public static Vector3 playerStartingPosition = new Vector3(-2.0309999f,4.48099995f,0);
    public static bool slimeIsMoving = false;
    public static bool boarIsMoving = false;


    //Player Time
    public static int rnrHourDisplay = 6;
    public static int rnrMinuteDisplay = 0;
    public static float rnrTimer = 0f;
    public static string rnrDay = "AM";


    public static void PlayerResets(){
        PlayerHealth = 100;
    }
}

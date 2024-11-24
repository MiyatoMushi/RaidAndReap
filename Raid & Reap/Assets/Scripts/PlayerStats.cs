using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{

    public static int PlayerHealth = 100;
    public static Vector3 playerStartingPosition = new Vector3(-2.0309999f,4.48099995f,0);
    public static bool slimeIsMoving = false;
    public static bool boarIsMoving = false;


    public static void PlayerResets(){
        PlayerHealth = 100;
    }

}

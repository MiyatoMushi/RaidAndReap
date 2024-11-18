using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{

    public static int PlayerHealth = 100;
    public static Vector3 playerStartingPosition = new Vector3(-2.53600001f,4.49499989f,1f);

    public static void PlayerResets(){
        PlayerHealth = 100;
    }

}

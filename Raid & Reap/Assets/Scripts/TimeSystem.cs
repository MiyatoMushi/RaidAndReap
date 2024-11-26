using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    //Text
    //public TextMeshProUGUI rnrHourDisplay;
    //public TextMeshProUGUI rnrMinuteDisplay;

    //Base Time Variables - JC
    public int rnrHourDisplay = 6;
    public int rnrMinuteDisplay = 0;

    private int rnrMaxHour = 20;
    private int rnrMaxMin = 60;
    private float rnrTimer = 0f;

    /* Start is called before the first frame update
    void Start()
    {
        
    }
    */

    // Update is called once per frame
    void Update()
    {
        rnrTimer += Time.deltaTime;

        if (rnrTimer < rnrMaxMin) {
            IncreementTime();
        } else if (rnrTimer == 61) {
            rnrTimer = 0;
        }
    }

    void IncreementTime() {
        if (rnrHourDisplay < rnrMaxHour) {
            if (rnrMinuteDisplay < rnrMaxMin) {
                rnrMinuteDisplay ++;
            }
            else if (rnrMinuteDisplay == rnrMaxHour) {
                rnrMinuteDisplay = 0;
                rnrHourDisplay++;
            }
        } else if (rnrHourDisplay == rnrMaxHour) {
            rnrHourDisplay = 6;
        }
    }
}

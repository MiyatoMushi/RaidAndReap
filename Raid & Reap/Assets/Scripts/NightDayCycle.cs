using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightDayCycle : MonoBehaviour
{
    public SpriteRenderer cycleSprite;
    public Color dayColor = new Color(1f, 1f, 1f, 0f); // Transparent during day
    public Color duskColor = new Color(1f, 0.5f, 0f, 0.5f); // Orange and semi-transparent at dusk
    public Color nightColor = new Color(0f, 0f, 0f, 0.8f); // Dark and nearly opaque at night

    void Start(){
        cycleSprite.color = dayColor;
    }
    void Update()
    {
        if (PlayerStats.rnrHourDisplay == 4 && PlayerStats.rnrDay == "PM")
        {
            // Instantly set color to dusk
            cycleSprite.color = duskColor;
        }
        else if (PlayerStats.rnrHourDisplay == 6 && PlayerStats.rnrDay == "PM")
        {
            // Instantly set color to night
            cycleSprite.color = nightColor;
        }
    }
}

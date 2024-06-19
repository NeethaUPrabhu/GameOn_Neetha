using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockUpdate : MonoBehaviour
{
    private const float REAL_SECONDS_PER_TIMEGAME_DAY = 5f;
    private float day;
    public TMP_Text clockText;
    public string hourString;
   
    public string minuteString;

    private void Update()
    {

        int hour = System.DateTime.Now.Hour;
        int minutes = System.DateTime.Now.Minute;
        
        hourString = hour.ToString("00");
        minuteString = minutes.ToString("00");  
        
        clockText.text= $"Time:" + hourString + ":" + minuteString;
    
    }
}

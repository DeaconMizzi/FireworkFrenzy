using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float secondsCount;
    private int minuteCount;

    void Update()
    {
        UpdateTimerUI();
    }

    //call this on update
    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        timerText.text = minuteCount + "m:" + (int)secondsCount + "s";
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        else if (minuteCount >= 60)
        {
            minuteCount = 0;
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private Transform hoursPivot, minutesPivot, secondsPivot;

    private const int hoursToDegrees = -30, minutesToDegrees = -6, secondsToDegrees = -6;
    void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hoursPivot.localRotation =
            Quaternion.Euler(0f, 0f, hoursToDegrees * (float) time.TotalHours);
        minutesPivot.localRotation =
            Quaternion.Euler(0f, 0f, minutesToDegrees * (float) time.TotalMinutes);
        secondsPivot.localRotation =
            Quaternion.Euler(0f, 0f, secondsToDegrees * (float)  time.TotalSeconds);
    }
}

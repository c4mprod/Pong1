using UnityEngine;
using System.Collections.Generic;

public static class GeneralHelpers
{
    public static string FloatToTimeString(this float _Time)
    {
        int lMinutes = Mathf.FloorToInt(_Time / 60.0f);
        int lSeconds = Mathf.FloorToInt(_Time - lMinutes * 60);

        string lFormatedTime = string.Format("{0:0}:{1:00}", lMinutes, lSeconds);

        return (lFormatedTime);
    }
}

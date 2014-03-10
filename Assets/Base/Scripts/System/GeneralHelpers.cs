using UnityEngine;
using System.Collections.Generic;

public static class GeneralHelpers
{
    public static string FloatToTimeString(this float _Time)
    {
        int lMinutes = Mathf.FloorToInt(_Time / 60.0f);
        int lSeconds = Mathf.FloorToInt(_Time - lMinutes * 60);

        string lFormatedTime = string.Format("{0:0}:{1:00}", lMinutes, lSeconds);

        return lFormatedTime;
    }

    public static string Last(this Dictionary<string, KeyCode> _Dic)
    {
        string lLast = "";
        string[] lKeys = new string[_Dic.Keys.Count];
        _Dic.Keys.CopyTo(lKeys, 0);

        foreach (string lKey in lKeys)
        {
            lLast = lKey;
        }

        return lLast;
    }
}

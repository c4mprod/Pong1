using UnityEngine;
using System.Collections.Generic;

public static class GeneralHelpers
{
    public static int CalculateCollectionPositions(out int _PreviousPos, out int _NextPos, int _CurrentPos,
        int _CollectionSize)
    {
        if ((_CollectionSize - 1) - _CurrentPos < 0)
            _CurrentPos = 0;
        if (_CurrentPos < 0)
            _CurrentPos = _CollectionSize - 1;

        _PreviousPos = _CurrentPos - 1;
        _NextPos = _CurrentPos + 1;

        if (_PreviousPos < 0)
            _PreviousPos = _CollectionSize - 1;
        if ((_CollectionSize - 1) - _NextPos < 0)
            _NextPos = 0;

        return (_CurrentPos);
    }

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

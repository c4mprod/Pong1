using UnityEngine;
using System.Collections.Generic;

public class PlayerDatas
{
    private float m_DataScore;
    public SingleRacketDatas m_RacketDatas = null;

    public float m_Score
    {
        get { return this.m_DataScore; }

        set
        {
            if (value >= 0.0f)
                this.m_DataScore = value;
        }
    }

    public PlayerDatas()
    {
        this.m_DataScore = 0;
    }
}

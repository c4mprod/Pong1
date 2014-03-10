using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RacketsDatas : GenericCustomAsset<RacketsDatas>
{
    public static readonly string Path = "Assets/Base/Resources/Rackets/racketsDatas.asset";

    public List<SingleRacketDatas> m_RacketsList = new List<SingleRacketDatas>();
}

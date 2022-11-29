using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapElemantEntity
{
    [Tooltip("敵「寝てる敵」の総数")]
    public int sleeperValue = 0;
    [Tooltip("敵「仲居さん」の総数")]
    public int nakaiValue = 0;
    [Tooltip("合成している家を用意するか")]
    public bool isSynthesisHouse;
    [Tooltip("通常の家の数")]
    public int houseValue;
    [Tooltip("盛り塩がある家の数")]
    public int houseValueOnSolt;
    [Tooltip("赤ちゃんがいる家の数")]
    public int houseValueInBaby;
    [Tooltip("破魔矢がある家の数")]
    public int houseValueInArrow;
}




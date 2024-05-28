using System;
using System.Collections.Generic;
using UnityEngine.Serialization;


[Serializable]
public class GameData
{
    public bool isGameDataExist;
    [FormerlySerializedAs("experiencePoints")] public int exp;
    public int moneyBalance;
    public int currentHookIndex;
    public int currentRodIndex;
    public int currentBaitIndex;

    public List<int> levelThresholds;

    
    //колличество минут прошедших в дне игрового времени, обнуляется каждый игровой день
    public int gameDayTimeInMinutes;

    public List<FishData> fishDatas;
    public List<HookData> hookDatas;
    public List<RodData> rodDatas;
    public List<BaitData> baitDatas;
}
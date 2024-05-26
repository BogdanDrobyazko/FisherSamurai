﻿using System;
using System.Collections.Generic;


[Serializable]
public class GameData
{
    public bool isGameDataExist;
    public int experiencePoints;
    public int moneyBalance;
    public int currentHookIndex;
    public int currentRodIndex;
    public int currentBaitIndex;

    
    //колличество минут прошедших в дне игрового времени, обнуляется каждый игровой день
    public int gameDayTimeInMinutes;

    public List<FishData> fishDatas;
    public List<HookData> hookDatas;
    public List<RodData> rodDatas;
    public List<BaitData> baitDatas;
}
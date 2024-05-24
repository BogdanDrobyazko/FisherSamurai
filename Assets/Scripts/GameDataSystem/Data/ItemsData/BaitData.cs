﻿using System;


[Serializable]
public class BaitData : IItemData
{
    public int itemID;
    public string itemTitle;
    public string itemDescription;
    public int itemPrice;
    public bool isItemBuyed;
    public bool isItemEquipped;
    public int baitsCount;
    public bool isItemMultiplable;
    public int typeOfFishToImprovement;


    public int id
    {
        get => itemID;
    }

    public string title
    {
        get => itemTitle;
    }

    public string description
    {
        get => itemDescription;
    }

    public int price
    {
        get => itemPrice;
    }

    public bool isBuyed
    {
        get => isItemBuyed;
        set => isItemBuyed = value;
    }

    public bool isEquipped
    {
        get => isItemEquipped;
        set => isItemEquipped = value;
    }

    public bool isMultiplable
    {
        get => isItemMultiplable;
    }

    public int potential
    {
        get => typeOfFishToImprovement;
    }

    public int itemCount
    {
        get => baitsCount;
        set => baitsCount = value;
    }
}
using System;


[Serializable]
public class HookData : IItemData
{
    public int itemID;
    public string itemTitle;
    public string itemDescription;
    public int itemPrice;
    public int hookSize;
    public bool isItemBuyed;
    public bool isItemEquipped;
    public bool isItemMultiplable;

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
        get => hookSize;
    }

    public int itemCount { get; set; }
}
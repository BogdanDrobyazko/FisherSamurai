using System;
using UnityEngine;
using UnityEngine.Events;


public class EventBus : MonoBehaviour, IReadOnlyEventBus
{
    private void Awake()
    {
        Debug.Log(name + " awake");
    }

    
    public UnityEvent<FishData> OnFishCatched { get; } = new();
    
    public void TriggerFishCatched(FishData catchedFish)
    {
        OnFishCatched?.Invoke(catchedFish);
    }
    
    public UnityEvent<int> OnMoneyBalanceChanged { get; } = new();

    public void TriggerMoneyBalanceChanged(int newAmount)
    {
        OnMoneyBalanceChanged?.Invoke(newAmount);
        Debug.Log(name + ": Money balance changed to " + newAmount);
    }

    public UnityEvent<int> OnExpChanged { get; } = new();

    public void TriggerExpChanged(int currentExp)
    {
        OnExpChanged?.Invoke(currentExp);
    }

    public UnityEvent<int> OnLevelChanged { get; } = new();
    
    public void TriggerLevelChanged(int newLevel)
    {
        OnLevelChanged?.Invoke(newLevel);
    }
}
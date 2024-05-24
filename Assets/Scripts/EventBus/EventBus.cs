using System;
using UnityEngine;
using UnityEngine.Events;


public class EventBus : MonoBehaviour, IReadOnlyEventBus
{
    public UnityEvent<FishData> OnFishCatched { get; } = new();
    
    public void TriggerFishCatched(FishData catchedFish)
    {
        OnFishCatched?.Invoke(catchedFish);
    }
    
    public UnityEvent<int> OnMoneyBalanceChanged { get; } = new();
    
    public void TriggerMoneyBalanceChanged(int newAmount)
    {
        OnMoneyBalanceChanged?.Invoke(newAmount);
    }
}
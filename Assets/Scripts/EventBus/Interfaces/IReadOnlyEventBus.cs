using System;
using UnityEngine.Events;

public interface IReadOnlyEventBus
{
    public UnityEvent<FishData> OnFishCatched { get; }
    public UnityEvent<int> OnMoneyBalanceChanged { get; }
    public UnityEvent<int> OnExpChanged { get; }
}
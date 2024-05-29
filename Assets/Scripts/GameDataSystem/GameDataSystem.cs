using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Kilosoft.Tools;
using UnityEngine.Events;


public class GameDataSystem : MonoBehaviour
{
    [SerializeField] private EventBus _eventBus;
    private readonly SaveDataSystem _saveDataSystem = new SaveDataSystem();

    [SerializeField] private GameData _currentGameData;
    [SerializeField] private GameData _baseGameData;
    [SerializeField] private List<Sprite> _fishSprites;

    [SerializeField] private List<Sprite> _rodsSprites;
    [SerializeField] private List<Sprite> _hooksSprites;
    [SerializeField] private GameObject _enviromentRod;
    [SerializeField] private GameObject _enviromentHook;
    [SerializeField] private int _maxFrameRate = 60;

    private float realSecondsLast;

    public void Awake()
    {
        Application.targetFrameRate = _maxFrameRate;
        if (_currentGameData.fishDatas == null)
            ChangeCurrentGameDataWithBase();


        if (_currentGameData.fishDatas != null)
        {
            FishData fish = _currentGameData.fishDatas[0];
        }

        RefreshRenderedItems();

        _eventBus.GetComponent<IReadOnlyEventBus>().OnLevelChanged.AddListener(OnLevelChanged);
    }

    private void Update()
    {
        realSecondsLast += Time.deltaTime;

        if (realSecondsLast >= 1)
        {
            _currentGameData.gameDayTimeInMinutes += 1;
            realSecondsLast = 0;
        }

        if (_currentGameData.gameDayTimeInMinutes >= 1440)
        {
            _currentGameData.gameDayTimeInMinutes %= 1440;
        }

        if (_currentGameData.gameDayTimeInMinutes < 0)
        {
            _currentGameData.gameDayTimeInMinutes += 1440;
        }
    }


    [EditorButton("Get All Staff")]
    public void GetAllStaff()
    {
        foreach (var fish in _currentGameData.fishDatas)
        {
            fish.isCaught = true;
        }

        foreach (var hook in _currentGameData.hookDatas)
        {
            hook.isBuyed = true;
        }

        foreach (var rod in _currentGameData.rodDatas)
        {
            rod.isBuyed = true;
        }

        foreach (var bait in _currentGameData.baitDatas)
        {
            bait.isBuyed = true;
            bait.itemCount = 10;
        }

        IncreaseMoney(999999999);
        SaveGameData();
    }

    [EditorButton("Save Base GameData")]
    public void SaveBaseGameData()
    {
        _saveDataSystem.SaveBaseGameData(_baseGameData);
    }

    [EditorButton("Load Base GameData")]
    public void LoadBaseGameData()
    {
        _baseGameData = _saveDataSystem.LoadBaseGameData();
    }

    [EditorButton("Save Current GameData")]
    public void SaveGameData()
    {
        _currentGameData.isGameDataExist = true;
        _saveDataSystem.SaveCurrentGameData(_currentGameData);
        Debug.Log("GameData saved");
    }

    [EditorButton("Load Current GameData")]
    public void LoadGameData()
    {
        _currentGameData = _saveDataSystem.LoadCurrentGameData();

        Debug.Log("GameData loaded");
    }

    [EditorButton("Refresh Max Frame Rate")]
    public void RefreshMaxFrameRate()
    {
        Application.targetFrameRate = _maxFrameRate;
    }


    public void ClearGameData()
    {
        ChangeCurrentGameDataWithBase();
        SaveGameData();
        LoadGameData();
    }

    public void ChangeCurrentGameDataWithBase()
    {
        _currentGameData = _baseGameData;
        SaveGameData();
        RefreshRenderedItems();
        _eventBus.TriggerMoneyBalanceChanged(GetMoneyBalance());
    }


    public int GetMoneyBalance() => _currentGameData.moneyBalance;

    public void DecreaseMoney(int operationMoney)
    {
        int currentMoneyAmount = _currentGameData.moneyBalance;
        int targetMoneyAmount = currentMoneyAmount - operationMoney;

        if (operationMoney < 0 ||
            operationMoney > currentMoneyAmount)
        {
            throw new Exception("Ошибка: operationMoney не может быть меньше нуля!");
        }

        _currentGameData.moneyBalance = targetMoneyAmount;
        _eventBus.TriggerMoneyBalanceChanged(targetMoneyAmount);
    }

    public void IncreaseMoney(int operationMoney)
    {
        int currentMoneyAmount = _currentGameData.moneyBalance;
        int targetMoneyAmount = currentMoneyAmount + operationMoney;

        if (targetMoneyAmount > 1000000000)
        {
            targetMoneyAmount = 999999999;
        }

        _currentGameData.moneyBalance = targetMoneyAmount;
        _eventBus.TriggerMoneyBalanceChanged(targetMoneyAmount);
    }

    public void IncreaseExp(int operationExp)
    {
        int currentExp = _currentGameData.exp;
        int targetExp = currentExp + operationExp;

        if (targetExp > _currentGameData.maxExp)
        {
            targetExp =  _currentGameData.maxExp;
        }

        _currentGameData.exp = targetExp;
        _eventBus.TriggerExpChanged(targetExp);
    }


    public int GetCurrentExp() => _currentGameData.exp;
    public int GetCurrentMaxExp() => _currentGameData.maxExp;
    public int GetPlayerLevel() => _currentGameData.playerLevel;

    public int GetCurrentGameDayTimeInMinuties() => _currentGameData.gameDayTimeInMinutes;
    public FishData GetFishFromAll(int index) => _currentGameData.fishDatas[index];
    public Sprite GetFishSpriteFromAll(int index) => _fishSprites[index];
    public List<FishData> GetAllFishesData() => _currentGameData.fishDatas;

    /// <summary>
    /// Returns the count of all fishes in the current game data.
    /// </summary>
    /// <returns>The number of fishes in the current game data.</returns>
    public int GetAllFishesCount()
    {
        return _currentGameData.fishDatas.Count;
    }


    public bool IsFishAvailable(int fishID) => GetPlayerLevel() >= _currentGameData.fishDatas[fishID].level;

    public bool IsFishCaught(int fishID) => _currentGameData.fishDatas[fishID].isCaught;

    public void RefreshFishRecord(int fishID, char newBestRank, int newBesPrice)
    {
        if (_currentGameData.fishDatas[fishID].bestRank < newBestRank)
            _currentGameData.fishDatas[fishID].bestRank = newBestRank;

        if (_currentGameData.fishDatas[fishID].recordReward < newBesPrice)
            _currentGameData.fishDatas[fishID].recordReward = newBesPrice;

        _currentGameData.fishDatas[fishID].isCaught = true;
    }


    public List<RodData> GetAllRodsData() => _currentGameData.rodDatas;
    public List<BaitData> GetAllBaitsData() => _currentGameData.baitDatas;
    public List<HookData> GetAllHooksData() => _currentGameData.hookDatas;
    public RodData GetCurrentRodData() => _currentGameData.rodDatas[_currentGameData.currentRodIndex];
    public BaitData GetCurrentBaitData() => _currentGameData.baitDatas[_currentGameData.currentBaitIndex];
    public HookData GetCurrentHookData() => _currentGameData.hookDatas[_currentGameData.currentHookIndex];

    public IItemData GetItemWithIndex(int itemTypeIndex, int itemIndex)
    {
        return itemTypeIndex switch
        {
            0 => _currentGameData.rodDatas[itemIndex],
            1 => _currentGameData.hookDatas[itemIndex],
            2 => _currentGameData.baitDatas[itemIndex],
            _ => null
        };
    }

    public void ChangeItemBuyedStatus(int itemTypeIndex, int itemIndex, bool newStatus)
    {
        switch (itemTypeIndex)
        {
            case 0:
                _currentGameData.rodDatas[itemIndex].isBuyed = newStatus;
                break;
            case 1:
                _currentGameData.hookDatas[itemIndex].isBuyed = newStatus;
                break;
            case 2:
                _currentGameData.baitDatas[itemIndex].isBuyed = newStatus;
                break;
            default:
                return;
        }

        SaveGameData();
    }

    public void ChangeItemEquipedStatus(int itemTypeIndex, int itemIndex, bool newStatus)
    {
        switch (itemTypeIndex)
        {
            case 0:
                _currentGameData.rodDatas[itemIndex].isEquipped = newStatus;

                _currentGameData.currentRodIndex = itemIndex;

                _enviromentRod.GetComponent<SpriteRenderer>().sprite = _rodsSprites[itemIndex];
                break;
            case 1:
                _currentGameData.hookDatas[itemIndex].isEquipped = newStatus;

                _currentGameData.currentHookIndex = itemIndex;

                break;
            case 2:
                _currentGameData.baitDatas[itemIndex].isEquipped = newStatus;

                _currentGameData.currentBaitIndex = itemIndex;
                break;
            default:
                return;
        }

        SaveGameData();
    }

    public void RefreshRenderedItems()
    {
        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    List<RodData> rods = GetAllRodsData();
                    foreach (var rod in rods.Where(rod => rod.isEquipped))
                    {
                        ChangeCurrentRodTypeIndex(rod.id);

                        _enviromentRod.GetComponent<SpriteRenderer>().sprite = _rodsSprites[rod.id];
                    }

                    break;
                case 1:
                    List<HookData> hooks = GetAllHooksData();
                    foreach (var hook in hooks.Where(hook => hook.isEquipped))
                    {
                        ChangeCurrentHookTypeIndex(hook.id);
                    }

                    break;
                case 2:
                    List<BaitData> baits = GetAllBaitsData();
                    foreach (var bait in baits.Where(bait => bait.isEquipped))
                    {
                        ChangeCurrentBaitTypeIndex(bait.id);
                    }

                    break;
            }
        }
    }

    public void ChangeCurrentBaitTypeIndex(int index)
    {
        _currentGameData.currentBaitIndex = index;
    }

    public void ChangeCurrentHookTypeIndex(int index)
    {
        _currentGameData.currentHookIndex = index;
    }

    public void ChangeCurrentRodTypeIndex(int index)
    {
        _currentGameData.currentRodIndex = index;
    }

    private void OnLevelChanged(int newLevel) => _currentGameData.playerLevel = newLevel;
}
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class PauseStateMachine : MonoBehaviour
{
    [SerializeField] private EventBus _eventBus;
    
    [Header("Pause Field")] public Canvas pauseFieldCanvas;
    public GameDataSystem gameDataSystem;
    public GameObject fishesCollection;
    public MoneyBalanceTextRenderer moneyBalanceTextRenderer;
    public TextMeshProUGUI time;

    [Header("Collection State")] public Canvas collectionCanvas;
    public CollectionButtons collectionButtons;
    [SerializeField] private Scrollbar _collectionScrolbar;


    [Header("Fish State")] public Canvas fishCanvas;
    public FishData curentCollectionFish;
    public GameObject selectedFishSprite;
    public TextMeshProUGUI selectedFishTitle;
    public TextMeshProUGUI selectedFishDescription;
    [FormerlySerializedAs("selectedFishRecordWeigth")] public TextMeshProUGUI selectedFishBestRank;
    public TextMeshProUGUI selectedFishRecordPrice;
    public TextMeshProUGUI selectedFishCatchingTimeRange;


    [Header("Shop State")] public Canvas shopCanvas;
    public int currentShopItemsTypeIndex;
    public int currentShopItemIndex;
    public int itemsToBuyCount = 1;
    public TextMeshProUGUI itemTitle;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemsToBuyCountText;
    public TextMeshProUGUI boughtItemsCountText;
    public GameObject itemsTypeButtons;
    public GameObject boughtItemsCount;

    [FormerlySerializedAs("itemsToBuyButtons")]
    public GameObject itemsToBuyButtonsContainer;

    public GameObject itemImage;
    public GameObject itemImageFrame;
    public GameObject buyButton;
    public GameObject equipButton;
    public GameObject multipleButtons;
    public List<Sprite> shopFramesSprites;
    public List<Sprite> shopHooksSprites;
    public List<Sprite> shopBaitsSprites;
    public List<Sprite> shopRodsSprites;


    [Header("Settings State")] public Canvas settingsCanvas;
    private IPauseState _currentState;
    private Dictionary<Type, IPauseState> _statesMap;
    public IPauseState lastPauseFieldState;
    public List<List<Sprite>> shopItemsSprites = new List<List<Sprite>>();

    private void Awake()
    {
        shopItemsSprites.Add(shopRodsSprites);
        shopItemsSprites.Add(shopHooksSprites);
        shopItemsSprites.Add(shopBaitsSprites);
    }

    private void Start()
    {
        this.InitState();
        SetDefaultState();
        lastPauseFieldState = new CollectionState();
        gameDataSystem.RefreshRenderedItems();
        
        _eventBus.TriggerMoneyBalanceChanged(gameDataSystem.GetMoneyBalance());
        _eventBus.TriggerExpChanged(gameDataSystem.GetCurrentExp());

        _collectionScrolbar.value = 1;
    }

    private void Update()
    {
        if (this._currentState != null)
            this._currentState.StateUpdate();

        int gameTimeMinutes = gameDataSystem.GetCurrentGameDayTimeInMinuties() % 60 / 5 * 5;
        int gameTimeHour = gameDataSystem.GetCurrentGameDayTimeInMinuties() / 60;
        string gameTimeString = String.Format("{0}:{1}", gameTimeHour.ToString("00"), gameTimeMinutes.ToString("00"));

        time.text = gameTimeString;
    }


    public void OnFishButtonClicked(int index)
    {
        curentCollectionFish = gameDataSystem.GetFishFromAll(index);
        SetFishState();
    }

    public void OnItemsTypeButtonClicked(int index)
    {
        currentShopItemsTypeIndex = index;
        currentShopItemIndex = 0;
        itemsToBuyCount = 1;
        SetShopState();
    }

    public void OnItemButtonClicked(int index)
    {
        currentShopItemIndex = index;
        itemsToBuyCount = 1;
        SetShopState();
    }

    public void OnBuyButtonClicked()
    {
        IItemData currentItem = gameDataSystem.GetItemWithIndex(currentShopItemsTypeIndex, currentShopItemIndex);

        if (!currentItem.isMultiplable)
        {
            if (!currentItem.isBuyed)
            {
                gameDataSystem.DecreaseMoney(currentItem.price);
                gameDataSystem.ChangeItemBuyedStatus(currentShopItemsTypeIndex, currentShopItemIndex, true);

                OnEquipButtonClicked();
            }
        }
        else
        {
            gameDataSystem.DecreaseMoney(currentItem.price * itemsToBuyCount);
            gameDataSystem.ChangeItemBuyedStatus(currentShopItemsTypeIndex, currentShopItemIndex, true);
            currentItem.itemCount += itemsToBuyCount;

            itemsToBuyCount = 1;

            OnEquipButtonClicked();
        }

        SetShopState();
    }

    public void OnEquipButtonClicked()
    {
        IItemData currentItem = gameDataSystem.GetItemWithIndex(currentShopItemsTypeIndex, currentShopItemIndex);


        if (!currentItem.isEquipped && currentItem.isBuyed)
        {
            for (int i = 0; i < 4; i++)
            {
                gameDataSystem.ChangeItemEquipedStatus(currentShopItemsTypeIndex, i, false);
            }

            gameDataSystem.ChangeItemEquipedStatus(currentShopItemsTypeIndex, currentShopItemIndex, true);
        }

        SetShopState();
    }


    private void InitState()
    {
        this._statesMap = new Dictionary<Type, IPauseState>();

        this._statesMap[typeof(PauseDisabledState)] = new PauseDisabledState();
        this._statesMap[typeof(PauseEnabledState)] = new PauseEnabledState();
        this._statesMap[typeof(CollectionState)] = new CollectionState();
        this._statesMap[typeof(ShopState)] = new ShopState();
        this._statesMap[typeof(SettingsState)] = new SettingsState();
        this._statesMap[typeof(CollectionFishFullWiewState)] = new CollectionFishFullWiewState();
    }

    private void SetState(IPauseState newState)
    {
        if (this._currentState != null)
            this._currentState.StateExit();

        this._currentState = newState;
        this._currentState.SetManager(this);
        this._currentState.StateEnter();
        _eventBus.TriggerMoneyBalanceChanged(gameDataSystem.GetMoneyBalance());
    }

    private void SetDefaultState()
    {
        SetPauseDisabledState();
    }

    public void SetLastPauseFieldState()
    {
        SetState(lastPauseFieldState);
    }

    private IPauseState Getstate<T>() where T : IPauseState
    {
        var type = typeof(T);
        return this._statesMap[type];
    }

    public void SetPauseDisabledState()
    {
        var pauseDisabledState = this.Getstate<PauseDisabledState>();
        this.SetState(pauseDisabledState);
    }

    public void SetPauseEnabledState()
    {
        var pauseEnabledState = this.Getstate<PauseEnabledState>();
        this.SetState(pauseEnabledState);
    }

    public void SetCollectionState()
    {
        var collectionState = this.Getstate<CollectionState>();
        this.SetState(collectionState);
    }

    public void SetFishState()
    {
        var collectionFishSelectedState = this.Getstate<CollectionFishFullWiewState>();
        this.SetState(collectionFishSelectedState);
    }

    public void SetShopState()
    {
        var shopState = this.Getstate<ShopState>();
        this.SetState(shopState);
    }

    public void SetSettingsState()
    {
        var settingsState = this.Getstate<SettingsState>();
        this.SetState(settingsState);
    }
}
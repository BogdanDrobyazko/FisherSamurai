using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

 
    public class PauseReferences
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
    }

using System;
using System.Collections.Generic;
using MinigameBehaviorSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MinigameStateMachine : MonoBehaviour, PullingMinigameManager
{
    [Header("Enviroment")] public GameObject cat;
    public GameObject sky;
    public GameObject pirs;
    public GameObject water;
    public GameObject line;

    [Header("Hitching State Settings")] public SortingGroup hitchingSortingGroup;
    public Canvas hitchingCanvas;
    public GameObject hitchingShadow;
    public GameObject hitchingFishOrbit;
    public GameObject hitchingFish;

    [Header("Pulling State Settings")] public SortingGroup pullingSortingGroup;

    [Header("ProgressBar")] public PullingProgressBar pullingProgressBar;


    [Header("Fish")] public Transform fish;

    [FormerlySerializedAs("pullingWiew")] [Header("Pulling Wiew")]
    public Transform rodZone;

    public Transform catchPoint;
    public int currentHookTypeIndex;


    [Header("Reward State Settings")] public GameDataSystem gameDataSystem;
    public FishingPrepareSystem fishingPrepareSystem;
    public Canvas catchedFishCanvas;
    public Canvas newCatchedFishCanvas;
    [Space] public GameObject fishesCollection;
    public FishData rewardFish;
    [Space] public GameObject CatchedFishField;

    [FormerlySerializedAs("rewardTextMeshPro")]
    public TextMeshProUGUI rewardRankTMPro;

    public GameObject rewardSprite;
    public GameObject rewardFrame;
    [Space] public GameObject NewCatchedFishField;
    public TextMeshProUGUI newRewardTextMeshPro;
    public GameObject newRewardSprite;
    private IMinigameState _currentState;
    private Dictionary<Type, IMinigameState> _statesMap;

    private void Start()
    {
        InitState();
        SetDefaultState();


        for (int i = 0; i < gameDataSystem.GetAllFishesCount(); i++)
        {
            fishesCollection.transform.GetChild(i).transform.GetComponent<Button>().interactable =
                gameDataSystem.GetFishFromAll(i).isCaught;
        }
    }

    private void Update()
    {
        if (_currentState != null)
            _currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        if (_currentState != null)
            _currentState.StateFixedUpdate();
    }


    private void InitState()
    {
        _statesMap = new Dictionary<Type, IMinigameState>();
        _statesMap[typeof(IdleState)] = new IdleState();
        _statesMap[typeof(WaitingForFishState)] = new WaitingForFishState();
        _statesMap[typeof(FishPeckingState)] = new FishPeckingState();
        _statesMap[typeof(TimeForHitchState)] = new TimeForHitchState();
        _statesMap[typeof(PullingFishState)] = new PullingFishState();
        _statesMap[typeof(FishCatchedState)] = new FishCatchedState();
        _statesMap[typeof(FishBrokeState)] = new FishBrokeState();
    }


    private void SetState(IMinigameState newState)
    {
        _currentState?.StateExit();

        _currentState = newState;
        _currentState.CacheDataFromManager(this);
        _currentState.StateEnter();
    }

    private void SetDefaultState()
    {
        var defaultState = Getstate<IdleState>();
        SetState(defaultState);
    }


    private IMinigameState Getstate<T>() where T : IMinigameState
    {
        var type = typeof(T);
        return _statesMap[type];
    }

    public void SetIdleState()
    {
        var idleState = Getstate<IdleState>();
        SetState(idleState);
    }

    public void SetWaitingForFishState()
    {
        var WaitingForFishState = Getstate<WaitingForFishState>();
        SetState(WaitingForFishState);
    }

    public void SetFishPeckingState()
    {
        var FishBeganToPeckState = Getstate<FishPeckingState>();
        SetState(FishBeganToPeckState);
    }

    public void SetTimeForHitchState()
    {
        var TimeForHitchState = Getstate<TimeForHitchState>();
        SetState(TimeForHitchState);
    }

    public void SetPullingFishState()
    {
        var PullingFishState = Getstate<PullingFishState>();
        SetState(PullingFishState);
    }

    public void SetFishCatchedState()
    {
        var FishCatchedState = Getstate<FishCatchedState>();
        SetState(FishCatchedState);
    }

    public void SetFishBrokeState()
    {
        var FishBrokeState = Getstate<FishBrokeState>();
        SetState(FishBrokeState);
    }
}
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
    
    [Header("ProgressBar")]
    public PullingProgressBar pullingProgressBar;
    

    [Header("Fish")] public Transform fish;

    [FormerlySerializedAs("pullingWiew")] [Header("Pulling Wiew")] public Transform rodZone;
    public Transform catchPoint;
    public int currentHookTypeIndex;


    [Header("Reward State Settings")] public GameDataSystem gameDataSystem;
    public FishingPrepareSystem fishingPrepareSystem;
    public Canvas catchedFishCanvas;
    public Canvas newCatchedFishCanvas;
    [Space] public GameObject fishesCollection;
    public FishData rewardFish;
    [Space] public GameObject CatchedFishField;
    [FormerlySerializedAs("rewardTextMeshPro")] public TextMeshProUGUI rewardRankTMPro;
    public GameObject rewardSprite;
    public GameObject rewardFrame;
    [Space] public GameObject NewCatchedFishField;
    public TextMeshProUGUI newRewardTextMeshPro;
    public GameObject newRewardSprite;
    private IMinigameState _currentState;
    private Dictionary<Type, IMinigameState> _statesMap;

    private void Start()
    {
        this.InitState();
        SetDefaultState();


        for (int i = 0; i < gameDataSystem.GetAllFishesCount(); i++)
        {
            fishesCollection.transform.GetChild(i).transform.GetComponent<Button>().interactable =
                gameDataSystem.GetFishFromAll(i).isCathed;
        }
    }

    private void Update()
    {
        if (this._currentState != null)
            this._currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        if (this._currentState != null)
            this._currentState.StateFixedUpdate();
    }


    private void InitState()
    {
        this._statesMap = new Dictionary<Type, IMinigameState>();
        this._statesMap[typeof(IdleState)] = new IdleState();
        this._statesMap[typeof(WaitingForFishState)] = new WaitingForFishState();
        this._statesMap[typeof(FishPeckingState)] = new FishPeckingState();
        this._statesMap[typeof(TimeForHitchState)] = new TimeForHitchState();
        this._statesMap[typeof(PullingFishState)] = new PullingFishState();
        this._statesMap[typeof(FishCatchedState)] = new FishCatchedState();
        this._statesMap[typeof(FishBrokeState)] = new FishBrokeState();
    }


    private void SetState(IMinigameState newState)
    {
        if (this._currentState != null)
            this._currentState.StateExit();

        this._currentState = newState;
        this._currentState.CacheDataFromManager(this);
        this._currentState.StateEnter();
    }

    private void SetDefaultState()
    {
        var defaultState = this.Getstate<IdleState>();
        this.SetState(defaultState);
    }


    private IMinigameState Getstate<T>() where T : IMinigameState
    {
        var type = typeof(T);
        return this._statesMap[type];
    }

    public void SetIdleState()
    {
        var idleState = this.Getstate<IdleState>();
        this.SetState(idleState);
    }

    public void SetWaitingForFishState()
    {
        var WaitingForFishState = this.Getstate<WaitingForFishState>();
        this.SetState(WaitingForFishState);
    }

    public void SetFishPeckingState()
    {
        var FishBeganToPeckState = this.Getstate<FishPeckingState>();
        this.SetState(FishBeganToPeckState);
    }

    public void SetTimeForHitchState()
    {
        var TimeForHitchState = this.Getstate<TimeForHitchState>();
        this.SetState(TimeForHitchState);
    }

    public void SetPullingFishState()
    {
        var PullingFishState = this.Getstate<PullingFishState>();
        this.SetState(PullingFishState);
    }

    public void SetFishCatchedState()
    {
        var FishCatchedState = this.Getstate<FishCatchedState>();
        this.SetState(FishCatchedState);
    }

    public void SetFishBrokeState()
    {
        var FishBrokeState = this.Getstate<FishBrokeState>();
        this.SetState(FishBrokeState);
    }
}
using System;
using System.Collections.Generic;
using MinigameBehaviorSystem;
using UnityEngine;

public class PullingFishState : IMinigameState
{
    [Header("Fish Settings")] private MinigameFish _fish;
    private Animator _fishAnimator;
    private Collider2D _fishCollider;
    private Transform _fishTransform;
    private MinigameStateMachine _manager;
    private int _newRodZoneSize;
    private float _progressDecreaseForce = 0.03f;
    private Dictionary<string, float> _progressDecreaseForceDictionary;
    private float _pullForce = 0.6f;

    [Header("Progress Settings")] private Dictionary<string, int> _rarenesDictionary;
    private Transform _catchPoint;
    private float _lineDurability;
    private PullingProgressBar _progressBar;
    //private Transform _progressBarFillTransform;
    // private Vector3 _progressBarScale;
    //private float _currentProgressFillScaleX;
    

    //Pulling Wiew - зона ловли рыбы в минигре, которая управляется игроком
    [Header("Pulling Wiew Settings")] private RodZone _rodZone;
    private Collider2D _rodZoneCollider;
    private int _rodZoneSize;
    private Transform _rodZoneTransform;


    public void CacheDataFromManager(MinigameStateMachine manager)
    {
        _manager = manager;
        SetupCaching();
    }

    public void SetupCaching()
    {
        _progressDecreaseForceDictionary = new Dictionary<string, float>()
        {
            {"0", 0.01f},
            {"1", 0.06f},
            {"2", 0.11f},
            {"3", 0.16f},
            {"4", 0.21f},
            {"5", 0.26f},
            {"6", 0.31f},
            {"7", 0.36f},
            {"8", 0.41f},
            {"9", 0.46f},
            {"10", 0.51f},
            {"11", 0.56f},
            {"12", 0.61f},
            {"13", 0.66f}
        };
            
        _progressBar = _manager.pullingProgressBar;

        //_progressBarFillTransform = _manager.progressBarFillTransform;
        //_progressBarScale = _progressBarFillTransform.localScale;
        //_currentProgressFillScaleX = _progressBarScale.x;
        
        _rodZoneTransform = _manager.rodZone;
        _rodZone = _manager.rodZone.GetComponent<RodZone>();
        _rodZoneCollider = _rodZone.GetCollider();
        _fish = _manager.fish.GetComponent<MinigameFish>();
        _fishTransform = _manager.fish;
        _fishAnimator = _fishTransform.transform.GetComponent<Animator>();
        _catchPoint = _manager.catchPoint;
        _newRodZoneSize = _manager.gameDataSystem.GetCurrentHookData().id;
        _pullForce =
            (float) Math.Round((_manager.gameDataSystem.GetCurrentRodData().potential / (double) 10), 1);

        UpdateProgressDecreaseForce();
    }

    public void StateEnter()
    {
        Debug.Log("Enter Pulling Fish state");

        EnableMinigame();

        Debug.Log("Update Pulling Fish state");
    }

    public void StateUpdate()
    {
        // отключение прогресса для отладки
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnOffProgress();
        }

        _rodZone.FollowMouse();

        ProgressController();
    }

    public void StateFixedUpdate()
    {
        _fish.FishMovementUpdate();
    }

    public void StateExit()
    {
        Debug.Log("Exit Pulling Fish  state");

        //_manager.pullingInputCanvas.enabled = false;
        _manager.pullingSortingGroup.sortingOrder = -1;

        _fishAnimator.enabled = false;
        _fish.MinigameEnd();

        _manager.cat.GetComponent<Animator>().Play("CatAnimationIdle");
    }

    private void UpdateProgressDecreaseForce()
    {
        _progressDecreaseForce = 0.01f + 0.05f * _progressDecreaseForceDictionary[_manager.rewardFish.level.ToString()];
    }

    private void ProgressController()
    {
        
        _progressBar.SetProgress( _lineDurability);

        if (_rodZoneCollider.OverlapPoint(_fishTransform.position))
        {
            float catchDistanceY = Mathf.Abs(_fishTransform.position.y - _catchPoint.position.y);

            _fish.Pull(_pullForce);

            if (catchDistanceY <= 0.5f)
            {
                _manager.SetFishCatchedState();
            }
        }
        else
        {
            
            _lineDurability -= _progressDecreaseForce * Time.deltaTime;
            if (_lineDurability <= 0)
            {
                _manager.SetFishBrokeState();
            }
        }


        _lineDurability = Mathf.Clamp(_lineDurability, 0, 1);
    } //Отслеживание прогресса и управление зеленой полоской

    private void EnableMinigame()
    {
        //_manager.pullingInputCanvas.enabled = true;
        _manager.pullingSortingGroup.sortingOrder = 10;

        _lineDurability = 1;

        _rodZone.ChageRodZoneSize(_newRodZoneSize);

        _fishAnimator.enabled = true;
        _fish.MinigameStart(_manager.rewardFish.level);
    }

    private void OnOffProgress()
    {
        if (_progressDecreaseForce == 0)
        {
            _pullForce =
                (float) Math.Round((_manager.gameDataSystem.GetCurrentRodData().potential / (double) 10), 1);
            UpdateProgressDecreaseForce();
        }
        else
        {
            _progressDecreaseForce = 0;
            _pullForce = 0;
        }
    }

}
using UnityEngine;

public class FishPeckingState : IMinigameState
{
    private Animator _hitchingFishAnimator;
    private Animator _hitchingFishOrbitAnimator;
    private MinigameStateMachine _manager;
    private int _prepareAnimationCount;
    private float _secondsForPeckingMax = 5;
    private float _secondsForPeckingMin = 2;
    private float _waitTimeAfterByte;

    public void CacheDataFromManager(MinigameStateMachine manager)
    {
        _manager = manager;
    }

    public void StateEnter()
    {
        Debug.Log("Enter Fish Began To Peck state");
        _hitchingFishAnimator = _manager.hitchingFish.GetComponent<Animator>();
        _prepareAnimationCount = Random.Range(1, 3);
        _waitTimeAfterByte = Random.Range(_secondsForPeckingMin, _secondsForPeckingMax);
        Debug.Log("Update Fish Began To Peck state");
    }

    public void StateUpdate()
    {
        if (SC_MobileControls.instance.GetMobileButtonDown("MinigameTouchButton"))
        {
            _manager.SetFishBrokeState();
        }

        // Проверяем, завершилась ли анимация HitchinPrepare, контролируемая HitchingPrepareAnimationEvent 
        if (_hitchingFishAnimator.GetCurrentAnimatorStateInfo(0).IsName("HitchingFishByte"))
        {
            _manager.SetTimeForHitchState();
        }
    }

    public void StateFixedUpdate()
    {
    }

    public void StateExit()
    {
        Debug.Log("Exit Fish Began To Peck  state");
    }

    public void SetupCaching()
    {
        throw new System.NotImplementedException();
    }
}
using UnityEngine;

public class WaitingForFishState : IMinigameState
{
    private Animator _catAnimator;
    private Transform _fishOrbitTransform;
    private Animator _hitchingFishAnimator;
    private SpriteRenderer _hitchingFishSpriteRenderer;
    private MinigameStateMachine _manager;
    private float _secondsBeforePeckingMax = 10;
    private float _secondsBeforePeckingMin = 5;
    private float _waitTime;
    private bool isShadowAnimationEnabled;


    public void CacheDataFromManager(MinigameStateMachine manager)
    {
        _manager = manager;
    }


    public void StateEnter()
    {
        Debug.Log("Enter Waiting For Fish state");

        _manager.rewardFish = _manager.fishingPrepareSystem.SetFishToCatch();

        _manager.cat.GetComponent<Animator>().Play("CatAnimationRodShot");

        int randomAngleForHitchingFish = Random.Range(60, 300);

        _manager.hitchingFishOrbit.transform.rotation = Quaternion.Euler(new Vector3(0, 0, randomAngleForHitchingFish));

        _waitTime = Random.Range(_secondsBeforePeckingMin, _secondsBeforePeckingMax);

        _fishOrbitTransform = _manager.hitchingFishOrbit.transform;

        _hitchingFishSpriteRenderer = _manager.hitchingFish.GetComponent<SpriteRenderer>();

        _catAnimator = _manager.cat.GetComponent<Animator>();
        
        _hitchingFishSpriteRenderer.flipX = _fishOrbitTransform.eulerAngles.z > 180f;

        _hitchingFishAnimator = _manager.hitchingFish.GetComponent<Animator>();
        
        
        Debug.Log("Update Waiting For Fish state");
    }

    public void StateUpdate()
    {
        if (_catAnimator.GetCurrentAnimatorStateInfo(0).IsName("CatAnimationWaitingForFish"))
        {
            _manager.hitchingSortingGroup.sortingOrder = 10;
        }

        _waitTime -= Time.deltaTime;

        if (_waitTime <= 0)
        {
            _hitchingFishAnimator.enabled = true;
            _manager.hitchingFish.GetComponent<SpriteRenderer>().sortingOrder = 12;
            _hitchingFishAnimator.GetComponent<Animator>().Play("PreHitchingFishPrepare");
            _manager.SetFishPeckingState();
        }
    }

    public void StateFixedUpdate()
    {
    }

    public void StateExit()
    {
        Debug.Log("Exit Waiting For Fish  state");
    }

    public void SetupCaching()
    {
        
    }
}
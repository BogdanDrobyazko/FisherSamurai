using UnityEngine;

public class TimeForHitchState : IMinigameState
{
    private MinigameStateMachine _manager;
    private float _secondsForHitch = 1f;
    private float _waitTime;

    public void CacheDataFromManager(MinigameStateMachine manager)
    {
        _manager = manager;
    }

    public void StateEnter()
    {
        Debug.Log("Enter Time For Hitch state");

        _manager.hitchingCanvas.enabled = true;

        if (_manager.gameDataSystem.GetCurrentBaitData().itemCount >= 1)
        {
            _manager.gameDataSystem.GetCurrentBaitData().itemCount -= 1;
        }

        if (_manager.gameDataSystem.GetCurrentBaitData().itemCount == 0)
        {
            _manager.gameDataSystem.GetCurrentBaitData().isEquipped = false;
            _manager.gameDataSystem.ChangeCurrentBaitTypeIndex(4);
        }

        //_manager.hitchingFishOrbit.GetComponent<Animator>().enabled = false;


        _waitTime = _secondsForHitch;

        Debug.Log("Update Time For Hitch state");
    }

    public void StateUpdate()
    {
        _waitTime -= Time.deltaTime;

        if (_waitTime <= 0)
        {
            _manager.SetFishBrokeState();
        }
        else if (SC_MobileControls.instance.GetMobileButtonDown("MinigameTouchButton"))
        {
            _manager.SetPullingFishState();
        }
    }

    public void StateFixedUpdate()
    {
    }

    public void StateExit()
    {
        Debug.Log("Exit Time For Hitch  state");
        _manager.hitchingSortingGroup.sortingOrder = -1;

        _manager.hitchingCanvas.enabled = false;
    }

    public void SetupCaching()
    {
        throw new System.NotImplementedException();
    }
}
using System;
using UnityEngine;

[Serializable]
public class FishBrokeState : IMinigameState
{
    [SerializeField] private MinigameStateMachine _manager;


    public void CacheDataFromManager(MinigameStateMachine manager)
    {
        _manager = manager;
    }

    public void StateEnter()
    {
        Debug.Log("Enter Fish Broke state");


        _manager.cat.GetComponent<Animator>().Play("CatAnimationIdle");


        Debug.Log("Update Fish Broke state");
    }

    public void StateUpdate()
    {
        _manager.SetIdleState();
    }

    public void StateFixedUpdate()
    {
    }

    public void StateExit()
    {
        Debug.Log("Exit Fish Broke  state");

        _manager.catchedFishCanvas.enabled = false;

        _manager.gameDataSystem.SaveGameData();
    }

    public void SetupCaching()
    {
        throw new NotImplementedException();
    }
}
using UnityEngine;
public class IdleState :  IMinigameState
{
    private GameDataSystem _gameDataSystem;

    private MinigameStateMachine _manager;

    public void CacheDataFromManager(MinigameStateMachine manager)
    {
        _manager = manager;
        _gameDataSystem = manager.gameDataSystem;
        
    }


    public void StateEnter()
    {
        Debug.Log("Enter Idle state");

        _gameDataSystem.LoadGameData();
        _gameDataSystem.RefreshRenderedItems();
        _gameDataSystem.IncreaseMoney(0);

        _manager.hitchingSortingGroup.sortingOrder = -1;
        _manager.hitchingCanvas.enabled = false;
        //_manager.hitchingFishOrbit.GetComponent<Animator>().enabled = false;
        _manager.hitchingFish.GetComponent<SpriteRenderer>().sortingOrder = -1;
        _manager.hitchingFish.GetComponent<Animator>().enabled = false;


        _manager.pullingSortingGroup.sortingOrder = -1;
        //_manager.pullingInputCanvas.enabled = false;

        _manager.catchedFishCanvas.enabled = true;
        _manager.newCatchedFishCanvas.enabled = false;

        _gameDataSystem.IncreaseMoney(0);

        Debug.Log("Update Idle state");

        for (int i = 0; i < 3; i++)
        {
            if (_gameDataSystem.GetAllBaitsData() == null)
            {
                _gameDataSystem.ChangeCurrentGameDataWithBase();
                ;
            }

            if (_gameDataSystem.GetAllBaitsData()[i].isEquipped)
            {
                _gameDataSystem.ChangeCurrentBaitTypeIndex(i);
            }
        }

        _manager.fish.transform.GetComponent<Animator>().enabled = false;
        _manager.hitchingShadow.transform.GetComponent<Animator>().enabled = false;
    }

    public void StateUpdate()
    {
        if (SC_MobileControls.instance.GetMobileButtonDown("MinigameTouchButton"))
        {
            _manager.SetWaitingForFishState();
        }
    }

    public void StateFixedUpdate()
    {
    }

    public void StateExit()
    {
        Debug.Log("Exit Idle state");
    }
}
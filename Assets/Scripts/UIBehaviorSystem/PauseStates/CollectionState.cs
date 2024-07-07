using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionState : IPauseState
{
    private PauseBehaviorManager _manager;
    private FishData _selectedFish;

    public void SetManager(PauseBehaviorManager manager)
    {
        _manager = manager;
    }

    public void StateEnter()
    {
        Debug.Log("Enter Pause Collection State state");

        _manager.collectionCanvas.enabled = true;
        _manager.fishCanvas.enabled = false;
        _manager.lastPauseFieldState = this;

        CollectionButtons collectionButtons = _manager.collectionButtons;

        for (int i = 0; i < _manager.gameDataSystem.GetAllFishesCount(); i++)
        {
            collectionButtons.SetImage(i, _manager.gameDataSystem.IsFishAvailable(i),
                _manager.gameDataSystem.IsFishCaught(i));
        }
    }

    public void StateUpdate()
    {
        if (SC_MobileControls.instance.GetMobileButtonDown("PauseButton"))
        {
            _manager.SetPauseDisabledState();
            _manager.pauseFieldCanvas.enabled = false;
        }
    }


    public void StateExit()
    {
        Debug.Log("Exit Pause Collection State state");
        _manager.collectionCanvas.enabled = false;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class SettingsState : IPauseState
{
    private PauseBehaviorManager _manager;

    public void SetManager(PauseBehaviorManager manager)
    {
        _manager = manager;
    }

    public void StateEnter()
    {
        Debug.Log("Enter Pause Settings State state");

        _manager.settingsCanvas.enabled = true;
        _manager.lastPauseFieldState = this;
    }

    public void StateUpdate()
    {
        if (SC_MobileControls.instance.GetMobileButtonDown("PauseButton"))
        {
            _manager.SetPauseDisabledState();
            _manager.pauseFieldCanvas.enabled = false;
        }

        if (SC_MobileControls.instance.GetMobileButtonDown("ClearAllProgressButton"))
        {
            _manager.gameDataSystem.ClearGameData();

            for (int i = 0; i < _manager.gameDataSystem.GetAllFishesCount(); i++)
            {
                _manager.fishesCollection.transform.GetChild(i).transform.GetComponent<Button>().interactable =
                    _manager.gameDataSystem.GetFishFromAll(i).isCaught;
            }
        }

        
    }


    public void StateExit()
    {
        Debug.Log("Exit Pause Settings State state");

        _manager.settingsCanvas.enabled = false;
    }
}
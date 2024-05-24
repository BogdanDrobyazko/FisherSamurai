using UnityEngine;

public class PauseDisabledState : IPauseState
{
    private PauseBehaviorManager _manager;

    public void SetManager(PauseBehaviorManager manager)
    {
        _manager = manager;
    }

    public void StateEnter()
    {
        Debug.Log("Enter Pause Disabled State state");

        _manager.settingsCanvas.enabled = false;
        _manager.pauseFieldCanvas.enabled = false;
        _manager.shopCanvas.enabled = false;
        _manager.collectionCanvas.enabled = false;
        _manager.fishCanvas.enabled = false;

        Time.timeScale = 1;
    }

    public void StateUpdate()
    {
        if (SC_MobileControls.instance.GetMobileButtonDown("PauseButton"))
        {
            _manager.SetPauseEnabledState();
        }
    }


    public void StateExit()
    {
        Debug.Log("Exit Pause Disabled State state");
    }
}
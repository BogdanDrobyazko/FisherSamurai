using UnityEngine;

public class PauseEnabledState : IPauseState
{
    private PauseBehaviorManager _manager;

    public void SetManager(PauseBehaviorManager manager)
    {
        _manager = manager;
    }

    public void StateEnter()
    {
        Debug.Log("Enter Pause Enabled State state");
        _manager.pauseFieldCanvas.enabled = true;
        Time.timeScale = 0;
        _manager.SetLastPauseFieldState();
    }

    public void StateUpdate()
    {
    }


    public void StateExit()
    {
        Debug.Log("Exit Pause Enabled State state");
    }
}
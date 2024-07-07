
public interface IPauseState
{
    void SetManager(PauseStateMachine manager);
    void StateEnter();
    void StateUpdate();
    void StateExit();
}

public interface IPauseState
{
    void SetManager(PauseBehaviorManager manager);
    void StateEnter();
    void StateUpdate();
    void StateExit();
}
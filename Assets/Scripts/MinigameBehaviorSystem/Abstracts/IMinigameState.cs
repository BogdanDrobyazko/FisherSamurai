
public interface IMinigameState
{
    void CacheDataFromManager(MinigameStateMachine manager);
    void StateEnter();
    void StateUpdate();
    void StateFixedUpdate();
    void StateExit();
}
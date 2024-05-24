using UnityEngine;

public class PullingFishStateTester : MonoBehaviour
{
    [SerializeField] private MinigameStateMachine _minigameManager;

    private void Update()
    {
        //вызываем Pulling Fish State при нажатии кнопки L

        if (Input.GetKeyDown(KeyCode.L))
        {
            _minigameManager.SetPullingFishState();
        }
    }
}
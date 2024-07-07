using UnityEngine;
using UnityEngine.UI;

public class CollectionFishFullWiewState : IPauseState
{
    private PauseStateMachine _manager;

    public void SetManager(PauseStateMachine manager)
    {
        _manager = manager;
    }

    public void StateEnter()
    {
        Debug.Log("Enter Pause Fish State state");

        _manager.fishCanvas.enabled = true;

        FishData fish = _manager.curentCollectionFish;

        _manager.selectedFishSprite.GetComponent<Image>().sprite =
            _manager.gameDataSystem.GetFishSpriteFromAll(fish.id);


        _manager.selectedFishTitle.text = fish.title;
        _manager.selectedFishDescription.text = fish.description;
        _manager.selectedFishBestRank.text = fish.bestRank.ToString();
        _manager.selectedFishRecordPrice.text = fish.recordReward.ToString();
        _manager.selectedFishCatchingTimeRange.text = $"{fish.catchStartHour}:00 - {(fish.catchEndHour + 1) % 24}:00";
        ;
    }

    public void StateUpdate()
    {
        if (SC_MobileControls.instance.GetMobileButtonDown("CloseFishButton"))
        {
            _manager.SetCollectionState();
        }
    }

    public void StateExit()
    {
        Debug.Log("Exit Pause Fish State state");

        _manager.fishCanvas.enabled = false;
    }
}
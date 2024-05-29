using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FishCatchedState : IMinigameState
{
    private FishData _fish;
    [SerializeField] private MinigameStateMachine _manager;

    public void CacheDataFromManager(MinigameStateMachine manager)
    {
        _manager = manager;
    }

    public void StateEnter()
    {
        Debug.Log("Enter Fish Catched state");


        _fish = _manager.rewardFish;

        Dictionary<int, char> rankDictionary = new Dictionary<int, char>()
        {
            {0, 'S'},
            {1, 'A'},
            {2, 'B'},
            {3, 'C'},
            {4, 'D'}
        };

        //int catchedWeight = Random.Range(_fish.maxWeight / 10, _fish.maxWeight);
        //float catchedPriceFloat = _fish.price * ((float) catchedWeight / (float) _fish.maxWeight);
        int catchedPrice = (int) _fish.rewardMoney;
        int catchedExp = (int) _fish.rewardExp;
        char catchedRank = rankDictionary[Random.Range(0, 5)];


        if (_fish.isCaught)
        {
            _manager.catchedFishCanvas.enabled = true;

            RectTransform targetRT = _manager.fishesCollection.transform.GetChild(_fish.id).GetChild(0)
                .GetComponent<RectTransform>();
            Sprite targetFishSprite = _manager.fishesCollection.transform.GetChild(_fish.id).GetChild(0)
                .GetComponent<Image>().sprite;

            Sprite targetFrameSprite = _manager.fishesCollection.transform.GetChild(_fish.id)
                .GetComponent<Image>().sprite;

            _manager.rewardFrame.transform.GetComponent<Image>().sprite = targetFrameSprite;

            _manager.rewardSprite.transform.GetComponent<Image>().sprite = targetFishSprite;

            _manager.rewardSprite.transform.GetComponent<RectTransform>().anchoredPosition = targetRT.anchoredPosition;
            _manager.rewardSprite.transform.GetComponent<RectTransform>().sizeDelta = targetRT.sizeDelta;
            _manager.rewardSprite.transform.GetComponent<RectTransform>().rotation = targetRT.rotation;


            _manager.rewardRankTMPro.text = catchedRank.ToString();
            _manager.CatchedFishField.GetComponent<Animator>().Play("CatchedFishAnimation");

            _manager.SetIdleState();
        }
        else
        {
            _manager.newCatchedFishCanvas.enabled = true;

            _manager.NewCatchedFishField.GetComponent<Animator>().Play("NewFishFieldAnimation");
            _manager.newRewardSprite.SetActive(true);
            Sprite sprite = _manager.gameDataSystem.GetFishSpriteFromAll(_fish.id);
            _manager.newRewardSprite.GetComponent<Image>().sprite = sprite;
            _manager.newRewardTextMeshPro.text =
                $"{_fish.title}!\n" +
                $"Качество: {catchedRank.ToString()}\n" +
                $"Стоимость: {catchedPrice.ToString()}\n" +
                $"{_fish.description}";
        }


        _manager.gameDataSystem.RefreshFishRecord(_fish.id, catchedRank, catchedPrice);


        _manager.gameDataSystem.IncreaseMoney(catchedPrice);
        _manager.gameDataSystem.IncreaseExp(catchedExp);


        Debug.Log("Update Fish Catched state");
    }

    public void StateUpdate()
    {
        if (SC_MobileControls.instance.GetMobileButton("CloseRewardButton"))
        {
            _manager.NewCatchedFishField.GetComponent<Animator>().Play("NewCatchedFishFieldOFF");
            _manager.SetIdleState();
        }
    }

    public void StateFixedUpdate()
    {
    }

    public void StateExit()
    {
        Debug.Log("Exit Fish Catched  state");

        _manager.catchedFishCanvas.enabled = false;

        _manager.gameDataSystem.IncreaseMoney(0);

        _manager.gameDataSystem.SaveGameData();
    }

    public void SetupCaching()
    {
        
    }
}
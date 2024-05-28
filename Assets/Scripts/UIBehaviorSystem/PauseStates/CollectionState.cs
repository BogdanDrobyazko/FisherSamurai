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

        for (int i = 0; i < _manager.gameDataSystem.GetAllFishesCount(); i++)
        {
            Transform fishButton = _manager.fishesCollection.transform.GetChild(i).transform;

            if (_manager.gameDataSystem.GetFishFromAll(i).isCathed)
            {
                fishButton.GetComponent<Button>().interactable = true;
                fishButton.GetChild(0).transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                fishButton.GetChild(1).transform.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
            }
            else
            {
                fishButton.GetComponent<Button>().interactable = false;
                fishButton.GetChild(0).transform.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                fishButton.GetChild(1).transform.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.2f);
            }
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
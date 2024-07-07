using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopState : IPauseState
{
    private List<BaitData> _baitsList;

    NumberFormatInfo _frmt = new NumberFormatInfo {NumberGroupSeparator = " ", NumberDecimalDigits = 0};
    private List<HookData> _hooksList;
    private PauseStateMachine _manager;
    private List<RodData> _rodsList;

    public void SetManager(PauseStateMachine manager)
    {
        _manager = manager;
    }

    public void StateEnter()
    {
        Debug.Log("Enter Pause Shop State state");

        _rodsList = _manager.gameDataSystem.GetAllRodsData();
        _hooksList = _manager.gameDataSystem.GetAllHooksData();
        _baitsList = _manager.gameDataSystem.GetAllBaitsData();
        int currentItemTypeIndex = _manager.currentShopItemsTypeIndex;
        int currentItemIndex = _manager.currentShopItemIndex;
        RefreshItems(currentItemTypeIndex);

        _manager.shopCanvas.enabled = true;
        _manager.lastPauseFieldState = this;

        

        _manager.itemsToBuyCountText.text = _manager.itemsToBuyCount.ToString();

        _manager.itemsTypeButtons.transform.GetComponent<OnClickedPauseButtonDisabler>()
            .ButtonDisabler(currentItemTypeIndex);

        _manager.itemsToBuyButtonsContainer.transform.GetComponent<OnClickedPauseButtonDisabler>()
            .ButtonDisabler(currentItemIndex);
    }

    public void StateUpdate()
    {
        IItemData currentItem = _manager.gameDataSystem.GetItemWithIndex(_manager.currentShopItemsTypeIndex,
            _manager.currentShopItemIndex);

        if (SC_MobileControls.instance.GetMobileButtonDown("PauseButton"))
        {
            _manager.SetPauseDisabledState();
            _manager.pauseFieldCanvas.enabled = false;
        }

        if (SC_MobileControls.instance.GetMobileButton("ForceDicreaseShopButton"))
        {
            if (_manager.itemsToBuyCount > 1)
                _manager.itemsToBuyCount -= 1;

            _manager.itemsToBuyCountText.text = _manager.itemsToBuyCount.ToString();

            RefreshPrice(currentItem);
        }

        if (SC_MobileControls.instance.GetMobileButton("ForceIncreaseShopButton"))
        {
            if (_manager.gameDataSystem.GetMoneyBalance() >= ((_manager.itemsToBuyCount + 1) * currentItem.price))
            {
                _manager.itemsToBuyCount += 1;
            }

            _manager.itemsToBuyCountText.text = _manager.itemsToBuyCount.ToString();

            RefreshPrice(currentItem);
        }

        if (SC_MobileControls.instance.GetMobileButtonDown("DicreaseShopButton"))
        {
            if (_manager.itemsToBuyCount > 1)
                _manager.itemsToBuyCount -= 1;

            _manager.itemsToBuyCountText.text = _manager.itemsToBuyCount.ToString();

            RefreshPrice(currentItem);
        }

        if (SC_MobileControls.instance.GetMobileButtonDown("IncreaseShopButton"))
        {
            if (_manager.gameDataSystem.GetMoneyBalance() >= ((_manager.itemsToBuyCount + 1) * currentItem.price))
            {
                _manager.itemsToBuyCount += 1;
            }

            _manager.itemsToBuyCountText.text = _manager.itemsToBuyCount.ToString();

            RefreshPrice(currentItem);
        }
    }


    public void StateExit()
    {
        Debug.Log("Exit Pause Shop State state");

        _manager.shopCanvas.enabled = false;
    }

    public void RefreshPrice(IItemData currentItem)
    {
        int currentPrice = currentItem.price * _manager.itemsToBuyCount;
        _manager.itemPrice.text = $"Стоимость: {currentPrice.ToString("n", _frmt)}";
    }

    private void RefreshItems(int itemsTypeIndex)
    {
        
        
        _manager.itemImageFrame.GetComponent<Image>().sprite =
            _manager.shopFramesSprites[_manager.currentShopItemIndex];
        
        GameObject buttonsContainer = _manager.itemsToBuyButtonsContainer;
        
        for (int i = 0; i < 4; i++)
        {
            Image itemButtonImage = buttonsContainer.transform.GetChild(i).GetComponent<Image>();

            itemButtonImage.sprite = _manager.shopItemsSprites[itemsTypeIndex][i];
        }

        switch (itemsTypeIndex)
        {
            case 0:
            {
                RodData currentRod = _rodsList[_manager.currentShopItemIndex];

                _manager.itemImage.GetComponent<Image>().sprite =
                    _manager.shopRodsSprites[_manager.currentShopItemIndex];

                RefreshItem(currentRod);
                break;
            }
            case 1:
            {
                HookData currentHook = _hooksList[_manager.currentShopItemIndex];


                _manager.itemImage.GetComponent<Image>().sprite =
                    _manager.shopHooksSprites[_manager.currentShopItemIndex];

                RefreshItem(currentHook);
                break;
            }
            case 2:
            {
               BaitData currentBait = _baitsList[_manager.currentShopItemIndex];

                _manager.itemImage.GetComponent<Image>().sprite =
                    _manager.shopBaitsSprites[_manager.currentShopItemIndex];

                RefreshItem(currentBait);
                break;
            }
            default:
                return;
        }
    }

    /*private void RefreshItemsTypeButtons<T>(List<T> currentItemTypeList) where T : IItemData
    {
        GameObject buttonsContainer = _manager.itemsToBuyButtonsContainer;
        
    }
    
    private void RefreshItemsTypeButtons<T>(List<T> currentItemTypeList) where T : IItemData
    {
        
        for (int i = 0; i < 4; i++)
        {
            //buttons.transform.GetChild(i).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = currentItemTypeList[i].title;
            Image itemImage = buttonsContainer.transform.GetChild(i).transform.GetComponentInChildren<Image>();
            
            itemImage.sprite = 
        }
    }*/


    private void RefreshItem<T>(T currentItem) where T : IItemData
    {
        _manager.itemTitle.text = currentItem.title;
        _manager.itemDescription.text = currentItem.description;


        _manager.itemPrice.text = $"Стоимость: {currentItem.price.ToString("n", _frmt)}";

        Button buyButton = _manager.buyButton.transform.GetComponent<Button>();
        TextMeshProUGUI buyButtonTMPro =
            _manager.buyButton.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
        buyButton.transform.gameObject.SetActive(true);
        _manager.buyButton.transform.localPosition = new Vector3(0, -240, 0);

        Button equipButton = _manager.equipButton.transform.GetComponent<Button>();

        TextMeshProUGUI equipButtonTMPro =
            _manager.equipButton.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
        equipButton.transform.gameObject.SetActive(false);
        _manager.equipButton.transform.localPosition = new Vector3(0, -240, 0);

        if (!currentItem.isBuyed)
        {
            if (_manager.gameDataSystem.GetMoneyBalance() >= currentItem.price * _manager.itemsToBuyCount)
            {
                //buyButtonTMPro.color = new Color(0.1f, 0.1f, 0.1f, 1);

                buyButton.interactable = true;
                buyButtonTMPro.text = "Купить";
            }
            else
            {
                //var colorBlock = buyButton.colors;
                //colorBlock.disabledColor = new Color(0.8f, 0.4f, 0.2f, 0.5f);
                //buyButton.colors = colorBlock;

                //buyButtonTMPro.color = new Color(0.1f, 0.1f, 0.1f, 0.7f);

                buyButton.interactable = false;
                buyButtonTMPro.text = "Купить";
            }
        }
        else
        {
            if (!currentItem.isEquipped)
            {
                //equipButtonTMPro.color = new Color(0.1f, 0.1f, 0.1f, 1);
                buyButton.transform.gameObject.SetActive(false);
                equipButton.transform.gameObject.SetActive(true);
                equipButton.interactable = true;
                equipButtonTMPro.text = "Экипировать";
            }
            else
            {
                //var colorBlock = equipButton.colors;
                //colorBlock.disabledColor = new Color(0.5f, 0.8f, 0.2f, 0.5f);
                //equipButton.colors = colorBlock;
                buyButton.transform.gameObject.SetActive(false);
                equipButton.transform.gameObject.SetActive(true);
                equipButton.interactable = false;

                equipButtonTMPro.text = "Экипировано";
            }
        }

        if (!currentItem.isMultiplable)
        {
            _manager.multipleButtons.SetActive(false);
            _manager.boughtItemsCount.SetActive(false);
        }
        else
        {
            _manager.multipleButtons.SetActive(true);
            _manager.boughtItemsCount.SetActive(true);

            _manager.buyButton.transform.gameObject.SetActive(true);

            if (_manager.gameDataSystem.GetMoneyBalance() >= currentItem.price)
            {
                //buyButtonTMPro.color = new Color(0.1f, 0.1f, 0.1f, 1);

                buyButton.interactable = true;
                buyButtonTMPro.text = "Купить";
            }
            else
            {
                //var colorBlock = buyButton.colors;
                //colorBlock.disabledColor = new Color(0.8f, 0.4f, 0.2f, 0.5f);
                //buyButton.colors = colorBlock;

                //buyButtonTMPro.color = new Color(0.1f, 0.1f, 0.1f, 0.7f);

                buyButton.interactable = false;
                buyButtonTMPro.text = "Купить";
            }

            _manager.boughtItemsCountText.text =
                string.Format("Осталось: {0}", currentItem.itemCount.ToString("n", _frmt));

            if (currentItem.itemCount > 0)
            {
                _manager.equipButton.transform.gameObject.SetActive(true);
                buyButton.transform.localPosition = new Vector3(120, -240, 0);
                equipButton.transform.localPosition = new Vector3(-120, -240, 0);
            }
            else
            {
                _manager.equipButton.transform.gameObject.SetActive(false);
                buyButton.transform.localPosition = new Vector3(0, -250, 0);
            }
        }
    }
}
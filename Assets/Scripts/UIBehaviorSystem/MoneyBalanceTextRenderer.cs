using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

public class MoneyBalanceTextRenderer : MonoBehaviour
{
    [SerializeField] GameObject _eventBusGameObject;
    [SerializeField] private float _amountChangeDelay; // продолжительность времени для плавного перехода
    [SerializeField] private TextMeshProUGUI _moneyBalanceText;
    private readonly NumberFormatInfo _frmt = new NumberFormatInfo {NumberGroupSeparator = "\n \n ", NumberDecimalDigits = 0};
    private int _currentValue = 0;

    private void Awake()
    {
        _eventBusGameObject.GetComponent<IReadOnlyEventBus>().OnMoneyBalanceChanged.AddListener(UpdateBalanceText);
    }

    public void UpdateBalanceText(int targetAmount) // обновить текстовое отображение количества денег
    {
        StartCoroutine(GraduallyIncreaseMoneyCoroutine(_currentValue, targetAmount));

        _currentValue = targetAmount;
    }
    
    private IEnumerator GraduallyIncreaseMoneyCoroutine(int currentValue, int targetValue)
    {
        int operationMoney = targetValue - currentValue;

        int delta = 5;

        while (MathF.Abs(operationMoney) > 0)
        {
            if (MathF.Abs(delta * (MathF.Abs(operationMoney) / operationMoney)) < MathF.Abs(operationMoney))
            {
                currentValue += (int) (delta * (MathF.Abs(operationMoney) / operationMoney));
                operationMoney -= (int) (delta * (MathF.Abs(operationMoney) / operationMoney));
                delta = (int) (delta * 1.2f);
            }
            else
            {
                currentValue += operationMoney;
                operationMoney = 0;
            }

            _moneyBalanceText.text = currentValue.ToString("n", _frmt);

            yield return new WaitForEndOfFrame();
        }
    }
}
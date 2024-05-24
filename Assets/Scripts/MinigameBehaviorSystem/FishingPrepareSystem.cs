using System.Collections.Generic;
using UnityEngine;



public class FishingPrepareSystem : MonoBehaviour
{
    [SerializeField] private GameDataSystem _gameDataSystem;
    [SerializeField] private List<float> _currentProbabilities; //вероятности выпадения рыб
    [SerializeField] private List<FishData> _availableFishes; // лист с доступными рыбами

    [SerializeField] private int _currentHour; //текущий час
    [SerializeField] private int _updateTime; //время обнавлени
    private List<FishData> _fishList; //база данных рыб

    private BaitData currentBait;


    private void Awake()
    {
        _fishList = _gameDataSystem.GetAllFishesData();

        //при первом включении меняем доступных рыб
        _updateTime = 1;
        _currentHour = _gameDataSystem.GetCurrentGameDayTimeInMinuties() / 60;
        FindAvailableFishes(_currentHour);
    }


    private void Update()
    {
        //задаем текущее игровое время
        _currentHour = _gameDataSystem.GetCurrentGameDayTimeInMinuties() / 60;

        //если настало время поменять доступных рыб
        if (IsTimeToUpdateAwailables())
        {
            _fishList = _gameDataSystem.GetAllFishesData();
            //меняем доступных рыб
            FindAvailableFishes(_currentHour);
        }
    }


    // метод возвращающий рыбу для ловли
    public FishData SetFishToCatch()
    {
        _fishList = _gameDataSystem.GetAllFishesData();

        //меняем доступных рыб
        FindAvailableFishes(_currentHour);

        currentBait = _gameDataSystem.GetCurrentBaitData();
        SetCurrentProbabilitesWithBait(currentBait);
        //выбираем рандомную рыбу из доступных относительно вероятности
        int selectedIndex = FishIndexSelection(_currentProbabilities);
        FishData selectedFish = _availableFishes[selectedIndex];

        //возвращаем выбранную рыбу
        return selectedFish;
    }


    //метод для нахождения доступных рыб из всех
    private void FindAvailableFishes(int currentTimeHour)
    {
        //очищаем предыдущий лист
        _availableFishes.Clear();

        // проходим по всем рыбам и если рыба подходит кидаем в лист
        foreach (FishData fish in _fishList)
        {
            if ((fish.catchStartHour <= currentTimeHour) && (fish.catchEndHour >= currentTimeHour))
            {
                _availableFishes.Add(fish);
            }
        }
    }


    //метод расчитывающий вероятности выпадения относительно цен звданных рыб
    private List<float> RecalculateProbabilities()
    {
        /* ОПИСАНИЕ АЛГОРИТМА
        Алгоритм рассчитывает вероятности обратно стоимости рыб т.е. самая дорогая рыба будет самой редкой.
        Уравнение выглялит так - (1/а[1] + 1/a[2] + ... + 1/a[n]) = x, где a - цена рыбы, n - колличество рыб, 
        x - сглаживающий коэфициент. Далее вероятность каждой рыбы будет равна - " 1 / (a[i] * x) ", 
        где i номер рыбы в массиве.*/

        //создаем лист из цен доступных рыб
        List<float> costs = new List<float>();
        foreach (FishData fish in _availableFishes)
        {
            costs.Add(fish.rewardMoney);
        }

        //ищем сглаживающий коэффициент
        float smoothingCoefficient = 0f;
        for (int i = 0; i < costs.Count; i++)
        {
            smoothingCoefficient += 1 / costs[i];
        }

        //закладываем и возвращаем лист вероятностей
        for (int i = 0; i < costs.Count; i++)
        {
            costs[i] = 1 / (costs[i] * smoothingCoefficient);
        }

        return costs;
    }


    //метод рандомной выборки индекса с учетом вероятностей
    private int FishIndexSelection(List<float> probabilities)
    {
        /*ОПИСАНИЕ АЛГОРИТМА
         Так как у каждой рыбы в массиве есть своя вероятность, а сумма этих вероятностей равна 1,
         то можно представить их как отрезок длиной 1, состоящий из отрезков поменьше.
         Если задать некое ранодмное число между 0 и 1, то это число попадет в пределы некокго отрезка
         как раз относительно его длины, т.е. вероятности его выпадения. Для этого мы сначала задаем 
         это рандомное число а потом последовательно складываем коэффициенты, и когда число будет меньше 
         суммы, возвращаем коэффициент последней вероятности.*/

        //поле суммы коэффициентов
        float cumulativeProbability = 0;

        //случайное число
        float randomValue = Random.Range(0f, 1f);

        //находим нужный коэфициент по алгоритму, и возвращаем его
        for (int i = 0; i < probabilities.Count; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                return i;
            }
        }

        return probabilities.Count - 1;
    }


    //метод проверяющий прошло ли достаточно времени чтобы обновлять доступных рыб
    private bool IsTimeToUpdateAwailables()
    {
        //сравниваем текущее время и время для следуещего обновления
        int currentTime = _gameDataSystem.GetCurrentGameDayTimeInMinuties() / 60;

        if ((currentTime >= _updateTime && _updateTime != 0) || currentTime == 0)
        {
            //если больше, меняем следующее время обновления и возвращаем true
            ChangeNextUpdateTime();

            return true;
        }
        else
        {
            return false;
        }
    }


    //метод меняющий следующее время обновления доступных рыб
    private void ChangeNextUpdateTime()
    {
        //все рыбы в нашей базе имеют периоды активности строгов рамках таких временых дат 00:00, 06:00, 12:00, 00:00

        //текущее время
        int currentTime = _gameDataSystem.GetCurrentGameDayTimeInMinuties() / 60;

        if (currentTime >= 23)
        {
            currentTime = -1;
        }
        //назначаем следующее время для обновления

        _updateTime = currentTime + 1;
    }

    //метод возвращающий заданные вероятности вылова в зависимости от наживки

    private void SetCurrentProbabilitesWithBait(BaitData bait)
    {
        switch (bait.potential)
        {
            case 0:
                _currentProbabilities = new List<float>()
                    {0.8182636f, 0.1636527f, 0.01636527f, 0.001636527f, 0.00008182637f};
                break;
            case 1:
                _currentProbabilities = new List<float>()
                    {0.01636527f, 0.8182636f, 0.1636527f, 0.001636527f, 0.00008182637f};
                break;
            case 2:
                _currentProbabilities = new List<float>()
                    {0.001636527f, 0.1636527f, 0.8182636f, 0.01636527f, 0.00008182637f};
                break;
            case 3:
                _currentProbabilities = new List<float>()
                    {0.001636527f, 0.01636527f, 0.1636527f, 0.8182636f, 0.00008182637f};
                break;
            case 4:
                _currentProbabilities = new List<float>()
                    {0.00008182637f, 0.001636527f, 0.01636527f, 0.1636527f, 0.8182636f};
                break;
        }
    }
}
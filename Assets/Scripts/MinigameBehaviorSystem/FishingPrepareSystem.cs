using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class FishingPrepareSystem : MonoBehaviour
{
    //максимальное число забросов через которое обязательно должна выпасть не выловленая раннее рыба
    private const int MANDATORY_FISH_BORDER = 20;
    private int _mandatoryFishCounter = 0;
    
    [SerializeField] private List<FishData> _caughtFishes;
    [SerializeField] private List<FishData> _fishesNotCaughtYet;

    public FishData GetFishToCatch(List<FishData> fishList, int baitForce, int currentHour,
        int playerLevel)
    {
        _caughtFishes.Clear();
        _fishesNotCaughtYet.Clear();    

        foreach (FishData fish in fishList
                     .Where(fish => fish.catchStartHour <= currentHour && fish.catchEndHour >= currentHour)
                     .Where(fish => fish.level <= playerLevel))
        {
            if (fish.isCaught)
            {
                _caughtFishes.Add(fish);
            }
            else
            {
                _fishesNotCaughtYet.Add(fish);
            }
        }

        // ReSharper disable once PossibleLossOfFraction
        bool isRequiredFishPreviouslyCaught = Random.value > 1.1f - (1.0f / baitForce);

        if (!isRequiredFishPreviouslyCaught &&
            _mandatoryFishCounter > MANDATORY_FISH_BORDER)
        {
            _mandatoryFishCounter = 0;
            isRequiredFishPreviouslyCaught = true;
        }
        else
        {
            _mandatoryFishCounter += 1;
        }

        if (_caughtFishes.Count == 0 ||
            _caughtFishes.Count <= _fishesNotCaughtYet.Count)
        {
            isRequiredFishPreviouslyCaught = false;
        }
        
        if (_fishesNotCaughtYet.Count == 0)
        {
            isRequiredFishPreviouslyCaught = true;
        }

        FishData requiredFish = new();

        if (isRequiredFishPreviouslyCaught)
        {
            requiredFish = SelectRandomFish(_caughtFishes);
        }
        else
        {
            requiredFish = SelectRandomFish(_fishesNotCaughtYet);
        }


        return requiredFish;
    }


    private FishData SelectRandomFish(List<FishData> fishes)
    {
        if (fishes.Count == 0) return new FishData();
        // Находим максимальную стоимость для нормализации
        float maxCost = fishes.Max(f => f.rewardMoney);

        // Инвертируем стоимости и нормализуем их
        List<float> normalizedProbabilities = fishes.Select(f => maxCost / f.rewardMoney).ToList();

        // Считаем сумму всех нормализованных вероятностей
        float totalProbability = normalizedProbabilities.Sum();

        // Создаем кумулятивное распределение
        List<float> cumulativeProbabilities = new();
        float cumulativeSum = 0f;
        foreach (float probability in normalizedProbabilities)
        {
            cumulativeSum += probability / totalProbability;
            cumulativeProbabilities.Add(cumulativeSum);
        }

        // Генерируем случайное число от 0 до 1
        float randomValue = Random.value;

        // Выбираем рыбу на основе случайного числа и кумулятивного распределения
        for (int i = 0; i < cumulativeProbabilities.Count; i++)
        {
            if (randomValue <= cumulativeProbabilities[i])
            {
                return fishes[i];
            }
        }

        // На всякий случай, если что-то пошло не так, возвращаем последнюю рыбу
        return fishes[^1];
    }
}
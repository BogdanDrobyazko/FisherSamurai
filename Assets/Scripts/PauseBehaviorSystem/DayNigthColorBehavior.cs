using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;


public class DayNigthColorBehavior : MonoBehaviour
{
    [SerializeField] private List<Image> _images;
    [SerializeField] private GameDataSystem _gameDataSystem;


    [Header("RGBForNight")] [SerializeField]
    private float _R;

    [SerializeField] private float _G;
    [SerializeField] private float _B;

    private int dayTimeInMinutes;

    void Update()
    {
        dayTimeInMinutes = _gameDataSystem.GetCurrentGameDayTimeInMinuties();

        switch (dayTimeInMinutes)
        {
            case >= 0 and <= 540:
            {
                float deltaTime = ((float) dayTimeInMinutes) / 540;

                ChangeColorInImagesWithDelta(deltaTime);

                break;
            }
            case >= 720 and <= 960:
            {
                foreach (var image in _images)
                {
                    image.color = new Color(1, 1, 1);
                }

                break;
            }
            case >= 960 and <= 1440:
            {
                float deltaTime = (1440 - (float) dayTimeInMinutes) / 480;

                ChangeColorInImagesWithDelta(deltaTime);

                break;
            }
        }
    }

    private void ChangeColorInImagesWithDelta(float delta)
    {
        float r = (_R + ((255 - _R) * delta)) / 255;
        float g = (_G + ((255 - _G) * delta)) / 255;
        float b = (_B + ((255 - _B) * delta)) / 255;

        foreach (var image in _images)
        {
            image.color = new Color(r, g, b);
        }
    }
}
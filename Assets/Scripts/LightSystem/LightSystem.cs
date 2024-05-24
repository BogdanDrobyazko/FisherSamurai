using System.Collections.Generic;
using UnityEngine;

public class LightSystem : MonoBehaviour
{
    [SerializeField] private List<Light> _lights;
    [SerializeField] private Light _sunLight;
    [SerializeField] private GameDataSystem _gameDataSystem;
    [SerializeField] private float a;

    private int dayTimeInMinutes;


    // Update is called once per frame
    void Update()
    {
        dayTimeInMinutes = _gameDataSystem.GetCurrentGameDayTimeInMinuties();

        switch (dayTimeInMinutes)
        {
            case >= 0 and <= 540:
            {
                float deltaTime = ((float) dayTimeInMinutes) / 540;

                _sunLight.intensity = deltaTime;

                _sunLight.color = new Color(1, (0.5f + deltaTime / 2), deltaTime);
                break;
            }
            case >= 540 and <= 960:
            {
                _sunLight.intensity = 1;

                _sunLight.color = new Color(1, 1, 1);
                break;
            }
            case >= 960 and <= 1440:
            {
                float deltaTime = (1440 - (float) dayTimeInMinutes) / 480;

                _sunLight.intensity = deltaTime;

                _sunLight.color = new Color(1, (0.5f + deltaTime / 2), deltaTime);
                break;
            }
        }

        foreach (var light in _lights)
        {
            switch (dayTimeInMinutes)
            {
                case >= 0 and <= 420:
                {
                    light.intensity = 1;

                    break;
                }
                case >= 420 and < 430:
                {
                    light.intensity -= 0.1f * Time.deltaTime;

                    break;
                }

                case >= 430 and <= 1080:
                {
                    light.intensity = 0;

                    break;
                }
                case >= 1080 and < 1085:
                {
                    light.intensity += 0.2f * Time.deltaTime;

                    break;
                }
                case >= 1085 and <= 1440:
                {
                    light.intensity = 1;

                    break;
                }
            }
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsTMP; // Объект текста для вывода FPS на экран

    [SerializeField] private float _fpsShowDelayTime;
    private float _deltaTime = 0.5f;
    private float _fps;
    private float delayTime;

    void Update()
    {
        // Вычисляем время прошедшее между кадрами
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;

        // Вычисляем FPS
        float _fps = 1.0f / _deltaTime;
        
        delayTime += Time.deltaTime;

        if (_fpsShowDelayTime <= delayTime)
        {
            delayTime = 0;
            
            // Форматируем строку для отображения
            string text = $"{_fps:0.}";
            // Выводим значение FPS на экран
            _fpsTMP.text = text;
        }
    }

   
}
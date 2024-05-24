using UnityEngine;
using UnityEngine.UI;

public class CameraBehavior : MonoBehaviour
{
    /* ОПИСАНИЕ ПОВЕДЕНИЯ КАМЕРЫ
     * Эталонное разрешение игры это 1980:1020 (вертикальная проекция), если у устройства разрешение по вертикали
     * больше, то должны увеличиваться области сверху и снизу экрана, а условный размер кота, не изменяться, а если
     * у устройства разрешение больше по горизонтали, тогда должны увеличиваться области слева и справа. 
     */

    [SerializeField] private float initialAspectRatio = 9f / 16f; // Эталонное соотношение сторон экрана
    [SerializeField] private float _cameraSizeBase;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _currentAspectRatio;
    [SerializeField] private CanvasScaler[] canvasScalers;

    void Start()
    {
        _cameraSizeBase = _camera.orthographicSize;
        UpdateCameraAspect();
    }


    private void Update()
    {
        UpdateCameraAspect();
    }

    private void UpdateCameraAspect()
    {
        // Рассчитываем текущее соотношение сторон экрана
        _currentAspectRatio = (float) Screen.width / (float) Screen.height;
        // Если текущее соотношение сторон меньше эталонного, то увеличиваем высоту области игры
        if (_currentAspectRatio <= initialAspectRatio)
        {
            _camera.orthographicSize = initialAspectRatio * initialAspectRatio * 10 / _currentAspectRatio;
        }
        // Если текущее соотношение сторон больше начального, то уменьшаем ширину области игры
        if (_currentAspectRatio > initialAspectRatio)
        {
            _camera.orthographicSize = _cameraSizeBase;
        }
    }
}
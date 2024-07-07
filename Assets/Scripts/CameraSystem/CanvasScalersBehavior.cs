using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static System.TimeZoneInfo;

namespace CameraSystem
{
    public class CanvasScalersBehavior : MonoBehaviour
    {
        [SerializeField] private float _currentAspectRatio;
        [SerializeField] private CanvasScaler[] _canvasScalers;

        [Header("Плавное увеличение UI при переходе через границу разрешения экрана")] 
        [SerializeField] private float _switchScaleModeBorder = 0.65f;

        [SerializeField] private float _containSizeStart = 0.8f;
        [SerializeField] private float _containSizeEnd = 0.8f;
        [SerializeField] private float _lerpTime;

        [SerializeField] private float _lastAspectRatio;
        private float _t = 0.0f;

        private bool _transitionDone = false;
        private float _transitionTimer = 0.0f;

        private void Update()
        {
            _currentAspectRatio = (float) Screen.width / Screen.height;
            if ((Math.Abs(_currentAspectRatio - _lastAspectRatio) > 0.01f) && !_transitionDone)
            {
                _lastAspectRatio = _currentAspectRatio;
                UpdatePauseFieldCanvasScaler();
            }
        }

        private void UpdatePauseFieldCanvasScaler()
        {
            foreach (var canvasScaler in _canvasScalers)
            {
                if (_currentAspectRatio < _switchScaleModeBorder)
                {
                    canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                }
                else
                {
                    canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
                    
                    if (_t < 1)
                    {
                        _transitionDone = false;
                        _transitionTimer += Time.deltaTime;
                        _t = Mathf.Clamp01(_transitionTimer / _lerpTime);
                        float currentValue = Mathf.Lerp(_containSizeStart, _containSizeEnd, _t);
                        canvasScaler.scaleFactor = currentValue * ((float)Screen.height / 1080);
                    }
                    else
                    {
                        _t = 0;
                        _transitionDone = true;
                    }

                }
            }
        }
    }
}

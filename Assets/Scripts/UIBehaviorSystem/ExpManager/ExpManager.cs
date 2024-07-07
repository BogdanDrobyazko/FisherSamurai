using System.Collections.Generic;
using Kilosoft.Tools;
using TMPro;
using UnityEngine;

namespace UIBehaviorSystem
{
    public class ExpManager : MonoBehaviour
    {
        [SerializeField] private EventBus _eventBus;
        [SerializeField] private ExpFill _expFill;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private List<int> _expThresholds;
        [SerializeField] private int _debugExp;
        private int _currentLevel;
        
        private void Awake()
        {
            _eventBus.GetComponent<IReadOnlyEventBus>().OnExpChanged.AddListener(UpdateExp);
        }

        [EditorButton("Update Exp")]
        public void UpdateExp() => UpdateExp(_debugExp);
        private void UpdateExp(int currentExp)
        {
            int operationLevel = 0;
            
            if (currentExp < _expThresholds[^1])
            {
                for (int i = 0; i < _expThresholds.Count; i++)
                {
                    if (currentExp >= _expThresholds[i]) continue;
                    operationLevel = i - 1;
                    break;
                }

                int actualPoint = _expThresholds[operationLevel];
                int targetPoint = _expThresholds[operationLevel + 1];
                
                float delta = (float)(currentExp - actualPoint) / (targetPoint - actualPoint);

                _expFill.SetFillPosition(delta);
            }
            else
            {
                operationLevel = _expThresholds.Count - 1;
                
                _expFill.SetFillPosition(1);
            }
            _levelText.text = operationLevel.ToString();
            
            if(operationLevel != _currentLevel)
            {
                _currentLevel = operationLevel;
                _eventBus.GetComponent<EventBus>().OnLevelChanged.Invoke(_currentLevel);
            }
        }
    }
}
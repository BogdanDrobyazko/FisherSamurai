using UnityEngine;

namespace UIBehaviorSystem
{
    public class ExpManager : MonoBehaviour
    {
        [SerializeField] private EventBus _eventBus;
        [SerializeField] private GameObject _expBar;
        [SerializeField] private GameObject _expBarFill;
        [SerializeField] private GameDataSystem _gameDataSystem;
        
        private int _currentExp;
        private int _currentLevel;
        private float _expToNextLevelDelta;
        
        private void Awake()
        {
            _eventBus.GetComponent<IReadOnlyEventBus>().OnExpChanged.AddListener(UpdateExp);
        }

        private void UpdateExp(int newAmount)
        {
            _expBarFill.transform.localScale = new Vector3(newAmount / 100f, 1, 1);
        }
    }
    
    
}
using UnityEngine;

 
    public class BaseGameDataPrefab : MonoBehaviour
    {
        [SerializeField] private GameData _baseGameData;

        public GameData GetBaseGameData()
        {
            return _baseGameData;
        }
    }
 
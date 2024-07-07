using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoBehaviour
    {
        private readonly SaveDataSystem _saveDataSystem = new();
        [SerializeField] private GameData _baseGameData;
        private GameData _currentGameData;

        public void Awake()
        {
            //проверка наличия файла gameData.json, если его нет, то создаем его
            _currentGameData = _saveDataSystem.LoadCurrentGameData();
            if (!_currentGameData.isGameDataExist)
            {
                _currentGameData = _baseGameData;
                _saveDataSystem.SaveCurrentGameData(_currentGameData);
                Debug.Log(name + ": Game data not found, game data was created");
            }
            else
            {
                Debug.Log(name + ": Game data found");
            }
            
            //включаем сцену game
            SceneManager.LoadScene("GameScene");

        }
    }
}
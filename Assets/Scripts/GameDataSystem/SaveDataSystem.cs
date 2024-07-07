using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveDataSystem 
{
    public GameData LoadBaseGameData()
    {
        return LoadFromJson("baseGameData.json");
    }

    public void SaveBaseGameData(GameData baseGameData)
    {
        SaveInJson(baseGameData, "baseGameData.json");
    }

    public GameData LoadCurrentGameData()
    {
        return LoadFromJson("gameData.json");
    }

    public void SaveCurrentGameData(GameData currentGameData)
    {
        SaveInJson(currentGameData, "gameData.json");
        //SaveInDat(currentGameData, "gameData.dat");
    }

    private void SaveInJson(GameData gameData, string fileName)
    {
        // Преобразуем список рыб в JSON-строку
        string json = JsonConvert.SerializeObject(gameData);

       
        // Сохраняем JSON-строку в файл по пути persistentDataPath/fishdata.json
        File.WriteAllText(Application.persistentDataPath + "/" + fileName, json);

        //дебаг
        Debug.Log("JSON Game data saved in " + Application.persistentDataPath + "/" + fileName);
    }

    private GameData LoadFromJson(string fileName)
    {
        GameData gameData = new GameData();

        // Загружаем JSON-строку из файла по пути persistentDataPath/fishdata.json
        string filePath = Application.persistentDataPath + "/" + fileName;

        if (!File.Exists(filePath))
        {
            SaveInJson(gameData, fileName);
            Debug.Log("Cannot load "  + fileName + ". Creating new one");
        }

        string json = File.ReadAllText(filePath);
        // Преобразуем JSON-строку в список рыб
        gameData = JsonConvert.DeserializeObject<GameData>(json);


        Debug.Log("Game Data loaded from JSON"); //дебаг

        return gameData;
    }

    private void SaveInDat(GameData gameData, string fileName)
    {
        string filePath = Application.persistentDataPath + "/" + fileName;
        BinaryFormatter binaryFormatter = new BinaryFormatter(); //создаем бинарный форматер
        FileStream dataFileStream = File.Create(filePath); //создаем потоковый файл сохранения в заданной директории
        binaryFormatter.Serialize(dataFileStream, gameData); //сериализуем текущие значения в потоковом файл 
        dataFileStream.Close(); //закрываем поток
        Debug.Log("DAT Game data saved in " + filePath); //дебаг
    }

    private GameData LoadFromDat(string fileName)
    {
        GameData gameData = new GameData();

        // Загружаем бинарную-строку из файла по пути persistentDataPath/fishdata.json
        string filePath = Application.persistentDataPath + "/" + fileName;
        
        if (!File.Exists(filePath)) //проверяем существуют ли данные для загрузки
        {
            SaveInDat(gameData, fileName);
            Debug.Log("File not found");
        }
        
        BinaryFormatter binaryFormatter = new BinaryFormatter(); //создаем бинарный форматер
        FileStream dataFileStream = File.Open(filePath, FileMode.Open); //открываем текущий потоковый файл в заданной директории
        gameData = (GameData) binaryFormatter.Deserialize(dataFileStream); //десериализуем данные в формате FishData
        dataFileStream.Close(); //закрываем поток
        
        Debug.Log("Game data loaded from DAT"); //дебаг

        return gameData; //возвращаем данные
    }
}


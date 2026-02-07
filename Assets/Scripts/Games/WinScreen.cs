using System;
using System.IO;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    private GameSettings gameSettings;
    private SaveData.GameData newData;
    [HideInInspector] public SaveData allData = new();
    private string path;

    private void Start()
    {
        gameSettings = GameController.Instance.gameSettings;

        DateTime today = DateTime.Now;
        newData = new()
        {
            score = GameController.Instance.balance,
            rank = 4,
            date = today.Date.ToString("d")
        };

        switch (gameSettings.saveSettings.savingMode)
        {
            case GameSettings.SavingModes.json:
                path = Application.persistentDataPath + gameSettings.saveSettings.JSONPath;
                FetchFromJson();
                SaveAsJson();
                break;

            case GameSettings.SavingModes.api:
                Debug.LogWarning("Not Implemented");
                break;
        }
    }

    private void FetchFromJson()
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("No JSON file found");
            return;
        }

        string loadedData = File.ReadAllText(path);
        allData = JsonUtility.FromJson<SaveData>(loadedData);
    }

    private void SaveAsJson()
    {
        DateTime today = DateTime.Now;
        SaveData.GameData newData = new()
        {
            score = GameController.Instance.balance,
            rank = 4,
            date = today.Date.ToString("d")
        };

        allData.data.Add(newData);

        string jsonData = JsonUtility.ToJson(allData);
        File.WriteAllText(path, jsonData);
    }

    public void Scoreboard()
    {
        print(newData.rank);
    }
}

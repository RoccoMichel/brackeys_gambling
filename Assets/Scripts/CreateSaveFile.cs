using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateSaveFile : MonoBehaviour
{
    public GameSettings settings;
    private SaveData newSaveFile = new();
    private SaveData.GameData devScore = new()
    {
        date = "Dev Score",
        rank = 1,
        score = 69102 // change to whatever
    };

    void Start()
    {
        if (settings.saveSettings.savingMode != GameSettings.SavingModes.json) return;

        string path = Application.persistentDataPath + settings.saveSettings.JSONPath;
        if (!File.Exists(path))
        {
            newSaveFile.data.Add(devScore);

            string jsonData = JsonUtility.ToJson(newSaveFile);
            File.WriteAllText(path, jsonData);
        }    
    }
}
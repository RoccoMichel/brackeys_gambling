using System;
using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text playerDisplay;
    [SerializeField] private TMP_Text[] podiumDisplay = new TMP_Text[3];
    [SerializeField] private SpriteRenderer[] podiumProfiles = new SpriteRenderer[3];

    // local variables
    private GameSettings gameSettings;
    private SaveData.GameData newData;
    [HideInInspector] public SaveData currentSave = new();
    private string path;
    private int playerRank = 1;
    private bool includePlayer;

    private void Start()
    {
        gameSettings = GameController.Instance.gameSettings;
        includePlayer = GameController.Instance.balance == 0;
        GameController.Instance.canvas.HideBetMenu();
        
        if (gameSettings.moreSettings.showPodium) SetUpPodium(); // Podium has a button for ContinueWithSaving
        else ContinueWithSaving();
    }

    private void SetUpPodium()
    {
        // Get podium winners
        Opponent player = new()
        {
            balance = GameController.Instance.balance,
            profile = GameController.Instance.gameSettings.moreSettings.playerProfile
        };
        List<Opponent> results = GameController.Instance.opponentList;
        results.Add(player); // ADD PLAYER SCORE
        results.Sort((x, y) => y.balance.CompareTo(x.balance));
        playerRank = results.IndexOf(player); // FIND PLAYER RANK

        // Display podium winners
        try
        {
            for (int i = 0; i < 3; i++)
            {
                podiumDisplay[i].text += $"\n{results[i].balance}";
                podiumProfiles[i].sprite = results[i].profile;
            }
        }
        catch { /*Less than 3 Participants*/ }
        playerDisplay.text = GameController.Instance.balance.ToString();

        if (results.Count > 3 && playerRank > 3) playerDisplay.text = $"YOUR SCORE: {results[playerRank].balance}";
        else playerDisplay.text = string.Empty;
    }

    public void ContinueWithSaving()
    {
        DateTime today = DateTime.Now;
        newData = new()
        {
            score = GameController.Instance.balance,
            rank = playerRank,
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

        if (gameSettings.moreSettings.showLeaderboard) // show leaderboard or go back to main menu
        {
            InstantiateLeaderboard();
            Destroy(gameObject); // Podium obscures otherwise
        }
        else SceneManager.LoadScene(0);
    }

    private void InstantiateLeaderboard()
    {
        if (!gameSettings.moreSettings.showLeaderboard) return;

        SaveData.GameData[] leaderboardData = new SaveData.GameData[11];
        int playerRank = currentSave.data.IndexOf(newData);
        int highlightIndex = playerRank;

        for(int i = 0; i < 11; i++)
        {
            if (playerRank > 10 && i == 10)
            {
                leaderboardData[i] = currentSave.data[playerRank];
                highlightIndex = 10;
                break;
            }

            leaderboardData[i] = currentSave.data[i];
        }

        Instantiate((GameObject)Resources.Load("UI/Leaderboard"), GameController.Instance.canvas.transform)
                .GetComponent<Leaderboard>().InstantiateList(leaderboardData, highlightIndex, playerRank);
        
    }
    private void FetchFromJson()
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("No JSON file found");
            SceneManager.LoadScene(0);
            return;
        }

        string loadedData = File.ReadAllText(path);
        currentSave = JsonUtility.FromJson<SaveData>(loadedData);
    }

    private void SaveAsJson()
    {
        if (!includePlayer) return; // Do not save current player!

        DateTime today = DateTime.Now;
        SaveData.GameData newData = new()
        {
            score = GameController.Instance.balance,
            rank = 4,
            date = today.Date.ToString("d")
        };

        currentSave.data.Add(newData);
        currentSave.data.Sort((x, y) => y.score.CompareTo(x.score));

        string jsonData = JsonUtility.ToJson(currentSave);
        File.WriteAllText(path, jsonData);
    }
}

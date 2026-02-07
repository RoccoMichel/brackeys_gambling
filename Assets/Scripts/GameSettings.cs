using UnityEngine;

[CreateAssetMenu(fileName = "New GameSettings", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    public enum Difficulties { easy, medium, hard}
    public enum SavingModes { json, api }

    [System.Serializable]
    public struct RoundSettings
    {
        [Tooltip("Affects: Trivia, Weather")]
        public Difficulties difficulty;
        public int enemyCount;
        public int gamesPerRound;
        public int playerStartMoney;
    }
    [System.Serializable]
    public struct SaveSettings
    {
        public SavingModes savingMode;
        [Space(30)]
        public string fetchScoresURI;
        public string saveScoresURI;
        [Space(30)]
        public string JSONPath;
    }

    // Instances with default values
    [Space(10)]
    public RoundSettings roundSettings = new()
    {
        difficulty = Difficulties.easy,
        enemyCount = 6,
        gamesPerRound = 10,
        playerStartMoney = 1000,
    };
    [Space(20)] public SaveSettings saveSettings = new() { JSONPath="/SaveJSON.LiveLoveGamble"};
    [Space(20), TextArea] public string devNotes;
}

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
        public int opponentCount;
        [Tooltip("In Percent")]
        public int opponentBettingRangeFromPlayer;
        public int gamesPerRound;
        public int startBalance;
        public bool doubleOrNothingQuery;
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
    [System.Serializable]
    public struct MoreSettings
    {
        public bool showPodium;
        public bool showLeaderboard;
        public Sprite playerProfile;
        public Sprite[] opponentProfiles;
    }

    // Instances with default values
    [Space(10)]
    public RoundSettings roundSettings = new()
    {
        difficulty = Difficulties.easy,
        opponentCount = 6,
        opponentBettingRangeFromPlayer = 25,
        gamesPerRound = 10,
        startBalance = 1000,
        doubleOrNothingQuery = true
    };
    [Space(20)] public SaveSettings saveSettings = new() { JSONPath="/SaveJSON.LiveLoveGamble"};
    [Space(20)] public MoreSettings moreSettings = new();
    [Space(20), TextArea] public string devNotes;
}

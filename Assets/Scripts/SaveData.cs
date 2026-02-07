using System.Collections.Generic;
public class SaveData
{
    public List<GameData> data = new();
    [System.Serializable]
    public struct GameData
    {
        public int score;
        public int rank;
        public string date;
    }
}

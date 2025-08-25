using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public CanvasManager canvas;
    public List<Player> PlayerList = new();
    private void Awake()
    {
        if(Instance != null) { Destroy(gameObject); }
        Instance = this;
    }

    private void Start()
    {
        NewRace();
    }

    public void RaceFinish(int result)
    {
        foreach(Player p in PlayerList)
        {
            if (p.selection == result) p.Win();
            else p.Lose();
        }
    }

    public void NewRace()
    {
        canvas.GenerateButtons(4);
    }

    public void StartRace()
    {
        canvas.ClearButtons();
    }
}

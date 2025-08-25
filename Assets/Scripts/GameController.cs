using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    public int optionsCount = 4;
    public static GameController Instance { get; private set; }
    public CanvasManager canvas;
    public List<Opponent> OpponentList = new();
    public Player player;

    [SerializeField] private GameObject snailRacePrefab;
    private void Awake()
    {
        if(Instance != null) { Destroy(gameObject); }
        Instance = this;
    }

    private void Start()
    {
        GenerateOpponents(4);
        NewRace();
    }

    public void RaceFinish(int result)
    {
        foreach(Opponent o in OpponentList)
        {
            if (o.selection == result) o.Win();
            else o.Lose();
        }

        if (player.selection == result) player.Win();
    }

    public void NewRace()
    {
        canvas.GenerateButtons(optionsCount);

        
    }

    public void StartRace()
    {
        canvas.ClearButtons();
        Instantiate(snailRacePrefab);
    }

    public void GenerateOpponents(int count)
    {
        for (int i = 0; i < count; i++)
        {
            OpponentList.Add(Instantiate(Resources.Load("Opponent"), canvas.opponentParent).GetComponent<Opponent>());
        }
    }
}

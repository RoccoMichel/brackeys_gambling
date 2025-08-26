using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    public GameModes gameMode;
    public bool inGame;
    public int optionsCount = 4;
    public static GameController Instance { get; private set; }
    public CanvasManager canvas;
    public List<Opponent> OpponentList = new();
    public Player player;

    [SerializeField] private GameObject snailRacePrefab;
    private SnailManager snailManager;

    public enum GameModes { SnailRace, }

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); }
        Instance = this;
    }

    private void Start()
    {
        player.balance = 500;
        GenerateOpponents(4);
        NewRace();
    }

    // Snail Race Related

    public void NewRace()
    {
        foreach (var o in OpponentList) o.ChooseBet();
        canvas.GenerateButtons(optionsCount);

        switch (gameMode)
        {
            case GameModes.SnailRace:
        
                snailManager = Instantiate(snailRacePrefab).GetComponentInChildren<SnailManager>();
                break;
        }
    }
    public void StartRace()
    {
        inGame = true;
        canvas.ClearButtons();


        //very good
        switch (gameMode)
        {
            case GameModes.SnailRace:

                snailManager.StartRace();
                break;
        }
    }
    public void RaceFinish(int result)
    {
        inGame = false;

        foreach (Opponent o in OpponentList)
        {
            if (o.selection == result) o.Win();
            else o.Lose();
        }

        if (player.selection == result)
        {
            player.Win();
            Destroy(Instantiate(Resources.Load("Confetti")), 4);
            canvas.SetBalanceColor(Color.green);
        }
        else
        {
            player.Lose();
            canvas.SetBalanceColor(Color.red);
        }

        NewRace();
    }

    // Opponent Related
    public void GenerateOpponents(int count)
    {
        for (int i = 0; i < count; i++)
        {
            OpponentList.Add(Instantiate(Resources.Load("Opponent"), canvas.opponentParent).GetComponent<Opponent>());
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public bool inGame;
    private int previousGame;
    internal Minigame currentGame;
    public GameObject[] games;
    public CanvasManager canvas;
    public List<Opponent> OpponentList = new();

    [Header("Player Values")]
    public int balance = 1000;
    public int bet;
    public int choice;
    [HideInInspector] public int gamesPlayed;

    private InputAction increase1;
    private InputAction increase10;
    private InputAction increase100;
    private InputAction decrease1;
    private InputAction decrease10;
    private InputAction decrease100;
    private InputAction max;
    private InputAction min;

    public void Win()
    {
        //Maybe we could have different win multipliers for different games ie some games are easier to win but has a lower reward and vice versa
        balance += bet * 2;
        bet = Mathf.Clamp(bet, 1, balance);
    }

    public void Lose()
    {
        balance -= bet;
        bet = Mathf.Clamp(bet, 1, balance);

        if (balance <= 0) SceneManager.LoadScene("GameOver");
    }
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); }
        Instance = this;
        bet = balance/2;
    }

    // Fixed the stupid ass input system - Rocco
    private void Start()
    {
        increase1 = InputSystem.actions.FindAction("increase1");
        increase10 = InputSystem.actions.FindAction("increase10");
        increase100 = InputSystem.actions.FindAction("increase100");
        decrease1 = InputSystem.actions.FindAction("decrease1");
        decrease10 = InputSystem.actions.FindAction("decrease10");
        decrease100 = InputSystem.actions.FindAction("decrease100");
        min = InputSystem.actions.FindAction("min");
        max = InputSystem.actions.FindAction("max");
        
        NewGame();
    }

    private void Update()
    {
        // Felix Invention
        if (inGame) return;
        // Adjusting Bet Amount based on player Input 
        if (increase1.WasPressedThisFrame()) bet += 1;
        else if (increase100.WasPressedThisFrame()) bet += 100;
        else if (increase10.WasPressedThisFrame()) bet += 10;
        // Order is important for modifier keys to work!
        if (decrease1.WasPressedThisFrame()) bet -= 1;
        else if (decrease100.WasPressedThisFrame()) bet -= 100;
        else if (decrease10.WasPressedThisFrame()) bet -= 10;
        if (min.WasPressedThisFrame()) bet = 0;
        if (max.WasPressedThisFrame()) bet = balance;

        bet = Mathf.Clamp(bet, 1, balance);
    }

    // Startar nytt spel och väljer ut ett slumpmässigt minigame - Felix
    public void NewGame()
    {
        if (gamesPlayed >= 10)
        {
        
             SceneManager.LoadScene("WinScreenScene");

            //Compare to opponents 

            // Upload results


        }
        int nextGame = previousGame;
        while (nextGame == previousGame)
        {
            previousGame = Random.Range(0, games.Length);
            if (games.Length == 1) break;
        }
        currentGame = Instantiate(games[nextGame]).GetComponent<Minigame>();
    }
    public void StartGame()
    {
        if (bet <= 0)
        {
            Debug.LogWarning("Player bet is zero or below!");
            return;
        }
       

        inGame = true;
        canvas.ClearButtons();
        currentGame.GameStart();
    }

    // Rocco
    public void GameFinish(int result)
    {
        inGame = false;
        gamesPlayed++;

        foreach (Opponent o in OpponentList)
        {
            if (o.choice == result) o.Win();
            else o.Lose();
        }

        if (choice == result)
        {
            Win();
            Destroy(Instantiate(Resources.Load("Confetti")), 4);
            canvas.SetBalanceColor(Color.green);
        }
        else
        {
            Lose();
            canvas.SetBalanceColor(Color.red);
        }

        NewGame();
    }

    public void IncreaseBet(int amount)
    {
        //Stopar dig från att betta mitt i spelet - Felix
        if (inGame) return;
        bet += amount;
        bet = Mathf.Clamp(bet, 1, balance);
    }

    // Opponent Related - Rocco
    public void GenerateOpponents(int count)
    {
        for (int i = 0; i < count; i++)
        {
            OpponentList.Add(Instantiate((GameObject)Resources.Load("Opponent"), canvas.opponentParent).GetComponent<Opponent>());
        }
    }
}

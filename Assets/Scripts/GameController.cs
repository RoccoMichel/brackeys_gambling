using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] gameModes;
    public bool inGame;
    public int optionsCount = 4;
    public static GameController Instance { get; private set; }
    public CanvasManager canvas;
    public List<Opponent> OpponentList = new();

    [SerializeField] private GameObject snailRacePrefab;
    private SnailManager snailManager;
    public int balance = 1000;
    public int bet;
    public int selection;

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
        bet = Mathf.CeilToInt(balance / 2);
    }

    public void Lose()
    {
        balance -= bet;
        bet = Mathf.CeilToInt(balance / 2);

        if (balance <= 0) SceneManager.LoadScene("GameOver");
    }
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); }
        Instance = this;
    }

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

        balance = 500;
        GenerateOpponents(4);
        NewGame();
    }

    private void Update()
    {
        if (increase1.WasPressedThisFrame()) bet += 1;
        else if (increase100.WasPressedThisFrame()) bet += 100;
        else if (increase10.WasPressedThisFrame()) bet += 10;
        if (decrease1.WasPressedThisFrame()) bet -= 1;
        else if (decrease100.WasPressedThisFrame()) bet -= 100;
        else if (decrease10.WasPressedThisFrame()) bet -= 10;
        if (min.WasPressedThisFrame()) bet = 0;
        if (max.WasPressedThisFrame()) bet = balance;

        bet = Mathf.Clamp(bet, 0, balance);
    }

    // Snail Race Related

    public void NewGame()
    {

    }
    public void StartGame()
    {
        inGame = true;
        canvas.ClearButtons();
    }
    public void GameFinish(int result)
    {
        inGame = false;

        foreach (Opponent o in OpponentList)
        {
            if (o.selection == result) o.Win();
            else o.Lose();
        }

        if (selection == result)
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

    // Opponent Related
    public void GenerateOpponents(int count)
    {
        for (int i = 0; i < count; i++)
        {
            OpponentList.Add(Instantiate(Resources.Load("Opponent"), canvas.opponentParent).GetComponent<Opponent>());
        }
    }
}

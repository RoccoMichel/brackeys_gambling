using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TMP_Text balanceDisplay;
    [SerializeField] private TMP_Text betDisplay;
    [SerializeField] private Transform buttonParent;
    [SerializeField] public Transform opponentParent;
    public Player player;
    public List<GameObject> Buttons = new();

    private void FixedUpdate()
    {
        betDisplay.text = $"Bet: ${player.bet}";
        balanceDisplay.text = $"${player.balance}";

        balanceDisplay.color = Color.Lerp(balanceDisplay.color, Color.white, 0.03f);
    }

    public void GenerateButtons(int amount)
    {
        for (int i = 1; i < amount + 1; i++)
        {
            Buttons.Add(Instantiate((GameObject)Resources.Load("Bet Button"), buttonParent));
            Buttons[i - 1].GetComponent<BetButton>().selection = i;
            Buttons[i - 1].GetComponent<BetButton>().message = $"Bet on Snail #{i}";
        }
    }

    public void SetBalanceColor(Color color)
    {
        balanceDisplay.color = color;
    }

    public void ClearButtons()
    {
        foreach (GameObject gameObject in Buttons) { Destroy(gameObject); }
        Buttons.Clear();
    }

    public void IncreaseBet()
    {
        if (GameController.Instance.inGame) return;

        if (player.bet == 1) player.bet = 10;
        else player.bet = Mathf.Clamp(player.bet += 10, 1, player.balance);
        betDisplay.text = $"Bet: ${player.bet}";
    }

    public void DecreaseBet()
    {
        if (GameController.Instance.inGame) return;

        player.bet = Mathf.Clamp(player.bet -= 10, 1, player.balance);
        betDisplay.text = $"Bet: ${player.bet}";
    }

    public void MaxBet()
    {
        if (GameController.Instance.inGame) return;

        player.bet = player.balance;
        betDisplay.text = $"Bet: ${player.bet}";
    }
}

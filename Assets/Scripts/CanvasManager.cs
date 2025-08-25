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

    private void Update()
    {
        betDisplay.text = $"Bet: ${player.bet}";
        balanceDisplay.text = $"${player.balance}";
    }

    public void GenerateButtons(int amount)
    {
        for (int i = 1; i < amount + 1; i++)
        {
            Buttons.Add(Instantiate((GameObject)Resources.Load("Bet Button"), buttonParent));
            Buttons[i - 1].GetComponent<BetButton>().selection = i;
        }
    }

    public void ClearButtons()
    {
        foreach (GameObject gameObject in Buttons) { Destroy(gameObject); }
        Buttons.Clear();
    }

    public void IncreaseBet()
    {
        if (GameController.Instance.inRace) return;

        if (player.bet == 1) player.bet = 10;
        else player.bet = Mathf.Clamp(player.bet += 10, 1, player.balance);
        betDisplay.text = $"Bet: ${player.bet}";
    }

    public void DecreaseBet()
    {
        if (GameController.Instance.inRace) return;

        player.bet = Mathf.Clamp(player.bet -= 10, 1, player.balance);
        betDisplay.text = $"Bet: ${player.bet}";
    }

    public void AllIn()
    {
        if (GameController.Instance.inRace) return;

        player.bet = player.balance;
        betDisplay.text = $"Bet: ${player.bet}";
    }
}

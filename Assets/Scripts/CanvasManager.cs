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
    List<GameObject> Buttons = new();

    private void Start()
    {
        RefreshCanvas();
        betDisplay.text = $"Bet: ${player.bet}";
    }

    public void RefreshCanvas()
    {
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
}

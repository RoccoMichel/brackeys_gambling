using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TMP_Text balanceDisplay;
    [SerializeField] private TMP_Text betDisplay;
    [SerializeField] private Transform buttonParent;
    [SerializeField] public Transform opponentParent;
    public List<GameObject> Buttons = new();

    private void FixedUpdate()
    {
        betDisplay.text = $"Bet: ${GameController.Instance.bet}";
        balanceDisplay.text = $"${GameController.Instance.balance}";

        balanceDisplay.color = Color.Lerp(balanceDisplay.color, Color.white, 0.03f);
    }

    public void InstantiateButtons(int amount, string message)
    {
        ClearButtons();
        for (int i = 1; i < amount + 1; i++)
        {
            Buttons.Add(Instantiate((GameObject)Resources.Load("Bet Button"), buttonParent));
            Buttons[i - 1].GetComponent<BetButton>().selection = i;
            Buttons[i - 1].GetComponent<BetButton>().messageDisplay.text = $"{message} #{i}";
        }
    }

    public void SetBalanceColor(Color color)
    {
        balanceDisplay.color = color;
    }

    public PopText InstantiatePopText()
    {
        return Instantiate((GameObject)Resources.Load("Winner-Text"), transform).GetComponent<PopText>();
    }

    public void ClearButtons()
    {
        foreach (GameObject gameObject in Buttons) { Destroy(gameObject); }
        Buttons.Clear();
    }
}

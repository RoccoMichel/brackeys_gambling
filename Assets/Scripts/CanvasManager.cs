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
    private GameObject popText;

    private void FixedUpdate()
    {
        betDisplay.text = $"Bet: ${GameController.Instance.bet}";
        balanceDisplay.text = $"${GameController.Instance.balance}";

        balanceDisplay.color = Color.Lerp(balanceDisplay.color, Color.white, 0.03f);
    }

    public void InstantiateButtons(int amount, string message, bool showButtonNumber)
    {
        ClearButtons();
        for (int i = 1; i < amount + 1; i++)
        {
            string buttonMessage = message + (showButtonNumber ? $"# {i}" : string.Empty);

            Buttons.Add(Instantiate((GameObject)Resources.Load("Bet Button"), buttonParent));
            Buttons[i - 1].GetComponent<BetButton>().selection = i;
            Buttons[i - 1].GetComponent<BetButton>().messageDisplay.text = buttonMessage;
        }
    }
    public void InstantiateButtons(int amount, string[] message, bool showButtonNumber)
    {
        ClearButtons();
        for (int i = 1; i < amount + 1; i++)
        {
            string buttonMessage = message[i - 1] + (showButtonNumber ? $"# {i}" : string.Empty);

            Buttons.Add(Instantiate((GameObject)Resources.Load("Bet Button"), buttonParent));
            Buttons[i - 1].GetComponent<BetButton>().selection = i;
            Buttons[i - 1].GetComponent<BetButton>().messageDisplay.text = buttonMessage;
        }
    }
    public void SetBalanceColor(Color color)
    {
        balanceDisplay.color = color;
    }

    public PopText InstantiatePopText()
    {
        if (popText != null) Destroy(popText);
        popText = Instantiate((GameObject)Resources.Load("Pop-Text"), transform);
        return popText.GetComponent<PopText>();
    }
    public PopText InstantiateQuestionText()
    {
        if (popText != null) Destroy(popText);
        popText = Instantiate((GameObject)Resources.Load("Question-Text"), transform);
        return popText.GetComponent<PopText>();
    }
    public PopText InstantiateWinnerText()
    {
        if (popText != null) Destroy(popText);
        popText = Instantiate((GameObject)Resources.Load("Winner-Text"), transform);
        return popText.GetComponent<PopText>();
    }

    public void ClearButtons()
    {
        foreach (GameObject gameObject in Buttons) { Destroy(gameObject); }
        Buttons.Clear();
    }
}

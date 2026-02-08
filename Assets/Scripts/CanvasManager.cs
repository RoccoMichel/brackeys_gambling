using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Animator betMenuController;
    [SerializeField] private TMP_Text balanceDisplay;
    [SerializeField] private TMP_Text betDisplay;
    [SerializeField] private Transform buttonParent;
    public Transform opponentParent;
    private List<GameObject> Buttons = new();
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

            Buttons.Add(Instantiate((GameObject)Resources.Load("UI/Bet Button"), buttonParent));
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

            Buttons.Add(Instantiate((GameObject)Resources.Load("UI/Bet Button"), buttonParent));
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
        popText = Instantiate((GameObject)Resources.Load("UI/Pop-Text"), transform);
        return popText.GetComponent<PopText>();
    }
    public PopText InstantiateQuestionText()
    {
        if (popText != null) Destroy(popText);
        popText = Instantiate((GameObject)Resources.Load("UI/Question-Text"), transform);
        return popText.GetComponent<PopText>();
    }
    public PopText InstantiateWinnerText()
    {
        if (popText != null) Destroy(popText);
        popText = Instantiate((GameObject)Resources.Load("UI/Winner-Text"), transform);
        return popText.GetComponent<PopText>();
    }

    public void ClearButtons()
    {
        foreach (GameObject gameObject in Buttons) { Destroy(gameObject); }
        Buttons.Clear();
    }

    private bool betHidden;
    public void HideBetMenu()
    {
        if (betHidden) return;

        betHidden = true;
        betMenuController.Rebind();
        betMenuController.Update(0f);
        betMenuController.Play("fly-out");
    }
    public void ShowBetMenu()
    {
        if (!betHidden) return;

        betHidden = false;
        betMenuController.Rebind();
        betMenuController.Update(0f);
        betMenuController.Play("fly-in");
    }
    private bool opsHidden;
    private Animator opsController;
    public void HideOpponents()
    {
        if (opsHidden) return;

        if (opsController == null) opsController = opponentParent.GetComponent<Animator>();
        opsHidden = true;
        opsController.Rebind();
        opsController.Update(0f);
        opsController.Play("slide-out");
    }
    public void ShowOpponents()
    {
        if (!opsHidden) return;

        if (opsController == null) opsController = opponentParent.GetComponent<Animator>();
        opsHidden = false;
        opsController.Rebind();
        opsController.Update(0f);
        opsController.Play("slide-in");
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TMP_Text balanceDisplay;
    [SerializeField] private Transform buttonParent;
    public Player player;
    List<GameObject> Buttons = new();

    private void Start()
    {
        RefreshCanvas();
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
}

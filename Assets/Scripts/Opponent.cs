using TMPro;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    public int balance = 1000;
    public int bet;
    public int selection;
    [SerializeField] private TMP_Text infoDisplay;

    public void ChooseBet()
    {
        selection = Random.Range(1, GameController.Instance.optionsCount + 1);
        bet = Random.Range(0, balance / 4);
    }

    private void Update()
    {
        if (balance > 0) infoDisplay.text = $"{selection} | ${balance}";
        else infoDisplay.text = "OUT";
    }

    public void Win()
    {
        balance = bet * 2;
        bet = 0;
    }

    public void Lose()
    {
        balance -= bet;
        bet = 0;
    }
}

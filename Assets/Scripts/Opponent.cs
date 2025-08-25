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

    private void FixedUpdate()
    {
        if (balance > 0) infoDisplay.text = $"{selection} | ${balance}";
        else infoDisplay.text = "OUT";

        infoDisplay.color = Color.Lerp(infoDisplay.color, Color.white, 0.03f);
    }

    public void Win()
    {
        if (balance <= 0) return;

        balance += bet * 2;
        bet = 0;

        infoDisplay.color = Color.green;
    }

    public void Lose()
    {
        if (balance <= 0) return;

        balance -= bet;
        bet = 0;

        infoDisplay.color = Color.red;
    }
}

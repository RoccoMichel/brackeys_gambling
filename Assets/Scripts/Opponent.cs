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
        selection = Random.Range(0, GameController.Instance.optionsCount + 1);
        bet = Random.Range(0, balance / 10) * 10;
    }

    private void Update()
    {
        infoDisplay.text = $"{selection + 1} | ${balance}";
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

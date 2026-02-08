using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Opponent : MonoBehaviour
{
    [Header("Start Balance from GameSettings")]
    public int balance;
    public int bet;
    public int choice;
    public Sprite profile;
    [SerializeField] private TMP_Text balanceDisplay;
    private GameSettings settings;

    private void Start()
    {
        settings = GameController.Instance.gameSettings;
        balance = settings.roundSettings.startBalance;
        profile = settings.moreSettings.opponentProfiles[Random.Range(0, settings.moreSettings.opponentProfiles.Length)];
    }
    public void ChooseBet()
    {
        if (balance == 0) return;

        choice = Random.Range(1, GameController.Instance.currentGame.choices + 1);
        int bettingRange = Mathf.CeilToInt(GameController.Instance.bet * 
            settings.roundSettings.opponentBettingRangeFromPlayer * 0.01f);

        bet = Mathf.Clamp(Random.Range(
            GameController.Instance.bet - bettingRange, GameController.Instance.bet + bettingRange), 1, balance);
    }

    public void ResetBalance()
    {
        balance = settings.roundSettings.startBalance;
        balanceDisplay.color = Color.blue;
    }

    private void FixedUpdate()
    {
        if (balance > 0) balanceDisplay.text = balance.ToString();
        else balanceDisplay.text = "OUT";

        GetComponent<Image>().sprite = profile;
        balanceDisplay.color = Color.Lerp(balanceDisplay.color, Color.white, 0.03f);
    }

    public void Win()
    {
        if (balance <= 0) return;

        balance += bet * 2;
        bet = 0;

        balanceDisplay.color = Color.green;
    }

    public void Lose()
    {
        if (balance <= 0) return;

        balance -= bet;
        bet = 0;

        balanceDisplay.color = Color.red;
    }
}

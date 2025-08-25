using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public string playerName = "Gambling Addict";
    public int balance = 1000;
    public int bet = 1;
    public int selection;

    public void Win()
    {
        balance += bet * 2;
        bet = Mathf.CeilToInt(balance / 2);
    }

    public void Lose()
    {
        balance -= bet;
        bet = Mathf.CeilToInt(balance / 2);

        if (balance <= 0) SceneManager.LoadScene("GameOver");
    }
}

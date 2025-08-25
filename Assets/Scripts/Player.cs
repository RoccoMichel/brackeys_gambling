using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class Player : ScriptableObject
{
    public string playerName = "YOU";
    public int balance = 1000;
    public int bet;
    public int selection;

    public void Win()
    {
        balance += bet*2;
        bet = 0;
    }

    public void Lose()
    {
        balance -= bet;
        bet = 0;

        if (balance <= 0) SceneManager.LoadScene("GameOverScene");
    }
}

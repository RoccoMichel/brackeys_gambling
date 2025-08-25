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
        bet = 1;
    }

    public void Lose()
    {
        balance -= bet;
        bet = 1;

        if (balance <= 0) SceneManager.LoadScene("GameOverScene");
    }
}

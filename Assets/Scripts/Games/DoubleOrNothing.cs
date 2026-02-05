using UnityEngine;
using UnityEngine.SceneManagement;

public class DoubleOrNothing : Minigame
{
    protected override void OnStart()
    {
        GameController.gamesPlayed = -1;
        GameController.canvas.InstantiateButtons(2, new string[] { "Double", "Nothing" }, showButtonNumber);

    }
    public override void GameStart()
    {
        GameFinish(1);
    }

    public override void GameFinish(int winner)
    {
        if (GameController.choice == 2)
        {
            SceneManager.LoadScene("GameWin");
            return;
        }
        GameController.bet = 0;
        base.GameFinish(winner);
    }
}

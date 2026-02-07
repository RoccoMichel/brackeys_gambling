using UnityEngine;

public class DoubleOrNothing : Minigame
{
    protected override void OnStart()
    {
        GameController.gamesPlayed = -1;
        GameController.canvas.InstantiateButtons(2, new string[] { "Double", "Nothing" }, showButtonNumber);
        GameController.canvas.InstantiatePopText().SetValues("Double or Nothing", 2);
    }
    public override void GameStart()
    {
        GameFinish(1);
    }

    public override void GameFinish(int winner)
    {
        if (GameController.choice == 2)
        {
            Instantiate((GameObject)Resources.Load("Podium"));
            return;
        }
        GameController.bet = 0;
        base.GameFinish(winner);
    }
}

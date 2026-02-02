using UnityEngine;

public class RockPaperScissors : Minigame
{
    private int counterRPS = 1;
    protected override void OnStart()
    {
        GameController.canvas.InstantiateButtons(3, new string[] { "Rock", "Paper", "Scissors" }, showButtonNumber);
        counterRPS = Random.Range(1, 4);
        Debug.Log("Counter is " + GetRPS(counterRPS));
    }
    public override void GameStart()
    {
        base.GameStart();


        if (counterRPS == GameController.choice) // Tie scenario
        {
            GameController.canvas.InstantiateWinnerText().SetValues("TIE\nGO AGAIN!", 2f);
            OnStart();
            return;
        }

        GameFinish(counterRPS);
    }
    public override void GameFinish(int winner)
    {
        GameController.canvas.InstantiateWinnerText().SetValues("It was " + GetRPS(winner), 3f);
        winner++;
        if (winner > 3) winner = 1;
        base.GameFinish(winner);
    }

    private string GetRPS(int index)
    {
        index = Mathf.Clamp(index, 1, 4);
        switch (index)
        {
            case 1:
                return "Rock";
            case 2:
                return "Paper";
            case 3:
                return "Scissors";
        }

        Debug.LogWarning("Failed to GetRPS from index: " + index);
        return string.Empty;
    }
}

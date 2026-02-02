using UnityEngine;

public class Minigame : MonoBehaviour
{
    [Header("Minigame Attributes")]
    public int choices = 2;
    protected int winner;
    [SerializeField, Tooltip("The words before the index on the button choices, leave out ': '")]
    private string buttonMessage = "Bet on";
    [SerializeField] protected bool showButtonNumber = true;
    protected GameController GameController { get { return GameController.Instance; } }


    protected virtual void OnStart()
    {
        GameController.canvas.InstantiateButtons(choices, buttonMessage, showButtonNumber);
    }

    private void Start() => OnStart();

    public virtual void GameStart()
    {
        // Start the game after the player has made a bet!
    }

    public virtual void GameFinish(int winner)
    {
        // GameController.canvas.InstantiateWinnerText().SetValues($"{winner} Won!", 3f);
        GameController.GameFinish(winner);
        Destroy(gameObject);
    }

    protected void GameFail()
    {
        Debug.LogError("Minigame failed, starting new one.");
        GameController.NewGame();
        Destroy(gameObject);
    }
}

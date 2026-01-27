using UnityEngine;

public class Minigame : MonoBehaviour
{
    [Header("Minigame Attributes")]
    public int choices = 2;
    protected int winner;
    [SerializeField, Tooltip("The words before the index on the button choices, leave out ': '")]
    private string buttonMessage = "Bet on";
    protected GameController GameController { get { return GameController.Instance; } }


    protected virtual void OnStart()
    {
        GameController.canvas.InstantiateButtons(choices, buttonMessage);
    }

    private void Start() => OnStart();

    public virtual void GameStart()
    {
        // Start the game after the player has made a bet!
    }

    public virtual void GameFinish(int winner)
    {
        GameController.GameFinish(winner);
        GameController.canvas.InstantiatePopText().SetValues($"{winner} Won!", 3f);
        Destroy(gameObject);
    }
}

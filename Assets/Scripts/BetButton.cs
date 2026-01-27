using TMPro;
using UnityEngine;

public class BetButton : MonoBehaviour
{
    public int selection = 0;
    public TMP_Text messageDisplay;
    GameController gameController;



    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void PlayerChoice()
    {
        gameController.choice = selection;

        gameController.StartGame();
    }
}

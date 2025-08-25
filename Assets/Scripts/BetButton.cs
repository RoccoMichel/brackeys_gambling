using TMPro;
using UnityEngine;

public class BetButton : MonoBehaviour
{
    public int selection = 0;
    [SerializeField] private TMP_Text display;
    GameController gameController;


    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        display.text = $"Bet on {selection}";
    }

    public void SetSelection()
    {
        gameController.player.selection = selection;

        gameController.StartRace();
    }
}

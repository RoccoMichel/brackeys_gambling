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
        gameController.PlayerList[0].selection = selection;

        Debug.Log($"{gameController.PlayerList[0].playerName} bet on: {selection}");
    }
}

using TMPro;
using UnityEngine;

public class BetButton : MonoBehaviour
{
    public int selection = 0;
    public string message = "gamble";
    [SerializeField] private TMP_Text display;
    GameController gameController;



    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        display.text = message;

    }

    public void SetSelection()
    {
        gameController.selection = selection;

        gameController.StartGame();
    }
}

using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SnailManager : MonoBehaviour
{
    [SerializeField] private Snail[] snails;
    [SerializeField] private BoxCollider2D goal;
    [SerializeField] private float minSnailSpeed;
    [SerializeField] private float maxSnailSpeed;
    private int winner;

    private void Start()
    {
        SetSpeed();

    }
    private void SetSpeed()
    {
        foreach(Snail snail in snails)
        {
            snail.SetSpeed(Random.Range(minSnailSpeed, maxSnailSpeed));
        }
        StartRace();
    }
    private void StopRace()
    {
        foreach(Snail snail in snails)
        {
            snail.HaltStop();
        }
    }
    private void StartRace()
    {
        foreach(Snail snail in snails)
        {
            snail.StartMoving();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Snail winSnail = collision.GetComponent<Snail>();
        StopRace();
        winner = winSnail.GetDaNumbah();
        GameObject text = Instantiate((GameObject)Resources.Load("Winner Text"), GameController.Instance.canvas.transform);
        text.GetComponent<TMP_Text>().text = $"Snail {winner} WON!";
        Destroy(text, 5);
        GameController.Instance.RaceFinish(winner);
        Destroy(this.transform.parent.gameObject);
    }
}

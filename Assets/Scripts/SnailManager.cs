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
        winner = winSnail.GetDaNumbah();
        Debug.Log(winner + " won");
        // tell gameController
    }
}

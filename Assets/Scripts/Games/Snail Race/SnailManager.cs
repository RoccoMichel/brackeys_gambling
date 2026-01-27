using UnityEngine;

public class SnailManager : Minigame
{
    [Header("Snail Manager Attributes")]
    [SerializeField] private Snail[] snails;
    [SerializeField] private float minSnailSpeed;
    [SerializeField] private float maxSnailSpeed;

    protected override void OnStart()
    {
        base.OnStart();
        SetSpeed();
        choices = snails.Length;
    }
    private void SetSpeed()
    {
        foreach(Snail snail in snails)
        {
            snail.SetSpeed(Random.Range(minSnailSpeed, maxSnailSpeed));
        }
    }

    public override void GameStart()
    {
        finished = false;
        foreach(Snail snail in snails)
        {
            snail.StartMoving();
        }

        GetComponent<AudioSource>().Play();
    }

    private void StopRace()
    {
        foreach (Snail snail in snails)
        {
            snail.HaltStop();
        }
    }

    private bool finished;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (finished) return;
        finished = true;

        Snail winSnail = collision.GetComponent<Snail>();
        StopRace();

        winner = winSnail.GetDaNumbah();
        GameFinish(winner);
    }
}

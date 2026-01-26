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
    }
    private void StopRace()
    {
        foreach(Snail snail in snails)
        {
            snail.HaltStop();
        }
    }
    public void StartRace()
    {
        finished = false;
        foreach(Snail snail in snails)
        {
            snail.StartMoving();
        }

        GetComponent<AudioSource>().Play();
    }

    bool finished;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (finished) return;

        finished = true;
        Snail winSnail = collision.GetComponent<Snail>();
        StopRace();
        winner = winSnail.GetDaNumbah();
        Instantiate((GameObject)Resources.Load("Winner-Text"), GameController.Instance.canvas.transform).GetComponent<PopText>().SetValues($"Snail #{winner} WON!", 3f);
        GameController.Instance.GameFinish(winner);
        Destroy(this.transform.parent.gameObject);
    }
}

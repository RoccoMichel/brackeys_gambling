using UnityEngine;

public class Snail : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private int numbah;
    private float speed;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public void HaltStop()
    {
        rb2D.linearVelocityX = 0f;
    }
    public void StartMoving()
    {
        rb2D.linearVelocityX = speed;
    }

    public int GetDaNumbah()
    {
        return numbah;
    }
}

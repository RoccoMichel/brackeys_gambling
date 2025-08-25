using System.Collections;
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
        StopAllCoroutines();
    }
    public void StartMoving()
    {
        StartCoroutine(Accelerate());
    }

    public int GetDaNumbah()
    {
        return numbah;
    }

    IEnumerator Accelerate()
    {
        float acceleration = 0;
        while (acceleration < speed)
        {
            rb2D.linearVelocityX = acceleration;
            acceleration = Mathf.Clamp(acceleration += Time.deltaTime, 0, speed);

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
